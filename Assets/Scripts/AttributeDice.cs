using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeDice : MonoBehaviour
{
    [SerializeField] int position;
    public bool isGacha = false;
    public int attributeNum = 0;

    public Sprite[] sprites;
    [SerializeField] int spritePerFrame = 12;

    private int index = 0;
    private SpriteRenderer image;
    private int frame = 0;

    [SerializeField] AttributeDetermination _determination;

    void Awake()
    {
        image = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isGacha) return;
        if (index == sprites.Length) return;
        frame++;
        if (frame < spritePerFrame) return;
        image.sprite = sprites[index];
        frame = 0;
        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }
    }

    public void SetImage(int num)
    {
        image.sprite = sprites[num];
    }

    public void StartGacha()
    {
        isGacha = true;
        StartCoroutine(startGacha());
    }

    private IEnumerator startGacha()
    {
        yield return new WaitForSeconds(2.5f + position * 0.1f);
        isGacha = false;
        attributeNum = Random.Range(0, 3);
        yield return null;
        image.sprite = sprites[attributeNum];
        _determination.attributeNum[position] = attributeNum;
        yield break;
    }
}
