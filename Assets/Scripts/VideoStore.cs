using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStore : MonoBehaviour
{
    public static VideoStore _instance;


    [System.Serializable]
    public struct data
    {
        public VideoData[] v_Data;
    }

    public data[] VideoInfo;

    void Start()
    {
        _instance = this;

    }


}




[System.Serializable]
public class VideoData
{
    public VideoClip[] clip;

    public string Title;

    public string T_Title;

    public string Description;

    public string T_Description;


    public Texture thumbnailClip;

    public bool useVideoClip;


}