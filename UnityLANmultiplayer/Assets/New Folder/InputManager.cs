using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
public class InputManager : NetworkBehaviour
{
    [SyncVar]
    public float hor = 0;

    [SyncVar]
    public float ver = 0;

    public static float x_Axis;

    public static float y_Axis = 0;

    public bool controller;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (controller)
        {
            SceneManager.LoadScene(1);
        }
    }

    void Update()
    {
        if (hor != x_Axis)
        {
            hor = x_Axis;
        }

        if (ver != y_Axis)
        {
            ver = y_Axis;
        }
    }



}
