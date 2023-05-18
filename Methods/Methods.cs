using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;

public static class Methods
{
    public static void switchScreen(GameObject from, GameObject to)
    {
        from.SetActive(false);
        to.SetActive(true);
    }

    public static bool pingHost(string ip)
    {
        System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();

        //PingOptions options = new PingOptions ();
        IPAddress iPAddress = IPAddress.Parse(ip);

        // Use the default Ttl value which is 128,
        // but change the fragmentation behavior.
        //options.DontFragment = true;

        // Create a buffer of 32 bytes of data to be transmitted.
        // string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        // byte[] buffer = Encoding.ASCII.GetBytes (data);
        int timeout = DevSettings.pingTimeout;
        try
        {
            PingReply reply = pingSender.Send(iPAddress, timeout);
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    public static long pingHostTime(string ip)
    {
        System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();

        //PingOptions options = new PingOptions ();
        IPAddress iPAddress = IPAddress.Parse(ip);

        // Use the default Ttl value which is 128,
        // but change the fragmentation behavior.
        //options.DontFragment = true;

        // Create a buffer of 32 bytes of data to be transmitted.
        // string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        // byte[] buffer = Encoding.ASCII.GetBytes (data);
        int timeout = DevSettings.pingTimeout;
        PingReply reply = pingSender.Send(iPAddress, timeout);
        long pingTime = reply.RoundtripTime;

        return pingTime;
    }

    public static List<string> PingNetwork(string ip)
    {
        List<string> reachableIps = new List<string>();
        string[] ipSplit = ip.Split(".");
        string gatewayIp = ipSplit[0] + "." + ipSplit[1] + "." + ipSplit[2] + ".";

        for (int i = 2; i < 255; i++)
        {
            string ipToPing = gatewayIp + i;
            if (pingHost(ipToPing))
            {
                reachableIps.Add(ipToPing);
            }
            ;
        }
        return reachableIps;
    }

    public static int randomNumber(int start, int end)
    {
        System.Random rnd = new System.Random();
        int randomNumber = rnd.Next(start, end + 1);

        return randomNumber;
    }

    public static string SerializeObject<T>(T objectToSerialize)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, objectToSerialize);
            return writer.ToString();
        }
    }

    public static T DeserializeObject<T>(string xmlString)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StringReader reader = new StringReader(xmlString))
        {
            return (T)serializer.Deserialize(reader);
        }
    }

    public static (
        string classToCall,
        string objectToCall,
        string method,
        string value
    ) resolveMessage(string classObjectMethodValue)
    {
        string classToCall = classObjectMethodValue.Split("::::::")[0];
        string objectMethodValue = classObjectMethodValue.Split("::::::")[1];

        string objectToCall = objectMethodValue.Split(":::::")[0];
        string methodValue = objectMethodValue.Split(":::::")[1];

        string method = methodValue.Split("::::")[0];
        string value = methodValue.Split("::::")[1];

        return (classToCall: classToCall, objectToCall: objectToCall, method: method, value: value);
    }

    public static Player getPlayerByIdClient(string playerId)
    {
        Player player = new Player();
        MultiplayerManagerClient.connectedPlayers.ForEach(connectedPlayer =>
        {
            if (connectedPlayer.id == playerId)
            {
                player = connectedPlayer;
            }
        });

        return player;
    }

    public static int getPlayerIndexByPlayerIdServer(string playerId)
    {
        int i = 0;
        int index = 0;
        Player player = new Player();
        MultiplayerManagerServer.connectedPlayers.ForEach(connectedPlayer =>
        {
            if (connectedPlayer.id == playerId)
            {
                index = i;
            }
            i++;
        });

        return index;
    }

    public static Gladiator createNewGladiator(
        string newName,
        double newBaseHealth,
        double newBaseAttributes,
        Weapon newLeftArmWeapon,
        Weapon newRightArmWeapon
    )
    {
        Guid uniqueID = Guid.NewGuid();
        string newId = uniqueID.ToString();

        GladiatorBody newGladiatorBody = new GladiatorBody(newBaseHealth);

        GladiatorAttributes newGladiatorAttributes = new GladiatorAttributes(newBaseAttributes);

        GladiatorWeapons newGladiatorWeapons = new GladiatorWeapons(
            newLeftArmWeapon,
            newRightArmWeapon
        );

        Gladiator newGladiator = new Gladiator(
            newId,
            newName,
            newGladiatorBody,
            newGladiatorAttributes,
            newGladiatorWeapons,
            "slaveTrader"
        );

        return newGladiator;
    }
}
