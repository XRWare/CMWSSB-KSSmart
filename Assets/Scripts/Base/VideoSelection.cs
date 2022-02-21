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

    public int videoIndex;

    public VideoClip CurrentVideo;

    public GameObject videoThumbnailPanel;

    public GameObject videoPlayerPanel;



    private bool sliderUpdated = false;




    void Start()
    {
        instance = this;

        videoPlayerPanel.SetActive(false);
        videoThumbnailPanel.SetActive(true);
        foreach (var item in videoPanel)
        {

            item.SetActive(false);
        }
        sliderUpdated = false;

    }

    void OnEnable()
    {
        GameObject.FindObjectOfType<Manager>().serverObjects.SetActive(false);

        player.loopPointReached += OnVideoCompleted;
    }

    void OnDisable()
    {
        player.loopPointReached -= OnVideoCompleted;

        GameObject.FindObjectOfType<Manager>().serverObjects.SetActive(true);
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
        CurrentVideo = VideoStore._instance.VideoInfo[CollisionManager.instance.val].v_Data[index].clip[language];
        videoIndex = index;
    }

    public void PlayVideo()
    {
        videoPlayerPanel.SetActive(true);
        videoThumbnailPanel.SetActive(false);

        if (!player.clip || player.clip != CurrentVideo)
        {

            UpdateAudio(Controller.instance.language);

            player.clip = CurrentVideo;
            player.Play();

            //Controller.instance.UpdateSlider((float)player.time, (float)player.length);
            sliderUpdated = true;
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


        // Controller.instance.UpdateSlider(percent, 1);
        Debug.Log("Frame server " + player.frame);

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
        if (player.isPlaying)
        {
            Controller.instance.UpdateSlider((float)player.time, (float)player.length);
        }

    }


    public void OnVideoCompleted(VideoPlayer vp)
    {
        Controller.instance.VideoCompleted();

    }


    public void UpdateAudio(int val)
    {

        language = val;
        //if (player.isPlaying)
        {

            player.Pause();
            var a = player.frame;

            videoPanel[videoIndex].SetActive(true);

            CurrentVideo = VideoStore._instance.VideoInfo[CollisionManager.instance.val].v_Data[videoIndex].clip[language];
            player.clip = CurrentVideo;

            player.frame = a;

            {
                Controller.instance.SyncVideo((int)a);
            }

            player.Play();

        }

    }


    public void OnBack(int a)
    {
        if (a == 1)
        {
            CollisionManager.instance.BeginScreen.SetActive(true);
        }
        else if (a == 2)
        {
            foreach (var item in videoPanel)
            {

                item.SetActive(false);
            }

            Invector.vCharacterController.vThirdPersonInput.ResetPosition(CollisionManager.instance.gameObject.transform);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else if (a == 3)
        {

            player.Pause();
            videoPlayerPanel.SetActive(false);
            videoThumbnailPanel.SetActive(true);
        }
    }
}


[System.Serializable]
public class VideoStorage
{

    public VideoClip[] videoClip;


}
