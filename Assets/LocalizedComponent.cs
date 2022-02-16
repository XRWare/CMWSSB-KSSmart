using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LocalizedComponent : MonoBehaviour
{
    private void Awake()
    {
        //LocalizationManager.instance.AddLComponent(this);
    }


    //event for SetLanguage
    public virtual void SetLanguage(Languages language)
    {

    }
}
