using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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



    void Start()
    {
        instance = this;
    }


    public void Play()
    {
        Controller.instance.PlayPause(true);
        playerPlaButton.gameObject.SetActive(false);
        playerPauseButton.gameObject.SetActive(true);
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



    public void SelectVideo(int a)
    {
        playbutton.gameObject.SetActive(true);
        Controller.instance.SelectVideo(a);
        playerPlaButton.gameObject.SetActive(true);
        playerPauseButton.gameObject.SetActive(false);
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

}
