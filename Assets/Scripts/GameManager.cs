using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void Awake()
    {
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
        //ÇªÇÍÇºÇÍÇÃëÆê´ÇÃå¬êîÇäiî[
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
        _diceNumText.text = "Å~" + greenNum + "\nÅ~" + redNum + "\nÅ~" + blueNum;

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
                Debug.Log("ÉQÅ[ÉÄÇénÇﬂÇ‹Ç∑");
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
                _phaseText.text = PhaseText[3];
                _explanationText.text = ExplanationText[3];
                break;
            case GameState.Finish:
                break;
            case GameState.EnemyDiceRoll:
                break;
            case GameState.EnemyMove:
                break;
            case GameState.EnemyAttack:
                break;
            case GameState.EnemyFinish:
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
        _diceNumText.text = "Å~" + greenNum + "\nÅ~" + redNum + "\nÅ~" + blueNum;
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
        yield return new WaitForSeconds(3f);
        GameStartText.SetActive(false);
        _turnText.gameObject.SetActive(true);
        _phaseText.gameObject.SetActive(true);
        _explanationText.gameObject.SetActive(true);
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

    void Update()
    {
        
    }
}
