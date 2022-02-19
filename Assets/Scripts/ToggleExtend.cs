using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleExtend : Toggle
{


    public GameObject OptionA;
    public GameObject OptionB;

    protected override void Start()
    {
        base.Start();

        OptionA = transform.GetChild(0).gameObject;
        OptionB = transform.GetChild(1).gameObject;


        // OptionA.SetActive(isOn);
        // OptionB.SetActive(!isOn);
    }


    public new void OnEnable()
    {
        OptionA.SetActive(isOn);
        OptionB.SetActive(!isOn);
    }



    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        OptionA.SetActive(isOn);
        OptionB.SetActive(!isOn);
    }


    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);

        OptionA.SetActive(!isOn);
        OptionB.SetActive(isOn);
    }

}
