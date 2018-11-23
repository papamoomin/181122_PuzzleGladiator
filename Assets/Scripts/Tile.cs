using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    public PuzzleManager ThePuzzleManager;
    public short tileNum = -1;
    public float scale;
    public int index;

    private void Awake()
    {
        scale = transform.lossyScale.x;
    }
    private void OnMouseDown()
    {
        if (ThePuzzleManager.isTurn)
        {
            ThePuzzleManager.isTileClick = true;
            ThePuzzleManager.clickedTileNum = tileNum;
            ThePuzzleManager.clickedTile.Add(this);
            ThePuzzleManager.lastClick = index;
            transform.DOScale(scale * 0.8f, 0.2f);

        }
    }

    public bool CheckIndex()
    {
        bool b = false;
        int x = index % 6;
        int y = index / 6;
        int lastClick = ThePuzzleManager.lastClick;
        if (lastClick < 0)
            return b;
        int lastX = lastClick % 6;
        int lastY = lastClick / 6;
        if (x - lastX >= -1 && x - lastX <= 1)
        {
            if (y - lastY >= -1 && y - lastY <= 1)
            {
                b = true;
            }
        }
        return b;
    }

    private void OnMouseEnter()
    {
        if (ThePuzzleManager.isTurn)
        {
            bool isCorrectTile = false;
            if (ThePuzzleManager.isTileClick)
            {
                if (ThePuzzleManager.clickedTileNum.Equals(tileNum))
                {
                    isCorrectTile = true;
                }
            }
            if (ThePuzzleManager.clickedTile.Contains(this))
            {
                for (int i = ThePuzzleManager.clickedTile.Count - 1; i > -1; --i)
                {
                    if (ThePuzzleManager.clickedTile[i].Equals(this))
                    {
                        ThePuzzleManager.lastClick = index;
                        SetThisTurnText();
                        return;
                    }
                    ThePuzzleManager.clickedTile[i].transform.DOScale(scale, 0.2f);
                    ThePuzzleManager.clickedTile.RemoveAt(i);
                }
            }
            if (!CheckIndex())
                return;
            if (isCorrectTile)
            {
                transform.DOScale(scale * 0.8f, 0.2f);
                ThePuzzleManager.clickedTile.Add(this);
                ThePuzzleManager.lastClick = index;
                SetThisTurnText();
            }
        }
    }

    public void SetThisTurnText()
    {
        switch (ThePuzzleManager.clickedTileNum)
        {
            case 0:
                {//sword
                    AllManager.TheManager.Send("SetThisTurnDamageText", AllManager.TheManager._UIManNum,
                        new List<int> { ((ThePuzzleManager.clickedTile.Count - 1) / 2) });
                    break;
                }
            case 1:
                {//shield
                    AllManager.TheManager.Send("SetThisTurnDefenceText", AllManager.TheManager._UIManNum,
new List<int> { ((ThePuzzleManager.clickedTile.Count - 1) / 2) });
                    break;
                }
            case 2:
                {//potion
                    AllManager.TheManager.Send("SetThisTurnHealingText", AllManager.TheManager._UIManNum,
new List<int> { ((ThePuzzleManager.clickedTile.Count - 1) / 2) });
                    break;
                }
        }
    }

    public void ResetTile()
    {
        ThePuzzleManager.SetPuzzleTile(index, this);
        ResetTileScale();
    }
    public void ResetTileScale()
    {
        transform.DOScale(scale, 0.2f);
    }
}