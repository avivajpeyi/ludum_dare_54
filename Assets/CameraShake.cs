using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CameraShake : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        // Get the Cinemachine Impulse Source component attached to the Virtual Camera.
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera(float amplitude, float frequency)
    {
        
        // Trigger the camera shake effect.
        Vector3 impulse = new Vector3(amplitude, amplitude, amplitude);
        impulseSource.GenerateImpulse(impulse);
    }
}