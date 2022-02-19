using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using UnityEngine.SceneManagement;

public class Manager : NetworkManager
{

    public bool Server;

    public GameObject serverObjects;

    public GameObject clientObjects;

    public GameObject videoScreen;

    public static Manager instance;

    public override void Start()
    {
        instance = this;
        base.Start();



        SceneManager.sceneUnloaded += SceneChange;

        if (Server)
        {


            serverObjects.SetActive(true);
            clientObjects.SetActive(false);

            gameObject.GetComponent<CustomDiscovery>().StartServer();
        }
        else
        {


            serverObjects.SetActive(false);
            clientObjects.SetActive(true);

            gameObject.GetComponent<CustomDiscovery>().ConnectClient();
        }



    }

    public void SceneChange(Scene a)
    {
        if (a.buildIndex == 1)

        {
            serverObjects.SetActive(true);
            clientObjects.SetActive(false);
        }
    }


    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

    }
}
