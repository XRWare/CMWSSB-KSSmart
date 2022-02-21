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
    public void UpdateSlider(float currentTime, float totalTime)
    {
        VideoController.instance.SetSlider(currentTime, totalTime);
    }

    [TargetRpc]
    public void VideoCompleted()
    {
        VideoController.instance.Pause();
    }


    [Command]
    public void PlayPause(bool val)
    {

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


    [TargetRpc]
    public void SyncVideo(int count)
    {
        UIController.instance.PlayUpdatedAudio(count);

    }
}
