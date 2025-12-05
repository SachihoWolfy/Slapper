using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public List<GameObject> healthImages;
    public int healthCount = 5;
    public GameObject loseScreen;
    public string MenuScene;
    public bool lost = false;

    private void Update()
    {
        UpdateHealth(healthCount);
        if(healthCount <= 0 && !lost)
        {
            StartCoroutine(Lose());
            lost = true;
        }
    }

    public void UpdateHealth(int health)
    {
        for (int i = health; i < healthImages.Count; i++)
        {
            healthImages[i].SetActive(false);
        }
        for (int i = 0; i<health; i++)
        {
            if (i < healthImages.Count)
            {
                healthImages[i].SetActive(true);
            }
        }
    }

    IEnumerator Lose()
    {
        loseScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(MenuScene);
    }
}
