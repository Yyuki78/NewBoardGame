using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] DiceRoll _diceRoll;
    private TileInfomation[] _tiles = new TileInfomation[49];
    private PieceInfomation _pieceInfo;
    private int TileNumber;

    void Awake()
    {
        _tiles = GetComponentsInChildren<TileInfomation>();
    }

    public void CheckMove(PieceInfomation _info)
    {
        _pieceInfo = _info;
        Vector2 pos = _info.CurrentPosition;
        int role = _info.Role;
        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i].isBlinking)
                _tiles[i].StopBlinking();
        }
        TileNumber = (int)pos.x - 1 + (int)((pos.y - 1) * 7);
        switch (role)
        {
            case 0://上下左右1マス
            case 1:
            case 2:
                //上
                if (pos.y != 1)
                    _tiles[TileNumber - 7].Blinking();
                //下
                if (pos.y != 7)
                    _tiles[TileNumber + 7].Blinking();
                //右
                if (pos.x != 7)
                    _tiles[TileNumber + 1].Blinking();
                //左
                if (pos.x != 1)
                    _tiles[TileNumber - 1].Blinking();
                break;
            case 3://自分からニマスで動ける範囲
                //上
                if (pos.y != 1)
                    _tiles[TileNumber - 7].Blinking();
                //下
                if (pos.y != 7)
                    _tiles[TileNumber + 7].Blinking();
                //右
                if (pos.x != 7)
                    _tiles[TileNumber + 1].Blinking();
                //左
                if (pos.x != 1)
                    _tiles[TileNumber - 1].Blinking();
                //右上
                if (pos.y != 1 && pos.x != 7)
                    _tiles[TileNumber - 6].Blinking();
                //右下
                if (pos.y != 7 && pos.x != 7)
                    _tiles[TileNumber + 8].Blinking();
                //左上
                if (pos.y != 1 && pos.x != 1)
                    _tiles[TileNumber - 8].Blinking();
                //左下
                if (pos.y != 7 && pos.x != 1)
                    _tiles[TileNumber + 6].Blinking();
                //上上
                if (pos.y > 2)
                    _tiles[TileNumber - 14].Blinking();
                //下下
                if (pos.y < 6)
                    _tiles[TileNumber + 14].Blinking();
                //右右
                if (pos.x < 6)
                    _tiles[TileNumber + 2].Blinking();
                //左左
                if (pos.x > 2)
                    _tiles[TileNumber - 2].Blinking();
                break;
            case 4://自分の周囲一マス(斜めも含む)
                //上
                if (pos.y != 1)
                    _tiles[TileNumber - 7].Blinking();
                //下
                if (pos.y != 7)
                    _tiles[TileNumber + 7].Blinking();
                //右
                if (pos.x != 7)
                    _tiles[TileNumber + 1].Blinking();
                //左
                if (pos.x != 1)
                    _tiles[TileNumber - 1].Blinking();
                //右上
                if (pos.y != 1 && pos.x != 7)
                    _tiles[TileNumber - 6].Blinking();
                //右下
                if (pos.y != 7 && pos.x != 7)
                    _tiles[TileNumber + 8].Blinking();
                //左上
                if (pos.y != 1 && pos.x != 1)
                    _tiles[TileNumber - 8].Blinking();
                //左下
                if (pos.y != 7 && pos.x != 1)
                    _tiles[TileNumber + 6].Blinking();
                break;
        }
    }

    public void MovePiece(Vector2 pos)
    {
        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i].isBlinking)
                _tiles[i].StopBlinking();
        }
        _tiles[TileNumber].ExistPiece = false;
        _pieceInfo.Move(pos);
        _diceRoll.MinusMoveNum();
    }
}
