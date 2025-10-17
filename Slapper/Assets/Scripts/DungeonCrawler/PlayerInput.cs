using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode turnLeft = KeyCode.Q;
    public KeyCode turnRight = KeyCode.E;
    public KeyCode punch = KeyCode.P;

    public PunchingBagInput bagInput;

    PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        ProcessBagInput();

        if (Input.GetKeyUp(forward) || bagInput == PunchingBagInput.FORWARD_TILT) { controller.MoveForward(); }
        if (Input.GetKeyUp(backward) || bagInput == PunchingBagInput.BACK_SLAM) { controller.MoveBackward(); }
        if (Input.GetKeyUp(left)) { controller.MoveLeft(); }
        if (Input.GetKeyUp(right)) { controller.MoveRight(); }
        if (Input.GetKeyUp(turnLeft) || bagInput == PunchingBagInput.LEFT_SLAM) { controller.RotateLeft(); }
        if (Input.GetKeyUp(turnRight) || bagInput == PunchingBagInput.RIGHT_SLAM) { controller.RotateRight(); }
        if (Input.GetKeyUp(punch) || bagInput == PunchingBagInput.FORWARD_SLAM) { controller.Punch(); }
    }
    private void ProcessBagInput()
    {
        //implement
        bagInput = PunchingBagInput.STATIONARY;
    }
}
public enum PunchingBagInput
{
    STATIONARY,
    FORWARD_TILT,
    FORWARD_SLAM,
    BACK_TILT,
    BACK_SLAM,
    LEFT_TILT,
    LEFT_SLAM,
    RIGHT_TILT,
    RIGHT_SLAM
}

// All Tilts could be movement (Except back)
// Back is throwing.
