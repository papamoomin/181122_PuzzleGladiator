using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviourPunCallbacks
{
    public AllManager TheManager;
    protected short _ManagerID = 0;
    public short ManagerID
    {
        get
        {
            return _ManagerID;
        }
    }
    public abstract void Receive(AllManager.Packet pk);
}