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
        if(x-lastX >= -1 && x-lastX <= 1)
        {
            if(y-lastY >= -1 && y-lastY <= 1)
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
            if (!CheckIndex())
                return;
            ThePuzzleManager.lastClick = index;
            bool isCorrectTile = false;
            if (ThePuzzleManager.isTileClick)
            {
                if (ThePuzzleManager.clickedTileNum.Equals(tileNum))
                {
                    isCorrectTile = true;
                }
            }
            if (isCorrectTile)
            {
                transform.DOScale(scale * 0.8f, 0.2f);
                ThePuzzleManager.clickedTile.Add(this);
            }
        }
    }

    public void ResetTile()
    {
        ThePuzzleManager.SetPuzzleTile(index, this);
        transform.DOScale(scale, 0.2f);
    }
}