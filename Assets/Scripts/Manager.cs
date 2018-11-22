using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviourPunCallbacks
{

    protected int _ManagerID = 0;
    public int ManagerID
    {
        get
        {
            return _ManagerID;
        }
    }
    public abstract void Receive(AllManager.Packet pk);
}