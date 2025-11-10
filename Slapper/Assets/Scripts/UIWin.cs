using UnityEngine;

public class UIWin : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CompleteUI;

    private void Update()
    {
        
    }

    public void UIComplete ()
    {
        CompleteUI.SetActive(true);
        CompleteUI.GetComponentInParent<Animator>().SetBool("Activate", true);
        Debug.Log("This is Working");
    }
}
