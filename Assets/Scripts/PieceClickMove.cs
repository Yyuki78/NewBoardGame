using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceClickMove : MonoBehaviour
{
    [SerializeField] GameManager _manager;
    [SerializeField] TileManager _tile;
    private PieceInfomation _info;

    private bool Decided = false;

    void Start()
    {
        _info = GetComponent<PieceInfomation>();
    }

    public void OnMouseDown()
    {
        switch (_manager._currentState)
        {
            case GameManager.GameState.AttributeAssignment:
                if (!_info.Side) return;
                if (_info.Role == 4) return;
                if (Decided) return;
                Decided = true;
                _manager.MinusAttribute(_info);
                break;
            case GameManager.GameState.Move:
                _tile.ShowStatus(_info);
                if (!_info.Side) return;
                _tile.CheckMove(_info);
                break;
            case GameManager.GameState.Attack:
                _tile.ShowStatus(_info);
                if (_info.Side)
                {
                    if (!_info.CanAttack) return;
                    _tile.CheckAttack(_info);
                }
                else
                {
                    _tile.AttackPiece(_info);
                }
                break;
            case GameManager.GameState.EnemyMove:
                _tile.ShowStatus(_info);
                if (_info.Side) return;
                _tile.CheckMove(_info);
                break;
            case GameManager.GameState.EnemyAttack:
                _tile.ShowStatus(_info);
                if (!_info.Side)
                {
                    if (!_info.CanAttack) return;
                    _tile.CheckAttack(_info);
                }
                else
                {
                    _tile.AttackPiece(_info);
                }
                break;
            default:
                Debug.Log("ƒNƒŠƒbƒN‚µ‚Ä‚àˆÓ–¡‚Ì‚È‚¢State‚Å‚·");
                break;
        }
    }
}
