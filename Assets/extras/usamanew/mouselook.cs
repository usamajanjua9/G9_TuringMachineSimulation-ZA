using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float lookSensitivity = 2f, lookSmoothDamp = .5f;
    [HideInInspector]
    public float yRot, xRot;
    [HideInInspector]
    public float currentY, currentX;
    [HideInInspector]
    public float yRotationV, xRotationV;
    // Start is called before the first frame update    
    // Update is called once per frame
    void LateUpdate()
    {
        yRot += Input.GetAxis("Mouse X") * lookSensitivity;
        xRot += Input.GetAxis("Mouse Y") * lookSensitivity;

        currentX = Mathf.SmoothDamp(currentX, xRot, ref xRotationV, lookSmoothDamp);
        currentY = Mathf.SmoothDamp(currentX, yRot, ref xRotationV, lookSmoothDamp);

        xRot = Mathf.Clamp(xRot, -80, 80);
        transform.rotation = Quaternion.Euler(-currentX, currentY, 0);
    }
}
