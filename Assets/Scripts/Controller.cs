using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Controller : NetworkBehaviour
{

    [SyncVar]
    public float Hor;

    [SyncVar]
    public float Vert;




    public FixedJoystick joystick;

    public Text val;

    public Text val1;


    public static Controller instance;

    public int index = 0;
    public int SelectedLevel = 0;
    public int language = 0;
    public bool isStatic;


    public long frameCount = -1;

    public void Start()
    {
        DontDestroyOnLoad(this);

        if (!isServer && isLocalPlayer)
        {
            Debug.Log("Check", this);
            joystick = FindObjectOfType<FixedJoystick>();
            instance = this;
            isStatic = true;
        }
        else if (!isLocalPlayer && isServer)
        {
            instance = this;
            isStatic = true;
        }

        LocalizationManager.instance.Init(language == 0 ? Languages.ENGLISH : Languages.TAMIL);


    }








    public void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (joystick)
            {
                SetValue(joystick.Horizontal, joystick.Vertical);
            }
        }



    }

    [Command]
    void SetValue(float x, float y)
    {
        Hor = x;
        Vert = y;
    }


    [Command]
    public void Interact()
    {

        CollisionManager.instance.InteractButton();
    }


    [TargetRpc]
    public void Interactable(bool val, int index)
    {
        SelectedLevel = index;
        UIController.instance.SetInteractable(val);
    }


    [Command]
    public void BackButton(int a)
    {

        VideoSelection.instance?.OnBack(a);

        CollisionManager.instance?.OnBack(a);
    }

    ///Video

    [Command]
    public void SelectVideo(int _index)
    {
        index = _index;

        VideoSelection.instance.SelectVideo(index);
    }


    [TargetRpc]
    public void VideoCompleted()
    {
        var Vc = VideoController.instance;
        Vc.slider.value = 0;
        // VideoController.instance.SetVideoVal(true);
        // VideoController.instance.player.Prepare();
        Vc.player.clip = null;
        Vc.player.clip = Vc.currentVideo;
        Vc.player.Prepare();

        VideoController.instance.Pause();

        UIController.instance.Back(3);

    }


    [Command]
    public void PlayPause(bool val)
    {
        Debug.Log("play function " + val);
        if (val)
        {
            VideoSelection.instance.PlayVideo();
        }
        else
        {
            VideoSelection.instance.PauseVideo();
        }

    }

    [Command]
    public void SetVideoTime(float a)
    {
        VideoSelection.instance.SetDuration(a);
    }

    [Command]
    public void SetVolume(float a)
    {
        VideoSelection.instance.SetVolume(a);
    }

    [Command]
    public void GetVolume()
    {
        VideoSelection.instance.UpdateVolume();
    }

    [TargetRpc]
    public void UpdateVolume(float a)
    {
        VideoController.instance.UpdateVolume(a);
    }


    [Command]
    public void UpdateLanguage(int index, bool UpdateAudio)
    {
        language = index;
        if (UpdateAudio)
        {
            VideoSelection.instance.UpdateAudio(index);


        }
        LocalizationManager.instance.SetLanguage(index == 0 ? Languages.ENGLISH : Languages.TAMIL);
    }


    [Command]
    public void StartFunction()
    {
        CollisionManager.instance.OnClickStart();
    }


    [ClientRpc]
    public void SyncVideo(long _frameCount)
    {
        Debug.Log("prepare function " + _frameCount);
        if (VideoSelection.instance)
        {
            Debug.Log("prepare function " + VideoSelection.instance.player.frame);

            if (_frameCount < 0) _frameCount = 1;


            {
                VideoSelection.instance.player.frame = _frameCount;
            }

            //VideoSelection.instance.player.Play();

            VideoSelection.instance.videoPrepared = false;

            StartCoroutine(UpdateFrame());
        }

        if (VideoController.instance)
        {
            if (_frameCount < 0) _frameCount = 1;

            {
                VideoController.instance.player.frame = _frameCount;
            }

            StartCoroutine(UpdateFrame());

        }

    }

    IEnumerator UpdateFrame()
    {
        if (VideoSelection.instance)
        {
            var vp = VideoSelection.instance;
            vp.player.Prepare();
            while (!vp.player.isPrepared || !vp.videoPrepared)
            {
                yield return new WaitForEndOfFrame();
            }

            PlaySyncdVideo();
        }

        if (VideoController.instance)
        {
            var vp = VideoController.instance;
            vp.player.Prepare();
            while (!vp.player.isPrepared)
            {
                yield return new WaitForEndOfFrame();
            }

            VideoPrepared();
        }
    }


    [ClientRpc]
    public void PlaySyncdVideo()
    {
        frameCount = -1;
        VideoSelection.instance?.player.Play();

        VideoController.instance?.player.Play();


    }


    [Command]
    public void VideoPrepared()
    {


        VideoSelection.instance.videoPrepared = true;

    }

}
