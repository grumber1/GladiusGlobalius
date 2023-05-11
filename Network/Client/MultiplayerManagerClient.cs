using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using TMPro;

public static class MultiplayerManagerClient
{
    public static TcpClient clientToServerClient;
    public static Stream clientToServerStream;
    public static List<Player> connectedPlayers = new List<Player>();
    public static Player player;
    public static bool isServer = false;
    public static bool startGameButton = false;
    public static string remoteIp;
}
