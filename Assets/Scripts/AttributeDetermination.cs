using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AttributeDetermination : MonoBehaviour
{
    public enum AttributeState
    {
        First,
        DiceRoll,
        Decision,
        Reroll,
        Finish
    }
    public AttributeState _currentState { private set; get; }

    public int[] attributeNum = new int[10];
    [SerializeField] AttributeDice[] _dices;

    [SerializeField] GameObject FirstButton;
    [SerializeField] GameObject RerollButton;
    [SerializeField] GameObject DecisionButton;
    [SerializeField] TextMeshProUGUI _rerollText;
    private int rerollNum = 2;

    void Start()
    {
        if (GameInfomation.AttributeNum == null)
            GameInfomation.AttributeNum = new int[10];
        Application.targetFrameRate = 60;
        SetState(AttributeState.First);
    }

    public void SetState(AttributeState state)
    {
        _currentState = state;
        switch (state)
        {
            case AttributeState.First:
                FirstButton.SetActive(true);
                RerollButton.SetActive(false);
                DecisionButton.SetActive(false);
                break;
            case AttributeState.DiceRoll:
                StartCoroutine(diceRoll());
                break;
            case AttributeState.Decision:
                RerollButton.SetActive(true);
                DecisionButton.SetActive(true);
                if (rerollNum <= 0)
                {
                    RerollButton.SetActive(false);
                }
                break;
            case AttributeState.Reroll:
                rerollNum--;
                _rerollText.text = "Žc‚èF" + rerollNum;
                SetState(AttributeState.DiceRoll);
                break;
            case AttributeState.Finish:
                GameInfomation.AttributeNum = attributeNum;
                SceneManager.LoadScene("GameScene");
                break;
        }
    }

    private IEnumerator diceRoll()
    {
        FirstButton.SetActive(false);
        RerollButton.SetActive(false);
        DecisionButton.SetActive(false);

        for(int i = 0; i < _dices.Length; i++)
        {
            _dices[i].StartGacha();
        }

        yield return new WaitForSeconds(4f);
        Array.Sort(attributeNum);
        yield return null;
        for (int i = 0; i < _dices.Length; i++)
        {
            _dices[i].SetImage(attributeNum[i]);
        }

        SetState(AttributeState.Decision);
        yield break;
    }

    public void OnClick(int num)
    {
        switch (num)
        {
            case 0:
                SetState(AttributeState.DiceRoll);
                break;
            case 1:
                SetState(AttributeState.Reroll);
                break;
            case 2:
                SetState(AttributeState.Finish);
                break;
        }
    }
}
