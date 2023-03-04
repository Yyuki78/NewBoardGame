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
            case 0://�㉺���E1�}�X
            case 1:
            case 2:
                //��
                if (pos.y != 1)
                    _tiles[TileNumber - 7].Blinking();
                //��
                if (pos.y != 7)
                    _tiles[TileNumber + 7].Blinking();
                //�E
                if (pos.x != 7)
                    _tiles[TileNumber + 1].Blinking();
                //��
                if (pos.x != 1)
                    _tiles[TileNumber - 1].Blinking();
                break;
            case 3://��������j�}�X�œ�����͈�
                //��
                if (pos.y != 1)
                    _tiles[TileNumber - 7].Blinking();
                //��
                if (pos.y != 7)
                    _tiles[TileNumber + 7].Blinking();
                //�E
                if (pos.x != 7)
                    _tiles[TileNumber + 1].Blinking();
                //��
                if (pos.x != 1)
                    _tiles[TileNumber - 1].Blinking();
                //�E��
                if (pos.y != 1 && pos.x != 7)
                    _tiles[TileNumber - 6].Blinking();
                //�E��
                if (pos.y != 7 && pos.x != 7)
                    _tiles[TileNumber + 8].Blinking();
                //����
                if (pos.y != 1 && pos.x != 1)
                    _tiles[TileNumber - 8].Blinking();
                //����
                if (pos.y != 7 && pos.x != 1)
                    _tiles[TileNumber + 6].Blinking();
                //���
                if (pos.y > 2)
                    _tiles[TileNumber - 14].Blinking();
                //����
                if (pos.y < 6)
                    _tiles[TileNumber + 14].Blinking();
                //�E�E
                if (pos.x < 6)
                    _tiles[TileNumber + 2].Blinking();
                //����
                if (pos.x > 2)
                    _tiles[TileNumber - 2].Blinking();
                break;
            case 4://�����̎��͈�}�X(�΂߂��܂�)
                //��
                if (pos.y != 1)
                    _tiles[TileNumber - 7].Blinking();
                //��
                if (pos.y != 7)
                    _tiles[TileNumber + 7].Blinking();
                //�E
                if (pos.x != 7)
                    _tiles[TileNumber + 1].Blinking();
                //��
                if (pos.x != 1)
                    _tiles[TileNumber - 1].Blinking();
                //�E��
                if (pos.y != 1 && pos.x != 7)
                    _tiles[TileNumber - 6].Blinking();
                //�E��
                if (pos.y != 7 && pos.x != 7)
                    _tiles[TileNumber + 8].Blinking();
                //����
                if (pos.y != 1 && pos.x != 1)
                    _tiles[TileNumber - 8].Blinking();
                //����
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
