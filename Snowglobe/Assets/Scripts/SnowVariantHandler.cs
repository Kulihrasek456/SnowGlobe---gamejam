using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowVariantHandler : MonoBehaviour
{
    public GameObject normalVariant;
    public GameObject snowyVariant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchToSnowy(){
        Vector3 scale    = normalVariant.transform.localScale;
        Vector3 position = normalVariant.transform.localPosition;
        Quaternion rotation = normalVariant.transform.localRotation;
        foreach (Transform child in transform){
            Destroy(child.gameObject);
        }
        GameObject newInstance =  Instantiate(snowyVariant,transform);
        newInstance.transform.localScale = scale;
        newInstance.transform.localPosition = position;
        newInstance.transform.localRotation = rotation;
    }
}
