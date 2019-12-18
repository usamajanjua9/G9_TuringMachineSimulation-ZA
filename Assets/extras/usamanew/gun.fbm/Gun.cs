using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject camera;
    [HideInInspector]
    float targetXRotation, targetYRotation;
    [HideInInspector]
    float targetXRotationV, targetYRotationV;

    public GameObject shell;
    public Transform shellSpawnPos, bulletSpawnPos;
    public float rotateSpeed = .3f, holdheight = -.5f, holdSide = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        targetXRotation = Mathf.SmoothDamp(targetXRotation, FindObjectOfType<mouselook>().xRot,ref targetXRotationV,rotateSpeed);
        targetYRotation = Mathf.SmoothDamp(targetYRotation, FindObjectOfType<mouselook>().yRot, ref targetYRotationV, rotateSpeed);
        transform.position = camera.transform.position + Quaternion.Euler(0, targetYRotation, 0)*new Vector3(holdSide, holdheight, 0);
        float clampedX = Mathf.Clamp(targetXRotation, -70, 80);
        transform.rotation = Quaternion.Euler(-clampedX, targetYRotation, rotateSpeed);
    }
    void Shoot() {
        if (Input.GetKeyDown("space"))
        { Fire(); }
        else if(Input.GetKeyDown("space")) { Fire(); }
    }
    void Fire() {
        GameObject shellCopy = Instantiate<GameObject>(shell, shellSpawnPos.position, Quaternion.identity) as GameObject;
        RaycastHit variable;
        bool status = Physics.Raycast(bulletSpawnPos.position,bulletSpawnPos.forward,out variable,100);
        if (status)
        {
            Debug.Log(variable.collider.gameObject.name);
        }
    }
}
