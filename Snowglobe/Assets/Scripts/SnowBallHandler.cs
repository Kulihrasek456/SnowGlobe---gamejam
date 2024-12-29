using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowBallHandler : MonoBehaviour
{
    public GameObject World;
    public ParticleSystem SnowParticleSystem;
    public GameObject returnButton;
    public GameObject returnButtonTarget;

    private bool won;
    void Update(){
        if(won){
            returnButton.transform.localPosition = Vector3.Lerp(returnButton.transform.localPosition, returnButtonTarget.transform.localPosition, 0.1f*40*Time.deltaTime);
        }
    }
    public void win(){
        SnowParticleSystem.Play();

        foreach (Transform child in World.transform)
        {
            SnowVariantHandler snowVariantHandler = child.gameObject.GetComponent<SnowVariantHandler>();
            if(snowVariantHandler){
                snowVariantHandler.switchToSnowy();
            }
        }

        won = true;
        
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"Complete", 1);
    }
}
