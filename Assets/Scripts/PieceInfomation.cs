using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInfomation : MonoBehaviour
{
    public bool Side;
    public int Role;//0‚Í•à•º,1‚Í‹RŽm,2‚Í–‚–@•º,3‚Í‹R”n,4‚ÍŽwŠöŠ¯
    public int HP { get; private set; }
    public int Attribute { get; private set; }
    public Vector2 CurrentPosition { get; private set; }
    public bool CanAttack { get; private set; }
    
    private SpriteRenderer[] PieceImage;

    void Start()
    {
        PieceImage = GetComponentsInChildren<SpriteRenderer>();
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
}
