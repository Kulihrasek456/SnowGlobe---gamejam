using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CameraPointer : MonoBehaviour
{
    private class ObjectCombo {
        public GameObject gameObject;
        public ItemIdentifier itemIdentifier;
    }

    private ObjectCombo lastObject;    

    private Camera cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        if(camera){
            cameraObject = camera;
        }

        lastObject = new ObjectCombo();
        lastObject.itemIdentifier = null;
        lastObject.gameObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)){
                if(lastObject.itemIdentifier){
                    lastObject.itemIdentifier.unselect();
                }
                if(hit.collider.gameObject.CompareTag("Interactable")){
                    GameObject targetObject = hit.collider.gameObject;
                    ItemIdentifier itemIdentifier = targetObject.GetComponent<ItemIdentifier>();

                    itemIdentifier.select();

                    switch (itemIdentifier.type){
                        case ItemIdentifier.itemType.chest:{
                            if(!lastObject.itemIdentifier){
                                break;
                            }
                            if(lastObject.itemIdentifier.type == ItemIdentifier.itemType.key){
                                ChestHandler chestHandler = targetObject.gameObject.GetComponent<ChestHandler>();
                                if(chestHandler.CheckForKeyHole(lastObject.itemIdentifier.keyID)){
                                    Destroy(lastObject.gameObject);
                                    lastObject.gameObject = null;
                                }
                            }
                        }break;

                        case ItemIdentifier.itemType.key:{
                           
                        }break;

                        default:{

                        }break;
                    }

                    lastObject.gameObject = targetObject;
                    lastObject.itemIdentifier = itemIdentifier;
                }else{
                    lastObject.gameObject = null;
                    lastObject.itemIdentifier = null;
                }
            }
        }
    }
}
