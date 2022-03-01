using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThumbnailPanel : MonoBehaviour
{

    public RawImage pic;
    public TMP_Text title;

    public TMP_Text description;


    public bool useFrame = false;

    public int index;


    public void OnEnable()
    {
        VideoData vd = VideoStore._instance.VideoInfo[Controller.instance.SelectedLevel].v_Data[index];

        if (vd.useVideoClip)
        {
            pic.texture = vd.thumbnailClip;
        }
        else
        {
            pic.texture = vd.thumbnailClip;
        }


        title.GetComponent<LocalizedComponent>().EnglishTranslated = vd.Title;
        title.GetComponent<LocalizedComponent>().TamilTranslated = vd.T_Title;


        description.GetComponent<LocalizedComponent>().EnglishTranslated = vd.Description;
        description.GetComponent<LocalizedComponent>().TamilTranslated = vd.T_Description;


    }




}
