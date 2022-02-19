﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Mirror.Discovery
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkDiscoveryHUD")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-discovery")]
    [RequireComponent(typeof(NetworkDiscovery))]
    public class CustomDiscovery : MonoBehaviour
    {
        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        Vector2 scrollViewPos = Vector2.zero;

        public NetworkDiscovery networkDiscovery;




        void Start()
        {

        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GetComponent<NetworkDiscovery>();
                UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
            }
        }
#endif

        void OnGUI()
        {
            if (NetworkManager.singleton == null)
                return;

            // if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
            //     DrawGUI();

            // if (NetworkServer.active || NetworkClient.active)
            //     StopButtons();
        }

        void DrawGUI()
        {
            GUILayout.BeginArea(new Rect(100, 100, 1000, 1000));
            GUILayout.BeginHorizontal();

            // if (GUILayout.Button("Find Servers"))
            // {
            //     discoveredServers.Clear();
            //     networkDiscovery.StartDiscovery();
            // }

            // LAN Host
            // if (GUILayout.Button("Start Host"))
            // {
            //     StartServer();
            // }

            // Dedicated server
            // if (GUILayout.Button("Start Server"))
            // {
            //     discoveredServers.Clear();
            //     NetworkManager.singleton.StartServer();
            //     networkDiscovery.AdvertiseServer();
            // }

            GUILayout.EndHorizontal();

            // show list of found server

            GUILayout.Label($"Discovered Servers [{discoveredServers.Count}]:");

            // servers
            scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);

            foreach (ServerResponse info in discoveredServers.Values)
                if (GUILayout.Button(info.EndPoint.Address.ToString()))
                    Connect(info);

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        void StopButtons()
        {
            GUILayout.BeginArea(new Rect(100, 100, 1000, 1000));

            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {


                if (GUILayout.Button("Stop Host"))
                {

                }
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Client"))
                {

                }
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop Server"))
                {
                }
            }

            GUILayout.EndArea();
        }

        void Connect(ServerResponse info)
        {
            networkDiscovery.StopDiscovery();
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }


        public void StartServer()
        {
            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
        }


        public void ConnectClient()
        {
            StartCoroutine(SearchServer());
        }

        IEnumerator SearchServer()
        {
            while (discoveredServers.Count == 0)
            {
                discoveredServers.Clear();
                networkDiscovery.StartDiscovery();

                yield return new WaitForSeconds(.5f);
            }

            foreach (ServerResponse info in discoveredServers.Values)
            {
                Connect(info);
                break;

            }


            yield return null;
        }

    }
}
