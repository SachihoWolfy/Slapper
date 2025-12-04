using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneDoor : Action
{
    public string targetSceneName;
    // Start is called before the first frame update
    public override void Execute()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
