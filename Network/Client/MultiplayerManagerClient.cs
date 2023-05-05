using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using TMPro;

public static class MultiplayerManagerClient
{
    public static Stream stream;
    public static int id;
    public static string name;
    public static List<Player> connectedPlayers = new List<Player>();
    public static bool isServer = false;
    public static bool startGameButton = false;
    public static string remoteIp;
}
