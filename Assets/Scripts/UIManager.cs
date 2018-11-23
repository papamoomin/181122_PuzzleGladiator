using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager
{
    public GameObject ConnectButton = null;
    public GameObject InGameUI = null;
    public Text PlayerHP = null;
    public Text EnemyHP = null;
    public Text Time = null;
    public Text ThisTurnDamage = null;
    public Text ThisTurnDefence = null;
    public Text ThisTurnHealing = null;
    public override void Receive(AllManager.Packet pk)
    {
        switch (pk.methodName)
        {
            case "ToggleConnectButton":
                {
                    ToggleConnectButton(pk.boolList[0]);
                    break;
                }
            case "GameInit":
                {
                    GameInit();
                    break;
                }
            case "SetTimeText":
                {
                    SetTimeText(pk.intList[0]);
                    break;
                }
            case "SetPlayerHPText":
                {
                    SetPlayerHPText(pk.intList[0]);
                    break;
                }
            case "SetEnemyHPText":
                {
                    SetEnemyHPText(pk.intList[0]);
                    break;
                }
            case "SetThisTurnText":
                {
                    SetThisTurnText(pk.intList[0], pk.intList[1], pk.intList[2]);
                    break;
                }
        }
    }

    private void Awake()
    {
        _ManagerID = AllManager.TheManager._UIManNum;
        TheManager = AllManager.TheManager;
    }

    public void SetTimeText(int num)
    {
        Time.text = num.ToString();
    }

    public void SetPlayerHPText(int num)
    {
        PlayerHP.text = num.ToString();
    }

    public void SetEnemyHPText(int num)
    {
        EnemyHP.text = num.ToString();
    }

    public void SetThisTurnText(int dam, int def, int heal)
    {
        ThisTurnDamage.text = dam.ToString();
        ThisTurnDefence.text = def.ToString();
        ThisTurnHealing.text = heal.ToString();
    }

    public void ToggleConnectButton(bool active)
    {
        ConnectButton.SetActive(active);
    }

    public void GameInit()
    {
        print("GameInit");
        InGameUI.SetActive(true);
    }
}
