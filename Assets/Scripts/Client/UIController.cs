using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class UIController : MonoBehaviour
{

    public static UIController instance;


    public GameObject StartScreen;

    public GameObject ControllerScreen;

    public GameObject InteractScene;

    public GameObject VideoPlayerScene;


    public GameObject commonScene;

    public FixedJoystick charController;
    public FixedJoystick camController;

    public Button interactButton;

    public GameObject selectionPanel;

    public GameObject Panel;

    public Toggle languageToggle;

    public Toggle languageToggleVid;


    public TMP_Text text;

    public GameObject startButton;

    public TMP_Text title;



    void Start()
    {
        instance = this;
        LoadFirstScreen();
        //ChangeLanguage();
        startButton.gameObject.SetActive(false);
        LocalizationManager.instance.Init(Languages.ENGLISH);
    }


    public void LoadFirstScreen()
    {


        gameObject.GetComponent<AnimationController>().Fade(1f, () =>
        {
            StartScreen.SetActive(true);
            commonScene.SetActive(false);
            VideoPlayerScene.SetActive(false);
        });



    }

    public void LoadSecondScreen()
    {

        gameObject.GetComponent<AnimationController>().Fade(1f, () =>
        {
            StartScreen.SetActive(false);
            commonScene.SetActive(true);
            VideoPlayerScene.SetActive(false);

            selectionPanel.SetActive(false);

            ControllerScreen.SetActive(true);
            InteractScene.SetActive(false);
            interactButton.gameObject.SetActive(false);
            title.GetComponent<LocalizedComponent>().EnglishTranslated = "Select an option";
            title.GetComponent<LocalizedComponent>().TamilTranslated = "தேர்ந்தெடுக்கவும்";
            title.GetComponent<LocalizedComponent>().SetLanguage(languageToggle.isOn ? Languages.TAMIL : Languages.ENGLISH);
        });



        Controller.instance.joystick = charController;
    }


    public void LoadThirdScreen()
    {

        gameObject.GetComponent<AnimationController>().Fade(1f, () =>
         {
             StartScreen.SetActive(false);
             commonScene.SetActive(true);
             VideoPlayerScene.SetActive(false);

             title.GetComponent<LocalizedComponent>().EnglishTranslated = VideoStore._instance.VideoInfo[Controller.instance.SelectedLevel].titleEnglish;
             title.GetComponent<LocalizedComponent>().TamilTranslated = VideoStore._instance.VideoInfo[Controller.instance.SelectedLevel].titleTamil;
             title.GetComponent<LocalizedComponent>().SetLanguage(languageToggle.isOn ? Languages.TAMIL : Languages.ENGLISH);

             ControllerScreen.SetActive(false);
             InteractScene.SetActive(true);
         });



        Controller.instance.joystick = camController;
    }

    public void LoadVideoPlayer()
    {

        gameObject.GetComponent<AnimationController>().Fade(.5f, () =>
        {
            StartScreen.SetActive(false);
            commonScene.SetActive(false);
            VideoPlayerScene.SetActive(true);
            languageToggleVid.isOn = Controller.instance.language == 1;
        }, () => { VideoController.instance.Play(); });



    }


    public void SetInteractable(bool val = true)
    {
        interactButton.gameObject.SetActive(val);
    }


    public void Interact()
    {
        Controller.instance.Interact();
    }


    public void ChangeLanguage()
    {
        languageToggleVid.isOn = languageToggle.isOn;
        LocalizationManager.instance.SetLanguage(languageToggle.isOn ? Languages.TAMIL : Languages.ENGLISH);
        Controller.instance.language = languageToggle.isOn ? 1 : 0;

        Controller.instance.UpdateLanguage(languageToggleVid.isOn ? 1 : 0, false);
    }

    public void UpdateLanguage(bool play)
    {
        //if (languageToggle.isOn != languageToggleVid.isOn)
        {

            languageToggle.isOn = languageToggleVid.isOn;
            var a = languageToggleVid.isOn ? 1 : 0;

            VideoPlayer vp = VideoController.instance.player;

            var temp = vp.isPlaying;

            VideoController.instance.Pause();
            var _temp = vp.frame;

            VideoController.instance.currentVideo = VideoStore._instance.VideoInfo[Controller.instance.SelectedLevel].v_Data[Controller.instance.index].clip[a];
            vp.clip = VideoController.instance.currentVideo;



            // AudioUpdated = true;



            languageToggleVid.isOn = languageToggle.isOn;
            LocalizationManager.instance.SetLanguage(languageToggle.isOn ? Languages.TAMIL : Languages.ENGLISH);
            Controller.instance.language = a;

            Controller.instance.UpdateLanguage(a, true);

            if (play)
            {
                vp.Prepare();
                vp.frame = _temp;
                Debug.Log("u lang");

                VideoController.instance.Pause();
                VideoController.instance.SetVideoVal(true);

                VideoController.instance.Play();
            }

        }


    }



    public void Back(int a)
    {


        if (a == 1)
        {
            LoadFirstScreen();
            Controller.instance.BackButton(a);
        }
        else if (a == 2)
        {
            if (selectionPanel.activeInHierarchy)
            {
                selectionPanel.GetComponent<AnimationController>().MoveDown();
                Panel.gameObject.SetActive(true);
            }
            else
            {
                VideoController.instance.playbutton.gameObject.SetActive(false);
                LoadSecondScreen();
                Controller.instance.BackButton(a);
            }

        }
        else if (a == 3)
        {
            LoadThirdScreen();
            Controller.instance.BackButton(a);
            VideoController.instance.playbutton.gameObject.SetActive(false);
        }
    }


    public void OnStart()
    {

        LoadSecondScreen();
        Controller.instance.StartFunction();
    }

    void Update()
    {
        if (StartScreen.activeInHierarchy)
        {
            if (Controller.instance)
            {
                text.text = "Connected";

                startButton.gameObject.SetActive(true);
            }
            else
            {

                text.text = "Waiting for Host";

                startButton.gameObject.SetActive(false);
            }
        }

    }


    public void SkyBoxVideo(int a)
    {
        Controller.instance.SkyBoxval(a);
    }

}
