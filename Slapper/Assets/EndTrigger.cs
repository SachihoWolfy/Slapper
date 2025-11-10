using UnityEngine;

public class EndTrigger : SubAction
{
    public UIWin gameManager;
    public GameObject CompleteUI;

    // Update is called once per frame
    public override void Execute()
    {

        gameManager.UIComplete();  
        CompleteUI.GetComponentInParent<Animator>().SetBool("Activate", true);
        Debug.Log("This is Working");
    }
    
}
