using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class teste : MonoBehaviour   
{
    private InputDevice targetDeviceLeft;
    private InputDevice targetDeviceRight;

    public bool apertado;
    void Start()
    {
        
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

       
        if(devices.Count > 0)
        {
            targetDeviceLeft = devices[1];
            targetDeviceRight = devices[2];

        }

    }

    void Update()
    {
        //targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        //if(primaryButtonValue == true)
        //{
        //    Debug.Log("pressing primary button");
        //}

        targetDeviceLeft.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton);
        if(triggerButton == true)
        {
            apertado = true;
        }
        else
        {
            apertado = false;
        }

    }
}
