using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : Action
{
    // Start is called before the first frame update
    public override void Execute()
    {
        Debug.Log("Killing Enemy");
        Destroy(gameObject);
    }
}
