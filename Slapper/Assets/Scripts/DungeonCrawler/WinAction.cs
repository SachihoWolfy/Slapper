using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinAction : Action
{
    public GameObject winScreen;
    public string nextSceneName;
    bool executed = false;
    public override void Execute()
    {
        if (!executed)
        {
            executed = true;
            StartCoroutine(WinSequence());
        }
    }

    IEnumerator WinSequence()
    {
        winScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneName);
    }
}
