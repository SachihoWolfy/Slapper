using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoor : Action
{
    public override void Execute()
    {
        Debug.Log("Door Broken");
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
