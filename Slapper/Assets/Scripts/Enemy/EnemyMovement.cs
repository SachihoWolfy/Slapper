using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public bool activeMovement = true;
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
        if (activeMovement)
        {
            if(target == null)
            {
                target = pc.transform;
            }
            if (framecount == 0)
            {
                framecount = movecount;
                MoveToLocation(target.position);
            }
            MoveToLocation(target.position);
        }
        DoLatch();
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
        // Snap
        if (Mathf.Abs(targetPos.z - transform.position.z) <= distanceThreshold && Mathf.Abs(targetPos.x - transform.position.x) <= distanceThreshold )
        {
            finalPos = target.position + target.forward * distanceThreshold;
            transform.LookAt(target.position);
            latched = true;
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
    public bool latched = false;
    float latchTick = 3f;
    float latchTimer = 3f;
    public void DoLatch()
    {
        if (latched)
        {
            latchTimer -= Time.deltaTime;
        }
        else
        {
            latchTimer = latchTick;
        }

        if (latchTimer <= 0)
        {
            latchTimer = latchTick;
            pc.TakeDamage();
        }
    }
}
