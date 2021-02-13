using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    [SerializeField] private float mouseSensitivity;

    public Transform Player;

    GameObject Light;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Light = transform.GetChild(0).gameObject;
        Light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FPSRotate();
        if(Input.GetKeyDown(KeyCode.L) && Light.activeSelf == true)
        {
            Light.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.L) && Light.activeSelf == false)
        {
            Light.SetActive(true);
        }
    }

    private void FPSRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localEulerAngles = new Vector3(rotationX, 0f, 0f);

        Player.Rotate(Vector3.up * mouseX);
    }
}
