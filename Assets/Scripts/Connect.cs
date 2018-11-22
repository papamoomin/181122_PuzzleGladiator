using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Connect : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    private void Awake()
    {
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



    public override void OnConnectedToMaster()
    {
        print("connect finish");
        PhotonNetwork.JoinRandomRoom(null, 2);
        print("make or join room start");
    }

    public override void OnCreatedRoom()
    {
        print("Create room finish");
    }

    public override void OnJoinedRoom()
    {
        print("make or join room finish");
        print("now Player : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("make or join room failed. Create Room Start");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }
}
