using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
public class VideoController : MonoBehaviour
{

    public Slider slider;
    public Slider volumeSlider;
    public static VideoController instance;

    public Button playbutton;

    public Button playerPlaButton;

    public Button playerPauseButton;

    public Text timer;

    public GameObject volumeSliderPanel;




    private PanelObjects currentPanelObject;


    public LocalizedComponent title;

    public LocalizedComponent description;

    public VideoPlayer player;

    public VideoClip currentVideo;

    public bool languageUpdated = false;
    void Start()
    {
        instance = this;
        playbutton.gameObject.SetActive(false);
        // SetAudio(Controller.instance.language);
        //LocalizationManager.instance.Init(Languages.TAMIL);
    }



    public void Play()
    {



        if (!player.clip || player.frame == 0 || (player.clip != currentVideo && currentVideo != null))
        {

            player.clip = currentVideo;
            // UIController.instance.UpdateLanguage();
            player.frame = 0;
            slider.value = 0;



        }


        {

            player.Play();
        }

        volumeSliderPanel.SetActive(false);
        Controller.instance.PlayPause(true);
        playerPlaButton.gameObject.SetActive(false);
        playerPauseButton.gameObject.SetActive(true);



    }

    public void Pause()
    {
        Controller.instance.PlayPause(false);
        playerPlaButton.gameObject.SetActive(true);
        playerPauseButton.gameObject.SetActive(false);
        player.Pause();
    }


    public void SetSlider(float val, float totalVal)
    {

        slider.value = val / totalVal;

        timer.text = Mathf.Floor((int)val / 60).ToString("00") + " : " + ((int)val % 60).ToString("00") + " / " + Mathf.Floor((int)totalVal / 60).ToString("00") + " : " + ((int)totalVal % 60).ToString("00");

        if (languageUpdated)
        {
            // player.time = val;
            languageUpdated = false;
        }


    }


    public void SetVideoVal(bool overRide = false)
    {

        if (playerPlaButton.gameObject.activeInHierarchy || overRide)
        {


            player.frame = (long)(player.frameCount * slider.value);
            Controller.instance.SetVideoTime(slider.value);
            Debug.Log("Frame " + player.frame);
        }

    }


    public void DelayPlay(float val)
    {
        Invoke("Play", val);
    }


    public void SelVid(GameObject g)
    {
        currentPanelObject = g.GetComponent<PanelObjects>();

        title.EnglishTranslated = currentPanelObject.title.EnglishTranslated;
        title.TamilTranslated = currentPanelObject.title.TamilTranslated;

        description.EnglishTranslated = currentPanelObject.description.EnglishTranslated;

        description.TamilTranslated = currentPanelObject.description.TamilTranslated;

        slider.value = 0;

    }
    public void SelectVideo(int a)
    {
        slider.value = 0;
        playbutton.gameObject.SetActive(true);
        Controller.instance.index = a;
        Controller.instance.SelectVideo(a);
        playerPlaButton.gameObject.SetActive(true);
        playerPauseButton.gameObject.SetActive(false);
        currentVideo = VideoStore._instance.VideoInfo[Controller.instance.SelectedLevel].v_Data[a].clip[Controller.instance.language];
        player.clip = currentVideo;
        UIController.instance.UpdateLanguage(false);
    }

    public void UpdateVolume(float a)
    {
        volumeSlider.value = a;
    }

    public void SetVolume()
    {
        Controller.instance.SetVolume(volumeSlider.value);
    }


    public void VolumeFunction()
    {
        if (!volumeSliderPanel.activeSelf)
        {
            volumeSliderPanel.SetActive(true);

            Controller.instance.GetVolume();
        }
        else
        {
            volumeSliderPanel.SetActive(false);
        }
    }

    // public void SetAudio(int index)
    // {
    //     LanguageToggle.isOn = index == 1;
    // }





}
