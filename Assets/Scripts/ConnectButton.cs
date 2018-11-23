using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectButton : MonoBehaviourPunCallbacks
{
    AllManager TheManager;
    public void Awake()
    {
        TheManager = AllManager.TheManager;
    }
    public void StartConnect()
    {
        TheManager.Send("ConnectClient", TheManager._NetworkManNum);
    }
}