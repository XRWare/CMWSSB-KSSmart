using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{

    InputManager input;
    void OnEnable()
    {
        input = FindObjectOfType<InputManager>();
    }



    public void Direction(float x)
    {
        input.hor = x;

    }

    public void Direction1(float y)
    {
        input.ver = y;
    }

}
