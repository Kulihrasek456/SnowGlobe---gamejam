using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform lookAtPosition;
    public float lookSensitivity = 5f;
    public float zoomSensitivity = 5f;
    
    public float maxDistance = 40f;
    public float minDistance = 2f;

    public float defaultDistance = 25f;


    private float maxVerticalAngle = 70f;
    private float minVerticalAngle = 0f;


    private float currDistance;

    private UnityEngine.Vector2 mousePos = new UnityEngine.Vector2(0f,0f);
    private Camera cameraObject;


    // Start is called before the first frame update
    void Start()
    {
        currDistance = defaultDistance;
        Camera camera = GetComponent<Camera>();
        if (camera.orthographic){
            camera.orthographicSize = currDistance;
            cameraObject = camera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            transform.LookAt(lookAtPosition);

            mousePos.x = Input.GetAxis("Mouse X");
            mousePos.y = Input.GetAxis("Mouse Y");

            UnityEngine.Vector3 newAngles = transform.eulerAngles;
            newAngles.y += mousePos.x * lookSensitivity;
            newAngles.x = Mathf.Clamp(
                (-mousePos.y * lookSensitivity) + newAngles.x
                ,minVerticalAngle,maxVerticalAngle);

            transform.eulerAngles = newAngles;
            
            
        }

        currDistance -= Input.mouseScrollDelta.y*zoomSensitivity;
        currDistance = Mathf.Clamp(currDistance, minDistance, maxDistance);

        transform.position = lookAtPosition.position - transform.forward * 80f;

    
        cameraObject.orthographicSize = currDistance;
    }
}
