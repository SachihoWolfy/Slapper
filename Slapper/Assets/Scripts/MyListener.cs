/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 * Modifications for InterfaceLab 2020 to move a cube
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MyListener : MonoBehaviour
{
    GameObject cubeModifier;
    float xAccel = 0;
    float yAccel = 0;
    float zAccel = 0;
    float xGyro = 0;
    float yGyro = 0;
    float zGyro = 0;
    public float sensitivity = 0.0002f;
    public float zDrift = 0.2f;
    float zDriftCalculation;
    private float prevZ;
    private float curZ;
    public float error = 1f;
    public bool calibrated;
    public float acceleration;
    bool check1;

    void Start() // Start is called before the first frame update
    {
        cubeModifier = GameObject.Find("Cube");
    }
    void Update() // Update is called once per frame
    {
        prevZ = Mathf.Abs(transform.rotation.z);
        if (calibrated)
        {
            RotateCube();
        }
        else
        {

            Calibrate();
        }
    }
    void OnMessageArrived(string msg)
    {
        //Debug.Log(msg);
        string[] stringNumbers = msg.Split(',', StringSplitOptions.RemoveEmptyEntries);
        float[] floatNumbers = stringNumbers.Select(float.Parse).ToArray();
        xAccel = floatNumbers[0];
        yAccel = floatNumbers[1];
        zAccel = floatNumbers[2];
        xGyro = floatNumbers[3];
        yGyro = floatNumbers[4];
        zGyro = floatNumbers[5];
        Debug.Log("XA: " + xAccel + " YA: " +  yAccel + " ZA: " +  zAccel + "   XG: " + xGyro + " YG: " + yGyro + " ZG: " + zGyro);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
    void Calibrate()
    {
        curZ = Mathf.Abs(transform.rotation.z);
        acceleration = (curZ - prevZ);
        if (acceleration > error)
        {
            zDriftCalculation += zDrift;
        }
        else if (acceleration < -error)
        {
            zDriftCalculation -= zDrift;
        }
        else
        {
            calibrated = true;
        }
        cubeModifier.transform.eulerAngles = Vector3.zero;
    }

    void RotateCube()
    {
        if (cubeModifier != null)
        {

            Vector3 r = new Vector3(xGyro * sensitivity, yGyro * sensitivity, zGyro * sensitivity);
            cubeModifier.transform.eulerAngles = cubeModifier.transform.eulerAngles + r;
        }
    }
}
