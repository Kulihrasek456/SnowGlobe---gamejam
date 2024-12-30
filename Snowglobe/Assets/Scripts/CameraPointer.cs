using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CameraPointer : MonoBehaviour
{

    public void SetTimeout(System.Action action, float delay)
    {
        StartCoroutine(ExecuteAfterDelay(action, delay));
    }

    private IEnumerator ExecuteAfterDelay(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    private class ObjectCombo {
        public GameObject gameObject;
        public ItemIdentifier itemIdentifier;
    }

    private ObjectCombo lastObject;    

    private Camera cameraObject;

    public GameObject SnowBall;
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
                                    lastObject.itemIdentifier = null;
                                }
                            }
                        }break;

                        case ItemIdentifier.itemType.key:{
                           
                        }break;

                        case ItemIdentifier.itemType.win:{
                            SnowBall.GetComponent<SnowBallHandler>().win();
                        }break;

                        default:{

                        }break;
                    }

                    lastObject.gameObject = targetObject;
                    lastObject.itemIdentifier = itemIdentifier;
                }else{
                    if(hit.collider.gameObject.GetComponent<SceneSwitcher>()){
                        gameObject.GetComponent<OrbitCamera>().locked = true;
                        hit.collider.gameObject.GetComponent<SceneSwitcher>().switchScene(cameraObject);
                    }


                    lastObject.gameObject = null;
                    lastObject.itemIdentifier = null;
                }
            }
        }
    }
}
