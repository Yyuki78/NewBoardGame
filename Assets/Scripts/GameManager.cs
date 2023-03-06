using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        AttributeAssignment,
        StartGame,
        DiceRoll,
        Move,
        Attack,
        Finish,
        EnemyDiceRoll,
        EnemyMove,
        EnemyAttack,
        EnemyFinish,
        GameFinish
    }
    public GameState _currentState { private set; get; }

    [SerializeField] int TurnNum = 0;

    [SerializeField] TextMeshProUGUI _turnText;
    [SerializeField] TextMeshProUGUI _phaseText;
    [SerializeField] TextMeshProUGUI _explanationText;
    [SerializeField] TextMeshProUGUI _diceNumText;

    [SerializeField] [TextArea] string[] TurnText;
    [SerializeField] [TextArea] string[] PhaseText;
    [SerializeField] [TextArea] string[] ExplanationText;

    private int[] attributeNum = new int[10];
    [SerializeField] int greenNum, redNum, blueNum;

    [SerializeField] Sprite[] diceImages;
    [SerializeField] Image CurrentDiceImage;

    [SerializeField] GameObject AttributeDecision;
    [SerializeField] GameObject GameStartText;

    [SerializeField] GameObject DiceRoll;
    [SerializeField] GameObject TurnFinishButton;
    [SerializeField] TextMeshProUGUI _turnNumText;

    [SerializeField] TileManager _tileManager;
    [SerializeField] GameObject GameFinish;

    void Awake()
    {
        Application.targetFrameRate = 60;
        if (GameInfomation.AttributeNum == null)
        {
            GameInfomation.AttributeNum = new int[10];
            for (int i = 0; i < GameInfomation.AttributeNum.Length; i++)
            {
                GameInfomation.AttributeNum[i] = UnityEngine.Random.Range(0, 3);
            }
            Array.Sort(GameInfomation.AttributeNum);
        }
        attributeNum = GameInfomation.AttributeNum;
    }

    void Start()
    {
        //���ꂼ��̑����̌����i�[
        int n = 0;
        while (attributeNum[n] == 0)
        {
            n++;
            if (n >= 10)
                break;
        }
        greenNum = n;

        while (attributeNum[n] == 1)
        {
            n++;
            if (n >= 10)
                break;
        }
        redNum = n - greenNum;
        blueNum = 10 - n;
        _diceNumText.text = "�~" + greenNum + "\n�~" + redNum + "\n�~" + blueNum;

        _turnText.text = TurnText[0];
        _phaseText.text = PhaseText[0];
        _explanationText.text = ExplanationText[0];

        SetState(GameState.AttributeAssignment);
    }

    public void SetState(GameState state)
    {
        _currentState = state;
        switch (state)
        {
            case GameState.AttributeAssignment:
                AttributeDecision.SetActive(true);
                break;
            case GameState.StartGame:
                Debug.Log("�Q�[�����n�߂܂�");
                StartCoroutine(GameStart());
                break;
            case GameState.DiceRoll:
                _turnText.text = TurnText[1];
                _phaseText.text = PhaseText[1];
                _explanationText.text = ExplanationText[1];
                DiceRoll.SetActive(true);
                break;
            case GameState.Move:
                _phaseText.text = PhaseText[2];
                _explanationText.text = ExplanationText[2];
                break;
            case GameState.Attack:
                DiceRoll.SetActive(false);
                _phaseText.text = PhaseText[3];
                _explanationText.text = ExplanationText[3];
                TurnFinishButton.SetActive(true);
                break;
            case GameState.Finish:
                SetState(GameState.EnemyDiceRoll);
                break;
            case GameState.EnemyDiceRoll:
                _turnText.text = TurnText[2];
                _phaseText.text = PhaseText[1];
                _explanationText.text = ExplanationText[1];
                DiceRoll.SetActive(true);
                break;
            case GameState.EnemyMove:
                _phaseText.text = PhaseText[2];
                _explanationText.text = ExplanationText[2];
                break;
            case GameState.EnemyAttack:
                DiceRoll.SetActive(false);
                _phaseText.text = PhaseText[3];
                _explanationText.text = ExplanationText[3];
                TurnFinishButton.SetActive(true);
                break;
            case GameState.EnemyFinish:
                TurnNum++;
                _turnNumText.text = "�^�[��:" + TurnNum;
                SetState(GameState.DiceRoll);
                break;
            case GameState.GameFinish:
                break;
        }
    }

    public void MinusAttribute(PieceInfomation info)
    {
        if (greenNum > 0)
        {
            info.SetAttribute(0);
            greenNum--;
        }
        else
        {
            if (redNum > 0)
            {
                info.SetAttribute(1);
                redNum--;
            }
            else
            {
                info.SetAttribute(2);
                blueNum--;
            }
        }
        _diceNumText.text = "�~" + greenNum + "\n�~" + redNum + "\n�~" + blueNum;
        if (greenNum <= 0)
        {
            CurrentDiceImage.sprite = diceImages[1];
        }
        if (redNum <= 0 && greenNum <= 0)
        {
            CurrentDiceImage.sprite = diceImages[2];
        }
        if (blueNum <= 0 && redNum <= 0 && greenNum <= 0)
        {
            SetState(GameState.StartGame);
        }
    }

    private IEnumerator GameStart()
    {
        AttributeDecision.SetActive(false);
        GameStartText.SetActive(true);
        _turnText.gameObject.SetActive(false);
        _phaseText.gameObject.SetActive(false);
        _explanationText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        GameStartText.SetActive(false);
        _turnText.gameObject.SetActive(true);
        _phaseText.gameObject.SetActive(true);
        _explanationText.gameObject.SetActive(true);
        _turnNumText.gameObject.SetActive(true);
        TurnNum++;
        _turnNumText.text = "�^�[��:" + TurnNum;
        SetState(GameState.DiceRoll);
        yield break;
    }

    public void GoMove()
    {
        if (_currentState == GameState.DiceRoll)
        {
            SetState(GameState.Move);
        }
        if (_currentState == GameState.EnemyDiceRoll)
        {
            SetState(GameState.EnemyMove);
        }
    }

    public void GoAttack()
    {
        if (_currentState == GameState.Move)
        {
            SetState(GameState.Attack);
        }
        if (_currentState == GameState.EnemyMove)
        {
            SetState(GameState.EnemyAttack);
        }
    }

    public void OnClickTurnFinish()
    {
        TurnFinishButton.SetActive(false);
        _tileManager.TurnFinish();
        if (_tileManager.isGameFinish)
        {
            GameFinishEffect();
            return;
        }
        if (_currentState == GameState.Attack)
        {
            SetState(GameState.Finish);
        }
        if (_currentState == GameState.EnemyAttack)
        {
            SetState(GameState.EnemyFinish);
        }
    }

    private void GameFinishEffect()
    {
        Debug.Log("�Q�[���I���@�������̂�" + _tileManager.isWinSide);
        GameFinish.SetActive(true);
    }

    public void OnClickOneMoreGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
