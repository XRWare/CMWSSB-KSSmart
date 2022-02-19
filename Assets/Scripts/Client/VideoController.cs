using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    void Start()
    {
        instance = this;

        // SetAudio(Controller.instance.language);
        //LocalizationManager.instance.Init(Languages.TAMIL);
    }



    public void Play()
    {
        volumeSliderPanel.SetActive(false);
        Controller.instance.PlayPause(true);
        playerPlaButton.gameObject.SetActive(false);
        playerPauseButton.gameObject.SetActive(true);
        slider.value = 0;
        UIController.instance.UpdateLanguage();
        // LocalizationManager.instance.SetLanguage(Controller.instance.language == 1 ? Languages.TAMIL : Languages.ENGLISH);
    }

    public void Pause()
    {
        Controller.instance.PlayPause(false);
        playerPlaButton.gameObject.SetActive(true);
        playerPauseButton.gameObject.SetActive(false);
    }


    public void SetSlider(float val, float totalVal)
    {
        slider.value = val / totalVal;

        timer.text = Mathf.Floor((int)val / 60).ToString("00") + " : " + ((int)val % 60).ToString("00") + " / " + Mathf.Floor((int)totalVal / 60).ToString("00") + " : " + ((int)totalVal % 60).ToString("00");
    }


    public void SetVideoVal()
    {

        Controller.instance.SetVideoTime(slider.value);

    }


    public void SelVid(GameObject g)
    {
        currentPanelObject = g.GetComponent<PanelObjects>();

        title.EnglishTranslated = currentPanelObject.title.EnglishTranslated;
        title.TamilTranslated = currentPanelObject.title.TamilTranslated;

        description.EnglishTranslated = currentPanelObject.description.EnglishTranslated;

        description.TamilTranslated = currentPanelObject.description.TamilTranslated;

    }
    public void SelectVideo(int a)
    {
        playbutton.gameObject.SetActive(true);
        Controller.instance.SelectVideo(a);
        playerPlaButton.gameObject.SetActive(true);
        playerPauseButton.gameObject.SetActive(false);


        UIController.instance.UpdateLanguage();
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
