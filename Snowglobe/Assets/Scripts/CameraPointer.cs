using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    public GameObject lastPointedAt;
    public GameObject selectedKeyObj;
    public int selectedKeyId;

    private Camera cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        if(camera){
            cameraObject = camera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;

            Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                lastPointedAt = null;
                if(hit.collider.gameObject.CompareTag("Interactable")){
                    lastPointedAt = hit.collider.gameObject;
                    ItemIdentifier itemIdentifier = lastPointedAt.GetComponent<ItemIdentifier>();
                    if(itemIdentifier){
                        switch (itemIdentifier.type)
                        {
                            case ItemIdentifier.itemType.key:{
                                selectedKeyObj = lastPointedAt;
                                selectedKeyId = lastPointedAt.GetComponent<KeyHandler>().keyID;
                            
                                Debug.Log("KEY "+selectedKeyId+" SELECTED");
                                break;
                            }
                            case ItemIdentifier.itemType.chest:{
                                ChestHandler chestHandler = lastPointedAt.GetComponent<ChestHandler>();
                                Debug.Log("CHEST SELECTED (current key: "+selectedKeyObj+")");
                                if(selectedKeyId>=0){
                                    if(chestHandler.CheckForKeyHole(selectedKeyId)){
                                        Destroy(selectedKeyObj);
                                    }
                                }   
                                selectedKeyId = -1;
                                selectedKeyObj = null;
                                break;
                            }
                            default:
                                selectedKeyId = -1;
                                selectedKeyObj = null;
                                break;
                        }
                    }
                    
                }
            }
        }
    }
}
