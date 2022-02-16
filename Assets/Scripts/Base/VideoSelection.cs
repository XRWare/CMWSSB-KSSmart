using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSelection : MonoBehaviour
{
    public static VideoSelection instance;

    public VideoStorage[] videos;

    public GameObject[] videoPanel;

    public VideoPlayer player;

    public int language;

    public VideoClip CurrentVideo;

    public GameObject videoThumbnailPanel;

    public GameObject videoPlayerPanel;


    public GameObject soundButton;

    public GameObject soundPanel;


    void Start()
    {
        instance = this;

        videoPlayerPanel.SetActive(false);
        videoThumbnailPanel.SetActive(true);
        foreach (var item in videoPanel)
        {

            item.SetActive(false);
        }


    }

    void OnEnable()
    {
        player.loopPointReached += OnVideoCompleted;
    }

    void OnDisable()
    {
        player.loopPointReached -= OnVideoCompleted;
    }

    public void SelectVideo(int index)
    {

        videoPlayerPanel.SetActive(false);
        videoThumbnailPanel.SetActive(true);


        foreach (var item in videoPanel)
        {
            item.SetActive(false);
        }

        videoPanel[index].SetActive(true);
        player.clip = null;
        CurrentVideo = videos[index].videoClip[language];
    }

    public void PlayVideo()
    {
        videoPlayerPanel.SetActive(true);
        videoThumbnailPanel.SetActive(false);

        if (!player.clip)
        {
            player.clip = CurrentVideo;
            player.Play();
        }

        if (player.isPaused)
        {
            player.Play();
        }


    }

    public void PauseVideo()
    {
        player.Pause();
    }

    public void SetDuration(float percent)
    {

        player.frame = (long)(player.frameCount * percent);

        player.Play();
        Controller.instance.UpdateSlider((float)player.time, (float)player.length);
    }

    public void SetVolume(float percent)
    {
        player.SetDirectAudioVolume(0, percent);
    }

    public void UpdateVolume()
    {
        Controller.instance.UpdateVolume(player.GetDirectAudioVolume(0));
    }

    void LateUpdate()
    {
        if (player.isPlaying && player.frameCount > 0)
        {
            Controller.instance.UpdateSlider((float)player.time, (float)player.length);
        }
    }


    public void OnVideoCompleted(VideoPlayer vp)
    {
        Controller.instance.VideoCompleted();

        videoPlayerPanel.SetActive(false);
        videoThumbnailPanel.SetActive(true);
    }
}


[System.Serializable]
public class VideoStorage
{

    public VideoClip[] videoClip;

}
