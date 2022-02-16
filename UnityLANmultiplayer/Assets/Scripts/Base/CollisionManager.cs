using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    public int val = 0;

    public static CollisionManager instance;

    void Start()
    {
        instance = this;
    }

    public void InteractButton()
    {
        SceneManager.LoadScene(val);
    }


    public void ChangePoint(int a)
    {
        val = a;
        Controller.instance.Interactable();
    }
}
