using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfomation : MonoBehaviour
{
    [SerializeField] Vector2 MyPosition;
    public Vector2 myPosition => MyPosition;
    public bool ExistPiece;
    public bool isBlinking { get; private set; } = false;
    public bool isColorChange { get; private set; } = false;

    private TileManager _manager;

    private SpriteRenderer _sprite;
    private Color oldColor;
    private bool plus = false;
    private Color changeValue = new Color(0, 0, 0, 0.02f);

    void Start()
    {
        _manager = GetComponentInParent<TileManager>();
        _sprite = GetComponent<SpriteRenderer>();
        oldColor = _sprite.color;
    }

    private void Update()
    {
        if (!isBlinking) return;
        if (_sprite.color.a > 1.0f)
            plus = false;
        if (_sprite.color.a < 0f)
            plus = true;

        if (plus)
        {
            _sprite.color += changeValue;
        }
        if (!plus)
        {
            _sprite.color -= changeValue;
        }
    }

    public void Blinking()
    {
        if (ExistPiece) return;
        if (isBlinking) return;
        isBlinking = true;
        //Debug.Log(MyPosition + "‚ªŒõ‚è‚Ü‚·");
    }

    public void StopBlinking()
    {
        if (!isBlinking) return;
        isBlinking = false;
        _sprite.color = oldColor;
    }

    public void ColorChangeRed()
    {
        if (isBlinking) return;
        if (isColorChange) return;
        isBlinking = true;
        isColorChange = true;
        _sprite.color = Color.red;
        //Debug.Log(MyPosition + "‚ªÔ‚­Œõ‚è‚Ü‚·");
    }

    public void ColorChangeBefore()
    {
        if (!isBlinking) return;
        if (!isColorChange) return;
        isBlinking = false;
        isColorChange = false;
        _sprite.color = oldColor;
    }

    public void OnMouseDown()
    {
        if (!isBlinking) return;
        if (isColorChange) return;
        _manager.MovePiece(myPosition);
        ExistPiece = true;
    }
}
