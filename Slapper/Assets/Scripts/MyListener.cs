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
    public Vector3 AcceleratorDeadzone = Vector3.zero;
    public Vector3 GryoDeadzone = Vector3.zero;
    public float sensitivity = 0.0002f;
    public float sensitivityA = 0.0000002f;
    public float zDrift = 0.2f;
    float zDriftCalculation;
    private float prevZ;
    private float curZ;
    public float error = 1f;
    public bool calibrated;
    public float acceleration;
    float timer;
    float recalibrateTime = 0.1f;
    bool check1;

    void Start() // Start is called before the first frame update
    {
        cubeModifier = GameObject.Find("Cube");
    }
    void Update() // Update is called once per frame
    {
        RotateCube();
        if (xGyro == 0 && yGyro == 0 && zGyro == 0)
        {
            timer += Time.deltaTime;
        }
        else { timer = 0; }
        if (timer > recalibrateTime)
        {
            timer = 0;
            cubeModifier.transform.rotation = Quaternion.identity;
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
        if(Mathf.Abs(xGyro) < GryoDeadzone.x)
        {
            xGyro = 0;
        }
         if(Mathf.Abs(yGyro) < GryoDeadzone.y)
        {
            yGyro = 0;
        }
        if (Mathf.Abs(zGyro) < GryoDeadzone.z)
        {
            zGyro= 0;
        }
        if (Mathf.Abs(xAccel) < AcceleratorDeadzone.x)
        {
            xAccel = 0;
        }
        if (Mathf.Abs(yAccel) < AcceleratorDeadzone.y)
        {
            yAccel = 0;
        }
        if (Mathf.Abs(zAccel) < AcceleratorDeadzone.z)
        {
            zAccel = 0;
        }
        Debug.Log("XA: " + xAccel + " YA: " +  yAccel + " ZA: " +  zAccel + "   XG: " + xGyro + " YG: " + yGyro + " ZG: " + zGyro);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }

    void RotateCube()
    {
        if (cubeModifier != null)
        {

            Vector3 r = new Vector3(xGyro * sensitivity, yGyro * sensitivity, zGyro * sensitivity);
            cubeModifier.transform.eulerAngles = cubeModifier.transform.eulerAngles + r;
            Vector3 p = new Vector3(xAccel * sensitivityA, yAccel * sensitivityA, zAccel * sensitivityA);
            cubeModifier.transform.position = Vector3.zero + p;
        }
    }
}
