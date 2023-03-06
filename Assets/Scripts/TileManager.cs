using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileManager : MonoBehaviour
{
    public bool isGameFinish { get; private set; } = false;
    public bool isWinSide { get; private set; } = false;

    [SerializeField] DiceRoll _diceRoll;
    private TileInfomation[] _tiles = new TileInfomation[49];
    private PieceInfomation[] _allPieceInfos = new PieceInfomation[22];
    private PieceInfomation _pieceInfo;
    private int TileNumber;
    [SerializeField] TextMeshProUGUI _pieceInfoText;

    void Awake()
    {
        _tiles = GetComponentsInChildren<TileInfomation>();
        _allPieceInfos = GetComponentsInChildren<PieceInfomation>();
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

    public void CheckAttack(PieceInfomation _info)
    {
        _pieceInfo = _info;
        Vector2 pos = _info.CurrentPosition;
        int role = _info.Role;
        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i].isColorChange)
                _tiles[i].ColorChangeBefore();
        }
        TileNumber = (int)pos.x - 1 + (int)((pos.y - 1) * 7);
        switch (role)
        {
            case 0://上下左右1マス
            case 3:
                if (pos.y != 1)//上
                    _tiles[TileNumber - 7].ColorChangeRed();
                if (pos.y != 7)//下
                    _tiles[TileNumber + 7].ColorChangeRed();
                if (pos.x != 7)//右
                    _tiles[TileNumber + 1].ColorChangeRed();
                if (pos.x != 1)//左
                    _tiles[TileNumber - 1].ColorChangeRed();
                break;
            case 2://自分からニマスで動ける範囲
                if (pos.y != 1)//上
                    _tiles[TileNumber - 7].ColorChangeRed();
                if (pos.y != 7)//下
                    _tiles[TileNumber + 7].ColorChangeRed();
                if (pos.x != 7)//右
                    _tiles[TileNumber + 1].ColorChangeRed();
                if (pos.x != 1)//左
                    _tiles[TileNumber - 1].ColorChangeRed();
                if (pos.y != 1 && pos.x != 7)//右上
                    _tiles[TileNumber - 6].ColorChangeRed();
                if (pos.y != 7 && pos.x != 7)//右下
                    _tiles[TileNumber + 8].ColorChangeRed();
                if (pos.y != 1 && pos.x != 1)//左上
                    _tiles[TileNumber - 8].ColorChangeRed();
                if (pos.y != 7 && pos.x != 1)//左下
                    _tiles[TileNumber + 6].ColorChangeRed();
                if (pos.y > 2)//上上
                    _tiles[TileNumber - 14].ColorChangeRed();
                if (pos.y < 6)//下下
                    _tiles[TileNumber + 14].ColorChangeRed();
                if (pos.x < 6)//右右
                    _tiles[TileNumber + 2].ColorChangeRed();
                if (pos.x > 2)//左左
                    _tiles[TileNumber - 2].ColorChangeRed();
                break;
            case 1://自分の周囲一マス(斜めも含む)
            case 4:
                if (pos.y != 1)//上
                    _tiles[TileNumber - 7].ColorChangeRed();
                if (pos.y != 7)//下
                    _tiles[TileNumber + 7].ColorChangeRed();
                if (pos.x != 7)//右
                    _tiles[TileNumber + 1].ColorChangeRed();
                if (pos.x != 1)//左
                    _tiles[TileNumber - 1].ColorChangeRed();
                if (pos.y != 1 && pos.x != 7)//右上
                    _tiles[TileNumber - 6].ColorChangeRed();
                if (pos.y != 7 && pos.x != 7)//右下
                    _tiles[TileNumber + 8].ColorChangeRed();
                if (pos.y != 1 && pos.x != 1)//左上
                    _tiles[TileNumber - 8].ColorChangeRed();
                if (pos.y != 7 && pos.x != 1)//左下
                    _tiles[TileNumber + 6].ColorChangeRed();
                break;
        }

        if (!_pieceInfoText.gameObject.activeSelf)
            _pieceInfoText.gameObject.SetActive(true);
        _pieceInfoText.text = "駒の情報\nHP:" + _pieceInfo.HP + "\n攻撃:" + _pieceInfo.Attack + "\n防御:" + _pieceInfo.Defense + "\n魔法防御:" + _pieceInfo.MagicDefense;
    }

    public void AttackPiece(PieceInfomation _enemyInfo)
    {
        Vector2 pos = _enemyInfo.CurrentPosition;
        TileNumber = (int)pos.x - 1 + (int)((pos.y - 1) * 7);
        if (!_tiles[TileNumber].ExistPiece) return;
        if (!_tiles[TileNumber].isColorChange) return;
        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i].isColorChange)
                _tiles[i].ColorChangeBefore();
        }

        _pieceInfo.Attacking();
        //ダメージ計算
        float Attack = _pieceInfo.Attack;
        float Defence = _enemyInfo.Defense;
        if (_pieceInfo.Role == 2)
        {
            Defence = _enemyInfo.MagicDefense;
        }
        float HitNum = 0;
        switch (_pieceInfo.Attribute)
        {
            case 0:
                if (_enemyInfo.Attribute == 0 || _enemyInfo.Attribute == 3)
                    HitNum = Random.Range(20, 40);//同じ
                else if (_enemyInfo.Attribute == 1)
                    HitNum = Random.Range(10, 20);//不利
                else
                    HitNum = Random.Range(50, 70);//有利
                break;
            case 1:
                if (_enemyInfo.Attribute == 0)
                    HitNum = Random.Range(50, 70);//有利
                else if (_enemyInfo.Attribute == 1 || _enemyInfo.Attribute == 3)
                    HitNum = Random.Range(20, 40);//同じ
                else
                    HitNum = Random.Range(10, 20);//不利
                break;
            case 2:
                if (_enemyInfo.Attribute == 0)
                    HitNum = Random.Range(10, 20);//不利
                else if (_enemyInfo.Attribute == 1)
                    HitNum = Random.Range(50, 70);//有利
                else
                    HitNum = Random.Range(20, 40);//同じ
                break;
            case 3:
                HitNum = Random.Range(20, 40);//同じ
                break;
        }
        HitNum *= (Attack / Defence);

        _enemyInfo.Attacked((int)HitNum);
        if(_enemyInfo.HP < 0)
        {
            _tiles[TileNumber].ExistPiece = false;
            if (_enemyInfo.Role == 4)
            {
                isGameFinish = true;
                if (_enemyInfo.Side)
                    isWinSide = false;
                else
                    isWinSide = true;
            }
        }
        Debug.Log("攻撃しました" + HitNum);
    }

    public void TurnFinish()
    {
        _pieceInfoText.gameObject.SetActive(false);
        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i].isColorChange)
                _tiles[i].ColorChangeBefore();
        }
        for (int i = 0; i < _allPieceInfos.Length; i++)
        {
            _allPieceInfos[i].TurnFinish();
        }
    }
}
