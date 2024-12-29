using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPointer : MonoBehaviour
{
    private Camera cameraObject;
    public Transform cameraTarget;

    public GameObject lastObject;

    [Range(0f, 1f)]
    public float moveSpeed = 0.5f; 
    // Start is called before the first frame update
    void Start()
    {
        cameraObject = gameObject.GetComponent<Camera>();
        lastObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);
        
        if(Input.GetMouseButtonDown(0)){
            
            if(lastObject.GetComponent<LevelSelectorHandler>()){
                lastObject.GetComponent<LevelSelectorHandler>().unselect();
            }
            lastObject = transform.parent.gameObject;
            if (Physics.Raycast(ray, out hit)){
                GameObject targetObject = hit.collider.gameObject;
                if(targetObject.GetComponent<SceneSwitcher>()){
                    targetObject.GetComponent<SceneSwitcher>().switchScene();
                    return;
                }

                lastObject = targetObject;
                LevelSelectorHandler levelSelectorHandler = lastObject.GetComponent<LevelSelectorHandler>();
                levelSelectorHandler.select();     

                cameraTarget.transform.localPosition = Vector3.zero;
                cameraTarget.transform.localPosition += new Vector3(lastObject.transform.localPosition.x,0,0);       

            }
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition,cameraTarget .transform.localPosition,moveSpeed);
    }
}
