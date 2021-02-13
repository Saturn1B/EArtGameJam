using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    Camera playerCamera;
    float targetFOV;
    float fov;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
        targetFOV = playerCamera.fieldOfView;
        fov = targetFOV;
    }

    // Update is called once per frame
    void Update()
    {
        float fovSpeed = 4f;
        fov = Mathf.Lerp(fov, targetFOV, Time.deltaTime * fovSpeed);
        playerCamera.fieldOfView = fov;
    }

    public void SetCameraFOV(float targetFOV)
    {
        this.targetFOV = targetFOV;
    }
}
