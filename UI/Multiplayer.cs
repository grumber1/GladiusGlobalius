using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Multiplayer : MonoBehaviour
{
    public GameObject MultiplayerGO;
    public GameObject LobbyGO;
    public GameObject ServerGO;
    public TMP_InputField inputFieldPlayerName;
    public TMP_Dropdown hostIpDropdown;
    public TMP_Dropdown joinIpDropdown;
    private List<string> localIps;
    private List<string> joinIps;

    public void Start()
    {
        localIps = getLocalIps();
        insertLocalIpsIntoHostIpDropdown();
        insertLocalIpsIntoJoinIpDropdown();
        //startInsertIpsIntoJoinIpDropdownThread();
    }

    public void onClickHostGame()
    {
        Methods.switchScreen(MultiplayerGO, ServerGO);
        MyTCPServer.localIp = localIps[hostIpDropdown.value];
        startMyTCPServerListenerThread();
    }

    public void onClickJoinGame()
    {
        string ip = joinIps[joinIpDropdown.value];
        MultiplayerManagerClient.remoteIp = ip;

        Guid uniqueID = Guid.NewGuid();
        string newId = uniqueID.ToString();

        string newPlayerName = inputFieldPlayerName.text;

        Player newPlayer = new Player(newId, newPlayerName);
        MultiplayerManagerClient.player = newPlayer;

        MyTCPClient.TCPClient(ip);

        MyTCPClient.sendObjectToServer(
            "MultiplayerManager",
            "connectedPlayers",
            "addNewPlayer",
            newPlayer
        );
        // TODO evtl mehr Daten mitschicken? Ip, etc..
        Methods.switchScreen(MultiplayerGO, LobbyGO);
    }

    public void onClickRefresh()
    {
        insertLocalIpsIntoJoinIpDropdown();
    }

    private void insertLocalIpsIntoJoinIpDropdown()
    {
        joinIps = new List<string>();
        localIps.ForEach(
            (localIp) =>
            {
                List<string> reachableIps = Methods.PingNetwork(localIp);
                reachableIps.ForEach(
                    (reachableIp) =>
                    {
                        joinIps.Add(reachableIp);
                    }
                );
            }
        );
        joinIpDropdown.ClearOptions();
        joinIpDropdown.AddOptions(joinIps);
    }

    private void insertLocalIpsIntoHostIpDropdown()
    {
        hostIpDropdown.ClearOptions();
        hostIpDropdown.AddOptions(localIps);
    }

    private List<string> getLocalIps()
    {
        List<string> localIps = new List<string>();
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress iPAddress in host.AddressList)
        {
            if (AddressFamily.InterNetwork == iPAddress.AddressFamily)
            {
                localIps.Add(iPAddress.ToString());
            }
        }
        return localIps;
    }

    private void CallMyTCPServerListenerStart()
    {
        MyTCPServer.listenerStart();
    }

    private void startMyTCPServerListenerThread()
    {
        ThreadStart MyTCPServerListenerStartRef = new ThreadStart(CallMyTCPServerListenerStart);
        Thread listenerThread = new Thread(MyTCPServerListenerStartRef);
        MyTCPServer.listenerThread = listenerThread;
        MyTCPServer.listenerThread.Start();
    }
}
