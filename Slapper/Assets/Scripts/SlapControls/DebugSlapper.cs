using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSlapper : MonoBehaviour
{
    public SlapBase slapBase;
    public float force;
    private SpriteDebug sd;

    public KeyCode Left = KeyCode.A;
    public KeyCode Right= KeyCode.D;
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode LeftUp = KeyCode.Q;
    public KeyCode RightUp = KeyCode.E;
    public KeyCode LeftDown = KeyCode.Z;
    public KeyCode RightDown = KeyCode.C;
    void Start()
    {
        if(slapBase == null)
        {
            slapBase = FindAnyObjectByType<SlapBase>();
        }
        if(sd == null)
        {
            sd = gameObject.GetComponent<SpriteDebug>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Left))
        {
            slapBase.Slap(Direction.LEFT, force);
            ChangeSprite(Direction.LEFT);
        }
        else if (Input.GetKey(Right))
        {
            slapBase.Slap(Direction.RIGHT, force);
            ChangeSprite(Direction.RIGHT);
        }
        else if (Input.GetKey(Up))
        {
            slapBase.Slap(Direction.UP, force);
            ChangeSprite(Direction.UP);
        }
        else if (Input.GetKey(Down))
        {
            slapBase.Slap(Direction.DOWN, force);
            ChangeSprite(Direction.DOWN);
        }
        else if (Input.GetKey(LeftUp))
        {
            slapBase.Slap(Direction.LEFT_UP, force);
            ChangeSprite(Direction.LEFT_UP);
        }
        else if (Input.GetKey(RightUp))
        {
            slapBase.Slap(Direction.RIGHT_UP, force);
            ChangeSprite(Direction.RIGHT_UP);
        }
        else if (Input.GetKey(LeftDown))
        {
            slapBase.Slap(Direction.LEFT_DOWN, force);
            ChangeSprite(Direction.LEFT_DOWN);
        }
        else if (Input.GetKey(RightDown))
        {
            slapBase.Slap(Direction.RIGHT_DOWN, force);
            ChangeSprite(Direction.RIGHT_DOWN);
        }
        
    }
    void ChangeSprite(Direction d)
    {
        sd.Hurt(d);
    }
}
