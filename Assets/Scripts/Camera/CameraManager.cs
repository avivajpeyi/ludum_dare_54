using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera[] vcams;

    [SerializeField] bool debugMode = false;

    // current cam idx
    private int camIdx = 0;

    private void Awake()
    {
        base.Awake();
        PlayerPerkEvents.eventIncreaseView += IncreaseCamera;
    }


    // Start is called before the first frame update
    void Start()
    {
        // find all vcam objects
        vcams = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();

        if (vcams == null)
            throw new System.Exception("vcams list is null");

        // sort by lens ortho size
        Array.Sort(vcams,
            (x, y) => x.m_Lens.OrthographicSize.CompareTo(y.m_Lens.OrthographicSize));
        camIdx = 0;
        SetActiveCamera();
    }


    private void Update()
    {
        if (!debugMode && Input.GetKeyDown(KeyCode.U))
            IncreaseCamera();
    }

    private void OnDestroy() {
        PlayerPerkEvents.eventIncreaseView -= IncreaseCamera;
    }

    public void IncreaseCamera(float increment = 1f)
    {
        Debug.Log("IncreaseCamera()");
        camIdx = Mathf.Clamp(camIdx + 1, 0, vcams.Length - 1);
        SetActiveCamera();
    }
    
    public bool AtMaxZoom()
    {
        return camIdx == vcams.Length - 1;
    }


    private void SetActiveCamera()
    {
        // deprioritize all cams otther than the camIdx one
        for (int i = 0; i < vcams.Length; i++)
        {
            if (i == camIdx)
                vcams[i].Priority = 10;
            else
                vcams[i].Priority = 0;
        }
    }
    
    public void Shake(CinemachineImpulseSource impulseSource, float magnitude = 1f)
    {
        Debug.Log("Shake()");
        impulseSource.GenerateImpulseWithForce(magnitude);
    }
}