using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;

public class Manager : NetworkManager
{

    public bool Server;

    public GameObject serverObjects;

    public GameObject clientObjects;

    public override void Start()
    {
        base.Start();

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

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

    }
}
