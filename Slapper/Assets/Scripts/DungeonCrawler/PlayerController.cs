using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool smoothTransition = false;
    public float transitionSpeed = 10f;
    public float transitionRotationSpeed = 500f;
    public int gridSize = 2;
    public LayerMask punchableLayerMask;
    public LayerMask collisionLayerMask;
    Vector3 targetGridPos;
    Vector3 prevTargetGridPos;
    Vector3 targetRotation;
    public FistAnimator fistAnimator;

    private void Start()
    {
        targetGridPos = Vector3Int.RoundToInt(transform.position);
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //This if statement is collision
        if (true)
        {
            prevTargetGridPos = targetGridPos;
            Vector3 targetPosition = targetGridPos;
            if (targetRotation.y > 270f && targetRotation.y < 361f) targetRotation.y = 0f;
            if (targetRotation.y < 0f) targetRotation.y = 270f;

            if (!smoothTransition)
            {
                transform.position = targetPosition;
                transform.rotation = Quaternion.Euler(targetRotation);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(targetRotation), Time.deltaTime * transitionRotationSpeed);
            }
        }
    }

    public void RotateLeft() { if (AtRest) targetRotation -= Vector3.up * 90f; }
    public void RotateRight() { if (AtRest) targetRotation += Vector3.up * 90f; }
    public void MoveForward() {
        if (AtRest && isForwardAvailable()) targetGridPos += transform.forward * gridSize;
        else if (AtRest && !isForwardAvailable()) Punch();
    }
    public void MoveBackward() { if (AtRest) targetGridPos -= transform.forward * gridSize; }
    public void MoveLeft() { if(AtRest) targetGridPos -= transform.right * gridSize; }
    public void MoveRight() { if (AtRest) targetGridPos += transform.right * gridSize; }

    bool AtRest
    {
        get
        {
            if ((Vector3.Distance(transform.position, targetGridPos) < 0.05f) && (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
                return true;
            else
                return false;
        }
    }
    public void Punch()
    {
        Debug.Log("Punching...");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, gridSize+1, punchableLayerMask)){
            Action punchAction = hit.transform.gameObject.GetComponent<Action>();
            if (punchAction != null)
            {
                Debug.Log("Trying Action: " +  punchAction.ToString());
                punchAction.Execute();
                fistAnimator.IsPunching = true;
            }
            else
            {
                Debug.LogError("Punch Action is NULL");
            }
        }
        else
        {
            fistAnimator.IsPushing = true;
        }
    }
    public bool isForwardAvailable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, gridSize, collisionLayerMask))
        {
            return false;
        }
        return true;
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * (gridSize+1), Color.yellow);
    }
    public void Throw()
    {
        //Implement
    }
}
