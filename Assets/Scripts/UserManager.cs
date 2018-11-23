using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Manager
{
    public int MaxHP = 30;
    public int NowHP;
    public int EnemyHP;
    public int nowTurnHeal = 0;
    public int nowTurnDamage = 0;
    public int nowTurnDef = 0;
    public int nowTurnEnemyHeal = 0;
    public int nowTurnEnemyDamage = 0;
    public int nowTurnEnemyDef = 0;
    public bool isTurnPlayerCheck = false;
    public bool isTurnEnemyCheck = false;

    private void Awake()
    {
        _ManagerID = AllManager.TheManager._UserManNum;
        TheManager = AllManager.TheManager;
    }

    public void SetPlayerHPText(int num)
    {
        TheManager.Send("SetPlayerHPText", TheManager._UIManNum, new List<int> { num });
    }

    public void SetEnemyHPText(int num)
    {
        TheManager.Send("SetEnemyHPText", TheManager._UIManNum, new List<int> { num });
    }

    public void SetThisTurnDamageText(int num)
    {
        TheManager.Send("SetThisTurnDamageText", TheManager._UIManNum, new List<int> { num });
    }

    public void SetThisTurnDefenceText(int num)
    {
        TheManager.Send("SetThisTurnDefenceText", TheManager._UIManNum, new List<int> { num });
    }

    public void SetThisTurnHealingText(int num)
    {
        TheManager.Send("SetThisTurnHealingText", TheManager._UIManNum, new List<int> { num });
    }

    public override void Receive(AllManager.Packet pk)
    {
        switch (pk.methodName)
        {
            case "Init":
                {
                    Init();
                    break;
                }
            case "ReceiveTurnValue":
                {
                    ReceiveTurnValue(pk.intList[0], pk.intList[1], pk.boolList[0]);
                    break;
                }
        }
    }

    public void ReceiveTurnValue(int hp, int def, bool player)
    {
        if (player)
        {
            isTurnPlayerCheck = true;
            if (hp > 0)
            {
                nowTurnHeal = hp;
                nowTurnDamage = 0;
            }
            else if (hp < 0)
            {
                nowTurnHeal = 0;
                nowTurnDamage = hp;
            }
            nowTurnDef = def;
            if (isTurnEnemyCheck)
            {
                ProgressingTurn();
            }
        }
        else
        {
            isTurnEnemyCheck = true;
            if (hp > 0)
            {
                nowTurnEnemyHeal = hp;
                nowTurnEnemyDamage = 0;
            }
            else if (hp < 0)
            {
                nowTurnEnemyHeal = 0;
                nowTurnEnemyDamage = hp;
            }
            nowTurnEnemyDef = def;
            if (isTurnPlayerCheck)
            {
                ProgressingTurn();
            }
        }
    }

    public void ProgressingTurn()
    {
        print("ProgressingTurn");
        int t = nowTurnDamage + nowTurnEnemyDef;
        if (t < 0)
            DamagingEnemyHP(t);
        t = nowTurnEnemyDamage + nowTurnDef;
        if (t < 0)
            DamagingHP(t);
        if (nowTurnEnemyHeal > 0)
            HealEnemyHP(nowTurnEnemyHeal);
        if (nowTurnHeal > 0)
            HealHP(nowTurnHeal);
        isTurnEnemyCheck = false;
        isTurnPlayerCheck = false;
        SetPlayerHPText(NowHP);
        SetEnemyHPText(EnemyHP);
        TheManager.Send("InitOnlyTurn", TheManager._PuzzleManNum);
        InitTurn();
    }

    public void Init()
    {
        NowHP = MaxHP;
        EnemyHP = MaxHP;
        InitTurn();
    }

    public void InitTurn()
    {
        nowTurnHeal = 0;
        nowTurnDamage = 0;
        nowTurnDef = 0;
        nowTurnEnemyHeal = 0;
        nowTurnEnemyDamage = 0;
        nowTurnEnemyDef = 0;
        SetThisTurnDamageText(0);
        SetThisTurnDefenceText(0);
        SetThisTurnHealingText(0);
    }

    public void HealHP(int value)
    {
        NowHP += value;
        if (NowHP > MaxHP)
            NowHP = MaxHP;
    }

    public void HealEnemyHP(int value)
    {
        EnemyHP += value;
        if (EnemyHP > MaxHP)
            EnemyHP = MaxHP;
    }

    public void DamagingHP(int value)
    {
        NowHP += value;
        if (NowHP <= 0)
            Dead(true);
    }

    public void DamagingEnemyHP(int value)
    {
        EnemyHP += value;
        if (EnemyHP <= 0)
            Dead(false);
    }

    public void Dead(bool player)
    {

    }
}