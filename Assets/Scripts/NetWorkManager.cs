using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkManager : Manager
{
    string gameVersion = "1";
    private void Awake()
    {
        _ManagerID = AllManager.TheManager._NetworkManNum;
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void ConnectClient()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            print("connect start");
        }
    }
    public override void Receive(AllManager.Packet pk)
    {
        switch(pk.methodName)
        {
            case "ConnectClient":
                {
                    ConnectClient();
                    break;
                }
        }
    }
    public override void OnConnectedToMaster()
    {
        print("connect finish");
        PhotonNetwork.JoinRandomRoom(null, 2);
        print("join room start");
    }

    public override void OnCreatedRoom()
    {
        print("Create room finish");
    }

    public override void OnJoinedRoom()
    {
        print("join room finish");
        print("now Player : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("join room failed. Create Room Start");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }
}
