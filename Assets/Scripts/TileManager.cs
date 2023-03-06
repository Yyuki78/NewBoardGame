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
            case 0://�㉺���E1�}�X
            case 3:
                if (pos.y != 1)//��
                    _tiles[TileNumber - 7].ColorChangeRed();
                if (pos.y != 7)//��
                    _tiles[TileNumber + 7].ColorChangeRed();
                if (pos.x != 7)//�E
                    _tiles[TileNumber + 1].ColorChangeRed();
                if (pos.x != 1)//��
                    _tiles[TileNumber - 1].ColorChangeRed();
                break;
            case 2://��������j�}�X�œ�����͈�
                if (pos.y != 1)//��
                    _tiles[TileNumber - 7].ColorChangeRed();
                if (pos.y != 7)//��
                    _tiles[TileNumber + 7].ColorChangeRed();
                if (pos.x != 7)//�E
                    _tiles[TileNumber + 1].ColorChangeRed();
                if (pos.x != 1)//��
                    _tiles[TileNumber - 1].ColorChangeRed();
                if (pos.y != 1 && pos.x != 7)//�E��
                    _tiles[TileNumber - 6].ColorChangeRed();
                if (pos.y != 7 && pos.x != 7)//�E��
                    _tiles[TileNumber + 8].ColorChangeRed();
                if (pos.y != 1 && pos.x != 1)//����
                    _tiles[TileNumber - 8].ColorChangeRed();
                if (pos.y != 7 && pos.x != 1)//����
                    _tiles[TileNumber + 6].ColorChangeRed();
                if (pos.y > 2)//���
                    _tiles[TileNumber - 14].ColorChangeRed();
                if (pos.y < 6)//����
                    _tiles[TileNumber + 14].ColorChangeRed();
                if (pos.x < 6)//�E�E
                    _tiles[TileNumber + 2].ColorChangeRed();
                if (pos.x > 2)//����
                    _tiles[TileNumber - 2].ColorChangeRed();
                break;
            case 1://�����̎��͈�}�X(�΂߂��܂�)
            case 4:
                if (pos.y != 1)//��
                    _tiles[TileNumber - 7].ColorChangeRed();
                if (pos.y != 7)//��
                    _tiles[TileNumber + 7].ColorChangeRed();
                if (pos.x != 7)//�E
                    _tiles[TileNumber + 1].ColorChangeRed();
                if (pos.x != 1)//��
                    _tiles[TileNumber - 1].ColorChangeRed();
                if (pos.y != 1 && pos.x != 7)//�E��
                    _tiles[TileNumber - 6].ColorChangeRed();
                if (pos.y != 7 && pos.x != 7)//�E��
                    _tiles[TileNumber + 8].ColorChangeRed();
                if (pos.y != 1 && pos.x != 1)//����
                    _tiles[TileNumber - 8].ColorChangeRed();
                if (pos.y != 7 && pos.x != 1)//����
                    _tiles[TileNumber + 6].ColorChangeRed();
                break;
        }

        if (!_pieceInfoText.gameObject.activeSelf)
            _pieceInfoText.gameObject.SetActive(true);
        _pieceInfoText.text = "��̏��\nHP:" + _pieceInfo.HP + "\n�U��:" + _pieceInfo.Attack + "\n�h��:" + _pieceInfo.Defense + "\n���@�h��:" + _pieceInfo.MagicDefense;
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
        //�_���[�W�v�Z
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
                    HitNum = Random.Range(20, 40);//����
                else if (_enemyInfo.Attribute == 1)
                    HitNum = Random.Range(10, 20);//�s��
                else
                    HitNum = Random.Range(50, 70);//�L��
                break;
            case 1:
                if (_enemyInfo.Attribute == 0)
                    HitNum = Random.Range(50, 70);//�L��
                else if (_enemyInfo.Attribute == 1 || _enemyInfo.Attribute == 3)
                    HitNum = Random.Range(20, 40);//����
                else
                    HitNum = Random.Range(10, 20);//�s��
                break;
            case 2:
                if (_enemyInfo.Attribute == 0)
                    HitNum = Random.Range(10, 20);//�s��
                else if (_enemyInfo.Attribute == 1)
                    HitNum = Random.Range(50, 70);//�L��
                else
                    HitNum = Random.Range(20, 40);//����
                break;
            case 3:
                HitNum = Random.Range(20, 40);//����
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
        Debug.Log("�U�����܂���" + HitNum);
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
