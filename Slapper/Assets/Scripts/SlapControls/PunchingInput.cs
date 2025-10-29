using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PunchingInput : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform cubeListener;
    public float inputCooldown = 0.4f;
    private float inputCooldownTimer = 0;
    private PunchingBagInput prevInput = PunchingBagInput.STATIONARY;

    public float xThreshold = 5f;
    public float zThreshold = 5f;
    public float recoilRecoveryCooldown = 0.4f;
    PunchingBagInput recoilState = PunchingBagInput.BACK_TILT;
    float recoilTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cubeListener = FindObjectOfType<MyListener>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotationToInput();
    }
    void RotationToInput()
    {
        PunchingBagInput input = ProcessInput();

        if (inputCooldownTimer > 0 || input == prevInput)
        {
            inputCooldownTimer -= Time.deltaTime;
            if ((prevInput != input && prevInput == PunchingBagInput.STATIONARY) || recoilState == input)
            {
                inputCooldownTimer = recoilRecoveryCooldown;
                recoilState = input;
            }
            else
            {
                recoilState = PunchingBagInput.BACK_SLAM;
            }
        }
        else
        {
            inputCooldownTimer = inputCooldown;
            playerInput.bagInput = input;
        }
        prevInput = input;
    }
    PunchingBagInput ProcessInput()
    {
        PunchingBagInput input = PunchingBagInput.STATIONARY;
        if(cubeListener.localEulerAngles.x > xThreshold)
        {
            if (cubeListener.localEulerAngles.x < 180)
            {
                input = PunchingBagInput.FORWARD_TILT;
            }
            else if (cubeListener.localEulerAngles.x < 360 - xThreshold)
            {
                input = PunchingBagInput.BACK_TILT;
            }
        }
        if (cubeListener.localEulerAngles.z > zThreshold)
        {
            if (cubeListener.localEulerAngles.z < 180)
            {
                input = PunchingBagInput.LEFT_TILT;
            }
            else if (cubeListener.localEulerAngles.z < 360 - zThreshold)
            {
                input = PunchingBagInput.RIGHT_TILT;
            }
        }
        return input;
    }
}
