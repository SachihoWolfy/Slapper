using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAction : MonoBehaviour
{
    public virtual void Execute()
    {
        Debug.LogError("DEFAULT SUBACTION DETECTED");
        //implement
    }
}
