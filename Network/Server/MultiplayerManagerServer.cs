using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Net.Sockets;

public static class MultiplayerManagerServer
{
    public static bool startGameButton = false;
    public static List<Player> connectedPlayers = new List<Player>();
    public static List<TcpClient> clients = new List<TcpClient>();
    public static List<Stream> streams = new List<Stream>();
}
