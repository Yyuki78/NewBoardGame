using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRoll : MonoBehaviour
{
    public int MoveNum = 0;

    private bool isGacha = false;
    [SerializeField] int spritePerFrame = 12;

    private int index = 0;
    private int frame = 0;

    [SerializeField] Sprite[] dices;
    [SerializeField] Image DiceImage;
    [SerializeField] GameObject DiceRollButton;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameManager _manager;

    void Start()
    {
        _text.text = "残り移動回数:0";
    }

    void Update()
    {
        if (!isGacha) return;
        if (index == dices.Length) return;
        frame++;
        if (frame < spritePerFrame) return;
        DiceImage.sprite = dices[index];
        frame = 0;
        index++;
        if (index >= dices.Length)
        {
            index = 0;
        }
    }

    private void OnEnable()
    {
        DiceRollButton.SetActive(true);
        _text.gameObject.SetActive(false);
        spritePerFrame = 12;
    }

    private void OnDisable()
    {
        _text.gameObject.SetActive(false);
        spritePerFrame = 12;
    }

    public void OnClick()
    {
        StartCoroutine(diceRoll());
    }

    private IEnumerator diceRoll()
    {
        isGacha = true;
        DiceRollButton.SetActive(false);
        _text.gameObject.SetActive(true);
        _text.text = "残り移動回数:0";
        index = Random.Range(0, dices.Length);
        yield return new WaitForSeconds(0.5f);
        spritePerFrame += 12;
        yield return new WaitForSeconds(0.5f);
        spritePerFrame += 12;
        yield return new WaitForSeconds(0.5f);
        spritePerFrame += 12;
        yield return new WaitForSeconds(0.5f);
        isGacha = false;
        yield return null;
        DiceImage.sprite = dices[index];
        MoveNum = index + 1;
        _text.text = "残り移動回数:" + MoveNum;
        yield return null;
        _manager.GoMove();
        yield break;
    }

    public void MinusMoveNum()
    {
        if (MoveNum <= 0) return;
        MoveNum--;
        _text.text = "残り移動回数:" + MoveNum;
        if (MoveNum == 0)
        {
            _manager.GoAttack();
        }
    }
}
