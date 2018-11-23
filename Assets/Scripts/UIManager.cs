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
            case "SetThisTurnDamageText":
                {
                    SetThisTurnDamageText(pk.intList[0]);
                    break;
                }
            case "SetThisTurnDefenceText":
                {
                    SetThisTurnDefenceText(pk.intList[0]);
                    break;
                }
            case "SetThisTurnHealingText":
                {
                    SetThisTurnHealingText(pk.intList[0]);
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

    public void SetThisTurnDamageText(int num)
    {
        ThisTurnDamage.text = num.ToString();
    }


    public void SetThisTurnDefenceText(int num)
    {
        ThisTurnDefence.text = num.ToString();
    }


    public void SetThisTurnHealingText(int num)
    {
        ThisTurnHealing.text = num.ToString();
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
