using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    static PlayerController pc;
    public float speed;
    public float distanceThreshold = 1.2f;
    public int movecount = 8;
    private int framecount;
    private void Update()
    {
        if (pc == null)
        {
            pc = FindObjectOfType<PlayerController>();
        }
        if (framecount == 0)
        {
            framecount = movecount;
            MoveToLocation(target.position);
        }
        MoveToLocation(target.position);
    }
    public void MoveToLocation(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        Vector3 finalPos = target.position;
        float speedThisFrame = speed * Time.deltaTime;
        // x
        if (targetPos.x > transform.position.x)
        {
            finalPos.x = transform.position.x + speedThisFrame;
            if (targetPos.x - finalPos.x < 0)
            {
                finalPos.x = targetPos.x;
            }
        }
        else if(targetPos.x < transform.position.x)
        {
            finalPos.x = transform.position.x - speedThisFrame;
            if (targetPos.x - finalPos.x > 0)
            {
                finalPos.x = targetPos.x;
            }
        }
        // z

        if (targetPos.z > transform.position.z)
        {
            finalPos.z = transform.position.z + speedThisFrame;
            if (targetPos.z - finalPos.z < 0)
            {
                finalPos.z = targetPos.z;
            }
        }
        else if (targetPos.z < transform.position.z)
        {
            finalPos.z = transform.position.z - speedThisFrame;
            if (targetPos.z - finalPos.z > 0)
            {
                finalPos.z = targetPos.z;
            }
        }
        if (Mathf.Abs(targetPos.z - transform.position.z) <= distanceThreshold && Mathf.Abs(targetPos.x - transform.position.x) <= distanceThreshold )
        {
            finalPos = target.position + target.forward * distanceThreshold;
            transform.LookAt(target.position);
        }
        else
        {
            if (!isForwardAvailable())
            {
                finalPos = transform.position;
            }
            transform.LookAt(finalPos);
        }
        transform.position = finalPos;
    }
    public bool isForwardAvailable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pc.gridSize, pc.collisionLayerMask))
        {
            return false;
        }
        return true;
    }
}
