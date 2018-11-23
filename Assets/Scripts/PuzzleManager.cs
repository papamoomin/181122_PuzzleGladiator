using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleManager : Manager
{
    public const float maxTurnTime = 15f;
    public float turnTime;
    public int intTurnTime;
    public bool isTurn = true;
    public bool isTileClick = false;
    public int clickedTileNum; // 0 = sword, 1 = shield, 2 = potion
    public Sprite[] TileSprites = new Sprite[3];
    public GameObject TilePrefab = null;
    public GameObject[] puzzleSprite = new GameObject[36];
    public List<GameObject> spriteList = new List<GameObject>();
    public List<Tile> clickedTile = new List<Tile>();
    public int lastClick = -1;

    public short[] puzzleMap = new short[36];

    public override void Receive(AllManager.Packet pk)
    {
        switch (pk.methodName)
        {
            case "Init":
                {
                    Init();
                    break;
                }
            case "InitOnlyTurn":
                {
                    InitOnlyTurn();
                    break;
                }
        }
    }

    public void InitOnlyTurn()
    {
        turnTime = maxTurnTime;
        intTurnTime = (int)maxTurnTime;
        SetTimeTextSet(intTurnTime);
        isTurn = true;
        isTileClick = false;
        lastClick = -1;
        clickedTile.Clear();
    }

    public void SetTimeTextSet(int num)
    {
        TheManager.Send("SetTimeText", TheManager._UIManNum, new List<int> { num });
    }

    private void Update()
    {
        if (isTurn)
        {
            turnTime -= Time.deltaTime;
            if (intTurnTime != ((int)turnTime + 1))
            {
                intTurnTime = (int)turnTime + 1;
                SetTimeTextSet(intTurnTime);
            }

            if (turnTime < 0)
            {
                isTurn = false;
                checkTurn(0, 1);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isTileClick)
                {
                    isTileClick = false;
                    isTurn = false;
                    int count = clickedTile.Count;
                    for (int i = 0; i < count; ++i)
                    {
                        clickedTile[i].ResetTile();
                    }
                    checkTurn(clickedTileNum, count);
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                if(isTileClick)
                {
                    for(int i = 0; i < clickedTile.Count;++i)
                    {
                        clickedTile[i].ResetTileScale();
                    }
                    clickedTile.Clear();
                    isTileClick = false;
                }
            }
        }
    }

    private void Awake()
    {
        _ManagerID = AllManager.TheManager._PuzzleManNum;
        TheManager = AllManager.TheManager;
    }

    public void checkTurn(int tileNum, int count)
    {
        switch (tileNum)
        {
            case 0:
                {
                    UseSword(count);
                    break;
                }
            case 1:
                {
                    UseShield(count);
                    break;
                }
            case 2:
                {
                    UsePotion(count);
                    break;
                }
        }
    }

    public void UseSword(int count)
    {
        SendMyPlayerVariation(-((count - 1) / 2), 0, true);
    }

    public void UseShield(int count)
    {
        SendMyPlayerVariation(0, (count - 1) / 2, true);
    }

    public void UsePotion(int count)
    {
        SendMyPlayerVariation((count - 1) / 2, 0, true);
    }

    public void SendMyPlayerVariation(int hp, int defence, bool player)
    {
        TheManager.Send("ReceiveEnemyTurnValueRPC", TheManager._NetworkManNum, new List<int> { hp, defence });
        TheManager.Send("ReceiveTurnValue", TheManager._UserManNum, new List<int> { hp, defence }, null, null, new List<bool> { player });
    }

    public void SetSpritePooling()
    {
        for (int i = 0; i < puzzleSprite.Length; ++i)
        {
            puzzleSprite[i] = Instantiate(TilePrefab, TilePrefab.transform.position, Quaternion.identity, transform);
            spriteList.Add(puzzleSprite[i]);
        }
    }

    public void Init()
    {
        SetSpritePooling();
        PuzzleMapInit();
        InitOnlyTurn();
    }

    public void PuzzleMapInit()
    {
        for (int i = 0; i < puzzleMap.Length; ++i)
        {
            Tile t = spriteList[i].GetComponent<Tile>();
            t.ThePuzzleManager = this;
            SetPuzzleTile(i, t);
            SetInitialTilePosition(i);
        }
    }

    public void SetPuzzleTile(int i, Tile t)
    {
        puzzleMap[i] = (short)Random.Range(0, 3);
        SetInitialTileSprite(i, puzzleMap[i]);
        SetInitialTileInit(i, t);
    }

    public void SetInitialTileInit(int i, Tile t)
    {
        t.tileNum = puzzleMap[i];
        t.index = i;
    }

    public void SetInitialTileSprite(int index, short num)
    {
        spriteList[index].GetComponent<SpriteRenderer>().sprite = TileSprites[num];
    }

    public void SetInitialTilePosition(int index)
    {
        spriteList[index].transform.position = new Vector3(-2.5f + (float)(index % 6), -4f + (float)(index / 6), 0);
    }
}