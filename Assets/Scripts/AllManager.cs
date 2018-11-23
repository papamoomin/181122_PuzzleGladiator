using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    public class Packet
    {
        public string methodName;
        public short receiveID;
        public List<int> intList = null;
        public List<string> stringList = null;
        public List<bool> boolList = null;
        public List<float> floatList = null;
    }

    public short _NetworkManNum = 1;
    public short _PuzzleManNum = 2;
    public short _UserManNum = 4;
    public short _UIManNum = 8;

    public GameObject TheNetworkManager = null;
    public GameObject ThePuzzleManager = null;
    public GameObject TheUserManager = null;
    public GameObject TheUIManager = null;
    private List<Manager> managerList = new List<Manager>();

    private void Awake()
    {
        managerList.Add(TheNetworkManager.GetComponent<NetWorkManager>());
        managerList.Add(ThePuzzleManager.GetComponent<PuzzleManager>());
        managerList.Add(TheUserManager.GetComponent<UserManager>());
        managerList.Add(TheUIManager.GetComponent<UIManager>());
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

    public void Send(string methodName, short receiveID, List<int> intList = null, List<string> stringList = null, List<float> floatList = null, List<bool> boolList = null)
    {
        Packet pk = new Packet();
        pk.methodName = methodName;
        pk.receiveID = receiveID;
        if (intList != null)
        {
            pk.intList = new List<int>();
            for (int i = 0; i < intList.Count; ++i)
            {
                pk.intList.Add(intList[i]);
            }
        }
        if (stringList != null)
        {
            pk.stringList = new List<string>();
            for (int i = 0; i < stringList.Count; ++i)
            {
                pk.stringList.Add(stringList[i]);
            }
        }
        if (floatList != null)
        {
            pk.floatList = new List<float>();
            for (int i = 0; i < floatList.Count; ++i)
            {
                pk.floatList.Add(floatList[i]);
            }
        }
        if (boolList != null)
        {
            pk.boolList = new List<bool>();
            for (int i = 0; i < boolList.Count; ++i)
            {
                pk.boolList.Add(boolList[i]);
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