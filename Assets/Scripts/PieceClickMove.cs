using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceClickMove : MonoBehaviour
{
    [SerializeField] GameManager _manager;
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
                break;
            case GameManager.GameState.Attack:
                break;
            case GameManager.GameState.EnemyMove:
                break;
            case GameManager.GameState.EnemyAttack:
                break;
            default:
                Debug.Log("ƒNƒŠƒbƒN‚µ‚Ä‚àˆÓ–¡‚Ì‚È‚¢State‚Å‚·");
                break;
        }
    }
}
