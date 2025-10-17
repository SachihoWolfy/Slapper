using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public virtual void Execute()
    {
        Debug.LogError("DEFAULT ACTION DETECTED");
        //implement
    }
}
