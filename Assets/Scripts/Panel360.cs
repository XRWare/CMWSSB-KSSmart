using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Panel360 : MonoBehaviour
{
    public TMP_Text title;

    public int index;


    public void OnEnable()
    {
        if (index < VideoStore._instance.VideoInfo[Controller.instance.SelectedLevel].v_360Data._360Clip.Length)
        {
            Video360Data vd = VideoStore._instance.VideoInfo[Controller.instance.SelectedLevel].v_360Data;
            title.GetComponent<LocalizedComponent>().EnglishTranslated = vd._buttonTitle[index];
            title.GetComponent<LocalizedComponent>().TamilTranslated = vd._TbuttonTitle[index];

            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {

            title.GetComponent<LocalizedComponent>().EnglishTranslated = "";
            title.GetComponent<LocalizedComponent>().TamilTranslated = "";
            gameObject.GetComponent<Button>().interactable = false;
        }

    }

}
