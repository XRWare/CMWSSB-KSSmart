using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Button PlayButton;



    void Start()
    {
        instance = this;
        LoadFirstScreen();
    }


    public void LoadFirstScreen()
    {
        StartScreen.SetActive(true);
        commonScene.SetActive(false);
        VideoPlayerScene.SetActive(false);
    }

    public void LoadSecondScreen()
    {
        StartScreen.SetActive(false);
        commonScene.SetActive(true);
        VideoPlayerScene.SetActive(false);


        ControllerScreen.SetActive(true);
        InteractScene.SetActive(false);
        interactButton.gameObject.SetActive(false);

        Controller.instance.joystick = charController;
    }


    public void LoadThirdScreen()
    {
        StartScreen.SetActive(false);
        commonScene.SetActive(true);
        VideoPlayerScene.SetActive(false);

        ControllerScreen.SetActive(false);
        InteractScene.SetActive(true);


        Controller.instance.joystick = camController;
    }

    public void LoadVideoPlayer()
    {
        StartScreen.SetActive(false);
        commonScene.SetActive(false);
        VideoPlayerScene.SetActive(true);
    }


    public void SetInteractable()
    {
        interactButton.gameObject.SetActive(true);
    }


    public void Interact()
    {
        Controller.instance.Interact();
    }
}
