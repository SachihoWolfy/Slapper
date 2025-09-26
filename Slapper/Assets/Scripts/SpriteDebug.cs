using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDebug : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite Hurt_Left;
    public Sprite Hurt_Up_Left;
    public Sprite Hurt_Up;
    public Sprite Hurt_Up_Right;
    public Sprite Hurt_Right;
    public Sprite Hurt_Default;
    // Start is called before the first frame update
    public void Hurt(Direction d)
    {
        switch (d)
        {
            case Direction.LEFT:
                sr.sprite = Hurt_Left;
                break;
            case Direction.LEFT_UP:
                sr.sprite = Hurt_Up_Left;
                break;
            case Direction.UP:
                sr.sprite= Hurt_Up;
                break;
            case Direction.RIGHT_UP:
                sr.sprite=Hurt_Up_Right;
                break;
            case Direction.RIGHT:
                sr.sprite=Hurt_Right;
                break;
            default:
                sr.sprite = Hurt_Default;
                break;
        }
    }
    public void Default()
    {
        sr.sprite = idleSprite;
    }
}
