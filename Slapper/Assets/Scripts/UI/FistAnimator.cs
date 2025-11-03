using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAnimator : MonoBehaviour
{
    private Animator anim;
    public bool IsPunching = false;
    public bool IsPushing = false;
    public bool IsDancing = false;
    public bool IsLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePunchingStatus();
        UpdatePushingStatus();
        if (Input.GetKeyUp(KeyCode.O))
        {
            IsDancing = !IsDancing;
        }
        anim.SetBool("IsDancing", IsDancing);
    }

    void UpdatePunchingStatus()
    {
        if (IsPunching)
        {
            IsLeft = !IsLeft;
        }
        anim.SetBool("IsPunching", IsPunching);
        anim.SetBool("IsLeft", IsLeft);
        IsPunching = false;
    }
    void UpdatePushingStatus()
    {
        anim.SetBool("IsPushing", IsPushing);
        IsPushing = false;
    }
}
