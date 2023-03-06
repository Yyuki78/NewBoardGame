using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInfomation : MonoBehaviour
{
    public bool Side;
    public int Role;//0‚Í•à•º,1‚Í‹RŽm,2‚Í–‚–@•º,3‚Í‹R”n,4‚ÍŽwŠöŠ¯
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int MagicDefense { get; private set; }
    public int HP { get; private set; } = 100;
    public int Attribute { get; private set; }
    public Vector2 CurrentPosition;
    public bool CanAttack { get; private set; } = true;
    
    private SpriteRenderer[] PieceImage;
    [SerializeField] GameObject HPBar;

    void Start()
    {
        PieceImage = GetComponentsInChildren<SpriteRenderer>();
        HPBar.SetActive(false);
        switch (Role)
        {
            case 0:
                Attack = 2;
                Defense = 2;
                MagicDefense = 2;
                break;
            case 1:
                Attack = 3;
                Defense = 4;
                MagicDefense = 3;
                break;
            case 2:
                Attack = 2;
                Defense = 1;
                MagicDefense = 2;
                break;
            case 3:
                Attack = 4;
                Defense = 2;
                MagicDefense = 1;
                break;
            case 4:
                Attack = 3;
                Defense = 3;
                MagicDefense = 3;
                Attribute = 3;
                break;
        }
        if (!Side)
        {
            if (Role == 4) return;
            int ran = Random.Range(0, 3);
            SetAttribute(ran);
        }
    }

    public void SetAttribute(int AttributeNum)
    {
        Attribute = AttributeNum;
        switch (Attribute)
        {
            case 0:
                PieceImage[1].color = Color.green;
                break;
            case 1:
                PieceImage[1].color = Color.red;
                break;
            case 2:
                PieceImage[1].color = Color.blue;
                break;
        }
    }

    public void Move(Vector2 newPos)
    {
        CurrentPosition = newPos;
        transform.position = new Vector3((newPos.x - 4) * 1.33f, (4 - newPos.y) * 1.33f, 0);
    }

    public void Attacking()
    {
        CanAttack = false;
    }

    public void Attacked(int num)
    {
        if (!HPBar.activeSelf)
            HPBar.SetActive(true);
        HP -= num;
        HPBar.transform.localScale = new Vector3(HP / 100f, 1, 1);
        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TurnFinish()
    {
        CanAttack = true;
    }
}
