using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkManager : Manager
{
    string gameVersion = "1";
    public GameObject TheConnectButton = null;

    private void Awake()
    {
        _ManagerID = AllManager.TheManager._NetworkManNum;
        PhotonNetwork.AutomaticallySyncScene = true;
        TheManager = AllManager.TheManager;
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
        switch (pk.methodName)
        {
            case "ConnectClient":
                {
                    ConnectClient();
                    break;
                }
            case "ReceiveEnemyTurnValueRPC":
                {
                    ReceiveEnemyTurnValueRPC(pk.intList[0], pk.intList[1]);
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
        TheManager.Send("ToggleConnectButton", TheManager._UIManNum, null, null, null, new List<bool> { false });

        if (PhotonNetwork.CurrentRoom.PlayerCount.Equals(2))
            SendStartSignal();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("join room failed. Create Room Start");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("Now : " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount.Equals(2))
            SendStartSignal();
    }

    public void SendStartSignal()
    {
        TheManager.Send("Init", TheManager._PuzzleManNum);
        TheManager.Send("Init", TheManager._UserManNum);
        TheManager.Send("GameInit", TheManager._UIManNum);
    }

    [PunRPC]
    public void ReceiveEnemyTurnValue(int hp, int def)
    {
        print("ReceiveEnemyTurnValue"+hp+"/"+def);
        TheManager.Send("ReceiveTurnValue", TheManager._UserManNum, new List<int> { hp, def }, null, null, new List<bool> { false });
    }

    public void ReceiveEnemyTurnValueRPC(int hp, int def)
    {
        print("ReceiveEnemyTurnValueRPC"+hp+"/"+def);
        photonView.RPC("ReceiveEnemyTurnValue", RpcTarget.Others, hp, def);
    }
}