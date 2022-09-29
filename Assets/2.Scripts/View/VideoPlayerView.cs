using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerView : MonoBehaviour 
{
    private VideoPlayer videoPlayer = null;
    private float duration;
    private DateTime startTime;
    private string clipName = "";

    private void Start()
    {
        videoPlayer = transform.GetComponent<VideoPlayer>();
        videoPlayer.started += OnStarted;
    }

    private void Update()
    {
        if (videoPlayer.isPlaying)
        {
            duration += Time.deltaTime;
        }
    }

    private void OnStarted(VideoPlayer vp)
    {
        if (duration > 0) // Submit previous video data
        {
            DataDTO prevData = new DataDTO(
                activityName: "New Video Played. Saving Previous Data",
                time: DateTime.Now,
                duration: duration,
                videoClipName: clipName,
                lenght: vp.clip.length
            );
            CPELoginControl.Api.SubmitSessionData((JObject)JToken.FromObject(prevData));
        }

        duration = 0;
        startTime = DateTime.Now;
        clipName = vp.clip.name;
        DataDTO newData = new DataDTO(
                activityName: "Video Started Playing",
                time: DateTime.Now,
                duration: duration,
                videoClipName: clipName,
                lenght: vp.clip.length
            );

        CPELoginControl.Api.SubmitSessionData((JObject)JToken.FromObject(newData));
    }

    private void OnDestroy()
    {
        if (duration > 0) // Submit last video data
        {
            DataDTO prevData = new DataDTO(
                activityName: "Video Player Closed",
                time: DateTime.Now,
                duration: duration,
                videoClipName: clipName
            );
            CPELoginControl.Api.SubmitSessionData((JObject)JToken.FromObject(prevData));
        }
    }
}
