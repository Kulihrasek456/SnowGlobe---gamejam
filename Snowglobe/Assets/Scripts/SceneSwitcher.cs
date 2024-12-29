using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public string targetScene;
    // Start is called before the first frame update

    public void switchScene(){
        SceneManager.LoadScene(targetScene);
    }
}
