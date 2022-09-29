using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDTO
{
    public string ActivityName;
    public DateTime Timestamp;
    public int TotalPlaylistWatchTime; // In Seconds
    public int ClipWatchTime; // In Seconds
    public string VideoClipName;
    public int VideoLenght;

    public DataDTO()
    {
        
    }

    public DataDTO(string activityName, DateTime time, float totalWatchTime = 0, float clipWatchTime = 0, string videoClipName = "", double lenght = 0)
    {
        Debug.Log($"<color=yellow>Data created for submission of event: {activityName}</color>");

        totalWatchTime = (float)(Math.Truncate(totalWatchTime * 100.0) / 100.0);
        clipWatchTime = (float)(Math.Truncate(clipWatchTime * 100.0) / 100.0);
        lenght = (Math.Truncate(lenght * 100.0) / 100.0);

        ActivityName = activityName;
        TotalPlaylistWatchTime = (int)totalWatchTime;
        Timestamp = time;
        ClipWatchTime = (int)clipWatchTime;
        VideoClipName = videoClipName;
        VideoLenght = (int)lenght;
    }
}
