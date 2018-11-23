using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager
{
    public GameObject ConnectButton = null;
    public GameObject InGameUI = null;
    public override void Receive(AllManager.Packet pk)
    {
        switch (pk.methodName)
        {
            case "ToggleConnectButton":
                {
                    ToggleConnectButton(pk.boolList[0]);
                    break;
                }
        }
    }

    private void Awake()
    {
        _ManagerID = AllManager.TheManager._UIManNum;
        TheManager = AllManager.TheManager;
    }

    public void ToggleConnectButton(bool active)
    {
        ConnectButton.SetActive(active);
    }

    public void GameInit()
    {
        InGameUI.SetActive(true);
    }
}
