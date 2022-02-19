using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LocalizedComponent : MonoBehaviour
{

    public string EnglishTranslated;

    public string TamilTranslated;

    private void Awake()
    {
        //LocalizationManager.instance.AddLComponent(this);
    }

    void OnEnable()
    {
        LocalizationManager.instance.UpdateLanguage();
    }


    //event for SetLanguage
    public virtual void SetLanguage(Languages language)
    {

        if (language == Languages.ENGLISH)
        {
            gameObject.GetComponent<TMP_Text>().text = EnglishTranslated;
        }
        else
        {
            var a = gameObject.GetComponent<CharReplacerTamil>();

            a._Text = TamilTranslated;

            a.UpdateMe();

            gameObject.GetComponent<TMP_Text>().text = a._Text;

        }



    }
}
