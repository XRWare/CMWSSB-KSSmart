using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
public class CollisionManager : MonoBehaviour
{
    public int val = 0;

    public int skyVal = 0;
    public Material mat;

    public static CollisionManager instance;

    public VideoClip[] clip;

    public VideoPlayer player;

    public GameObject BeginScreen;
    public TMP_Text text;

    public AnimationController anim;

    void Start()
    {
        instance = this;
        gameObject.GetComponent<AnimationController>().FadeFun();
    }

    public void InteractButton()
    {
        anim.Fade(1, () =>
        {
            RenderSettings.skybox = mat;
            player.Pause();
            player.clip = VideoStore._instance.VideoInfo[val].v_360Data._360Clip[0];
            player.Play();
            GameObject.FindObjectOfType<Manager>().videoScreen.SetActive(true);
        });

    }

    public void SkyBoxVideo(int a)
    {
        skyVal = a;
        player.Pause();
        player.clip = VideoStore._instance.VideoInfo[val].v_360Data._360Clip[skyVal];
        player.Play();
    }


    public void ChangePoint(int a)
    {
        val = a;
        Controller.instance.SelectedLevel = a;
        Controller.instance.Interactable(a != -1, a);
    }


    public void OnBack(int a)
    {
        if (a == 1)
        {
            anim.Fade(1, () =>
            {
                CollisionManager.instance.BeginScreen.SetActive(true);
            });
            //CollisionManager.instance.BeginScreen.SetActive(true);
        }
    }

    public void OnClickStart()
    {
        anim.Fade(1, () =>
        {
            BeginScreen.SetActive(false);
        });

    }

    void Update()
    {
        if (BeginScreen.activeInHierarchy)
        {
            if (Controller.instance)
            {
                text.text = "Controller Connected";


            }
            else
            {

                text.text = "Server started \n Waiting for controller";


            }
        }

    }

}
