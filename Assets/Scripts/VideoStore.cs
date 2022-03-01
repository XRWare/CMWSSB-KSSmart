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

        public Video360Data v_360Data;
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


[System.Serializable]
public class Video360Data
{
    public VideoClip[] _360Clip;

    public string[] _buttonTitle;

    public string[] _TbuttonTitle;
}