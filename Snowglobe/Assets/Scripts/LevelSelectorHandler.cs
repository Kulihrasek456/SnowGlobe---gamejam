using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorHandler : MonoBehaviour
{
    private Vector3 targetScale = new(1,1,1);

    private Vector3 targetPosition = new(0,0,0);
    public GameObject switchButton;

    public GameObject World;
    public ParticleSystem SnowParticleSystem;

    public int levelID;
    

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Level"+levelID+"Complete", 0) > 0){
            SnowParticleSystem.Play();
            foreach (Transform child in World.transform)
            {
                SnowVariantHandler snowVariantHandler = child.gameObject.GetComponent<SnowVariantHandler>();
                if(snowVariantHandler){
                    snowVariantHandler.switchToSnowy();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale, 
            targetScale, 
            0.5f);

        switchButton.transform.localPosition = Vector3.Lerp(
            switchButton.transform.localPosition,
            targetPosition,
            0.5f);
    }

    public void select()
    {
        targetScale = new(2,2,2);
        targetPosition = new(0,0.1f,-1f);
    }

    public void unselect()
    {
        targetScale = new(1,1,1);
        targetPosition = new(0,0,0);
    }
}
