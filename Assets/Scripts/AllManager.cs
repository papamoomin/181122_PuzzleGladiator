using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    public class Packet
    {
        public string methodName;
        public int receiveID;
        public object[] arguments;
    }

    public int _NetworkManNum = 0x00000001;
    public GameObject TheNetworkManager = null;
    private List<Manager> managerList = new List<Manager>();

    private void Awake()
    {
        managerList.Add(TheNetworkManager.GetComponent<NetWorkManager>());
    }

    static private AllManager _TheManager = null;
    static public AllManager TheManager
    {
        get
        {
            if (_TheManager == null)
            {
                _TheManager = FindObjectOfType(typeof(AllManager)) as AllManager;
            }
            return _TheManager;
        }
    }

    public void Send(string methodName, int receiveID, object[] arguments)
    {
        Packet pk = new Packet();
        pk.methodName = methodName;
        pk.receiveID = receiveID;
        if (arguments != null)
        {
            pk.arguments = new object[arguments.Length];
            for (int i = 0; i < arguments.Length; ++i)
            {
                pk.arguments[i] = arguments[i];
            }
        }
        Send(pk);
    }

    public void Send(Packet pk)
    {
        Dispatch(pk);
    }

    private void Dispatch(Packet pk)
    {
        for (int i = 0; i < managerList.Count; ++i)
        {
            int bReseive = managerList[i].ManagerID & pk.receiveID;
            if (bReseive > 0)
            {
                managerList[i].Receive(pk);
            }
        }
    }
}
