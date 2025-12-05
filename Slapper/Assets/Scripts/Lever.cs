using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Action
{
    public GameObject leverOn;
    public GameObject leverOff;
    public bool isOn = false;

    public GameObject affectedObject;
    private void Start()
    {
        UpdateLeverState();
    }
    public override void Execute()
    {
        ChangeLeverState();
    }

    void ChangeLeverState()
    {
        isOn = !isOn;
        UpdateLeverState();
    }

    void UpdateLeverState()
    {
        affectedObject.SetActive(!isOn);
        leverOff.SetActive(!isOn);
        leverOn.SetActive(isOn);
    }
}
