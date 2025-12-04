using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : Action
{
    public override void Execute()
    {
        Application.Quit();
    }
}
