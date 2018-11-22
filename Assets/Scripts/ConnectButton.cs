using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectButton : MonoBehaviourPunCallbacks
{
    AllManager TheManager;
    public void Start()
    {
        TheManager = AllManager.TheManager;
    }
    public void StartConnect()
    {
        TheManager.Send("ConnectClient", TheManager._NetworkManNum, null);
    }
}
