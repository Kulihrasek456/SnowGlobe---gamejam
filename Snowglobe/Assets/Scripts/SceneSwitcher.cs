using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public string targetScene;
    private Camera cameraObject;
    // Start is called before the first frame update

    void Update(){
        if(cameraObject){
            cameraObject.orthographicSize = Mathf.Lerp(cameraObject.orthographicSize,0,15*Time.deltaTime);
            if(Mathf.Abs(cameraObject.orthographicSize) < 0.05f){
                SceneManager.LoadScene(targetScene);
            }
        }
    }

    public void switchScene(Camera targetCameraObject){
        if(!targetCameraObject){
            SceneManager.LoadScene(targetScene);
        }else{
            cameraObject = targetCameraObject;
        }
    }
}
