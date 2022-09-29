using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDTO
{
    public string ActivityName;
    public DateTime Timestamp;
    public string PlayedDuration; // In Seconds
    public string VideoClipName;
    public string VideoLenght;

    public DataDTO()
    {
        
    }

    public DataDTO(string activityName, DateTime time, float duration = 0, string videoClipName = "", double lenght = 0)
    {
        Debug.Log($"<color=yellow>Data created for submission of event: {activityName}</color>");

        duration = (float)(Math.Truncate(duration * 100.0) / 100.0);
        lenght = (Math.Truncate(lenght * 100.0) / 100.0);

        ActivityName = activityName;
        Timestamp = time;
        PlayedDuration = duration <= 0 ? "--" : $"{(int)duration} seconds";
        VideoClipName = videoClipName;
        VideoLenght = $"{(int)lenght} seconds";
    }
}
