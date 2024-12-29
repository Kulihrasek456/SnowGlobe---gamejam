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


    public float maxVerticalAngle = 70f;
    public float minVerticalAngle = 0f;


    private float currDistance;

    private UnityEngine.Vector2 mousePos = new UnityEngine.Vector2(0f,0f);
    private Camera cameraObject;

    public Transform cameraTarget;

    [Range(0f, 1f)]
    public float moveSpeed = 0.5f; 
    [Range(0f, 1f)]
    public float scrollSpeed = 0.5f;
    [Range(0f, 1f)]
    public float lookSpeed = 0.5f;

    public bool locked = false;


    // Start is called before the first frame update
    void Start()
    {
        currDistance = defaultDistance;
        Camera camera = GetComponent<Camera>();
        if (camera.orthographic){
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize,currDistance,scrollSpeed*40*Time.deltaTime);
            cameraObject = camera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(locked){
            return;
        }
        if(Input.GetMouseButton(1)){
            cameraTarget.LookAt(lookAtPosition);

            mousePos.x = Input.GetAxis("Mouse X");
            mousePos.y = Input.GetAxis("Mouse Y");

            UnityEngine.Vector3 newAngles = cameraTarget.eulerAngles;
            newAngles.y += mousePos.x * lookSensitivity;
            newAngles.x = Mathf.Clamp(
                (-mousePos.y * lookSensitivity) + newAngles.x
                ,minVerticalAngle,maxVerticalAngle);

            cameraTarget.eulerAngles = newAngles;
            
            
        }

        currDistance -= Input.mouseScrollDelta.y*zoomSensitivity;
        currDistance = Mathf.Clamp(currDistance, minDistance, maxDistance);

        cameraTarget.position = lookAtPosition.position - cameraTarget.forward * 80f;

    
        cameraObject.orthographicSize = Mathf.Lerp(cameraObject.orthographicSize,currDistance,scrollSpeed*40*Time.deltaTime);

        transform.position = UnityEngine.Vector3.Lerp(transform.position,cameraTarget.position,moveSpeed*40*Time.deltaTime);
        transform.rotation = UnityEngine.Quaternion.Lerp(transform.rotation, cameraTarget.rotation, lookSpeed*40*Time.deltaTime);
    }
}
