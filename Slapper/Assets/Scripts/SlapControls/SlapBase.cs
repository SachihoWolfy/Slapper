using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SlapBase : MonoBehaviour
{
    float maxLean = 30f;
    float maxScale = 1.3f;
    float minScale = 0.7f;
    float maxForce = 100f;
    float maxOffset = 1f;
    public float slapTimer = 0f;
    public float slapTime = 0.5f;
    float lastForce = 0f;
    public Direction lastSlapDirection = Direction.NONE;
    public Vector3 lastDisiredRotation;
    SpriteDebug sd;
    private void Start()
    {
        if (sd == null)
        {
            sd = gameObject.GetComponent<SpriteDebug>();
        }
    }

    private void Update()
    {
        slapTimer += Time.deltaTime;
        RotateDummy(lastSlapDirection);
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, -maxLean, maxLean), Mathf.Clamp(transform.eulerAngles.y, -maxLean, maxLean), Mathf.Clamp(transform.eulerAngles.z, -maxLean, maxLean));
    }

    void RotateDummy(Direction d)
    {
        float leftwards = 0f;
        float upwards = 0f;
        switch (d)
        {
            case Direction.LEFT:
                leftwards = 1f;
                upwards = 0f;
                break;
            case Direction.LEFT_UP:
                leftwards = 1f;
                upwards = 1f;
                break;
            case Direction.UP:
                leftwards = 0f;
                upwards = 1f;
                break;
            case Direction.RIGHT_UP:
                leftwards = -1f;
                upwards = 1f;
                break;
            case Direction.RIGHT:
                leftwards = -1f;
                upwards = 0f;
                break;
            case Direction.RIGHT_DOWN:
                leftwards = -1f;
                upwards = -1f;
                break;
            case Direction.DOWN:
                leftwards = 0f;
                upwards = -1f;
                break;
            case Direction.LEFT_DOWN:
                leftwards = 1f;
                upwards = -1f;
                break;
            default:
                break;
        }
        HandleRotation(leftwards, upwards, lastForce);
    }
    void HandleRotation(float leftwards, float upwards, float force)
    {
        Vector3 disiredRotation = new Vector3((upwards * maxLean) * force, (leftwards * maxLean)*force, (leftwards * maxLean)*force);
        Vector3 disiredScale = new Vector3(Mathf.Clamp((-upwards * force) + 1, minScale, maxScale), Mathf.Clamp((upwards*force) + 1, minScale, maxScale), 1);
        Vector3 disiredPosition = new Vector3(-leftwards * force * maxOffset, 0, 0);
        lastDisiredRotation = disiredRotation;
        // if (slapTimer > slapTime) { transform.rotation = Quaternion.identity;}
        float realTime = slapTime * force;
        if (slapTimer > 0 && slapTimer < realTime)
        {
            Vector3 curAngle = new Vector3(
                Mathf.LerpAngle(transform.eulerAngles.x, disiredRotation.x, slapTimer / realTime),
                Mathf.LerpAngle(transform.eulerAngles.y, disiredRotation.y, slapTimer / realTime),
                Mathf.LerpAngle(transform.eulerAngles.z, disiredRotation.z, slapTimer / realTime)
                );
            transform.eulerAngles = curAngle;
            transform.localScale = Vector3.Lerp(transform.localScale, disiredScale, slapTimer / realTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, disiredPosition, slapTimer / realTime);
        }
        if ((slapTimer > 0 && slapTimer > realTime)){
            Vector3 curAngle = new Vector3(
                Mathf.LerpAngle(transform.eulerAngles.x, 0, (slapTimer - realTime) / realTime),
                Mathf.LerpAngle(transform.eulerAngles.y, 0, (slapTimer - realTime) / realTime),
                Mathf.LerpAngle(transform.eulerAngles.z, 0, (slapTimer - realTime) / realTime)
                );
            transform.eulerAngles = curAngle ;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, (slapTimer - realTime) / realTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, (slapTimer - realTime) / realTime);
            sd.Default();
        }
    }

    public void Slap(Direction d, float force)
    {
        slapTimer = 0f;
        lastSlapDirection = d;
        lastForce = Mathf.Clamp(force, 0f, maxForce);
    }
}
public enum Direction
{
    NONE,
    LEFT,
    LEFT_UP,
    UP,
    RIGHT_UP,
    RIGHT,
    RIGHT_DOWN,
    DOWN,
    LEFT_DOWN
}