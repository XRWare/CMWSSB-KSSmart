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



    public bool videoPrepared = false;




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


        {

            // Controller.instance.SyncVideo(player.frame);

            StartCoroutine(CheckVideoPlayer());
        }


    }

    IEnumerator CheckVideoPlayer()
    {
        player.Prepare();
        while (!videoPrepared || !player.isPrepared)
        {
            Debug.Log("preparing");
            yield return new WaitForEndOfFrame();
        }



        Controller.instance.SyncVideo(Controller.instance.frameCount == -1 ? player.frame : Controller.instance.frameCount);

        videoPrepared = false;
    }


    public void PauseVideo()
    {
        player.Pause();
    }

    public void SetDuration(float percent)
    {


        player.time = (player.length * percent);


    }

    public void SetVolume(float percent)
    {
        player.SetDirectAudioVolume(0, percent);
    }

    public void UpdateVolume()
    {
        Controller.instance.UpdateVolume(player.GetDirectAudioVolume(0));
    }

    // void LateUpdate()
    // {
    //     if (player.isPlaying)
    //     {
    //         Controller.instance.UpdateSlider((float)player.time, (float)player.length);
    //     }

    // }


    public void OnVideoCompleted(VideoPlayer vp)
    {
        Controller.instance.VideoCompleted();

    }


    public void UpdateAudio(int val)
    {

        language = val;
        {

            player.Pause();
            var a = player.frame;
            videoPanel[videoIndex].SetActive(true);
            CurrentVideo = VideoStore._instance.VideoInfo[CollisionManager.instance.val].v_Data[videoIndex].clip[language];
            player.clip = CurrentVideo;
            player.frame = a;
            Controller.instance.frameCount = a;
            player.Prepare();
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
            foreach (var item in videoPanel)
            {

                item.SetActive(false);
            }
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
