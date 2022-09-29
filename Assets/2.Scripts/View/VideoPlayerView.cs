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
    private float clipWatchtime;
    private float totalWatchtime;
    private DateTime timeStamp;
    private string clipName = "";
    private double clipLength;

    private void Start()
    {
        videoPlayer = transform.GetComponent<VideoPlayer>();
        videoPlayer.started += OnStarted;
    }

    private void Update()
    {
        if (videoPlayer.isPlaying)
        {
            totalWatchtime += Time.deltaTime;
            clipWatchtime += Time.deltaTime;
        }
    }

    private void OnStarted(VideoPlayer vp)
    {
        if (clipWatchtime > 0) // Submit previous video data
        {
            ProcessDataThenSubmit("New Video Played. Saving Previous Data");
        }

        clipWatchtime = 0;
        timeStamp = DateTime.Now;
        clipName = vp.clip.name;
        clipLength = vp.clip.length;

        ProcessDataThenSubmit("Video clip started playing.");
    }

    private void ProcessDataThenSubmit(string logTitle)
    {
        DataDTO dataLog = new DataDTO(
                activityName: logTitle,
                time: DateTime.Now,
                totalWatchTime: totalWatchtime,
                clipWatchTime: clipWatchtime,
                videoClipName: clipName,
                length: clipLength
            );

        CPELoginControl.Api.SubmitSessionData((JObject)JToken.FromObject(dataLog));
    }

    private void OnDestroy()
    {
        if (clipWatchtime > 0) // Submit last video data
        {
            ProcessDataThenSubmit("Video player page closed");
        }
    }
}
