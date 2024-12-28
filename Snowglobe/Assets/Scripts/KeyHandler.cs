using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyHandler : MonoBehaviour
{
    public int keyID;
    public List<GameObject> gems;
    public GameObject gemHole;

    // Start is called before the first frame update
    void Start()
    {
        if(keyID>=0 && keyID<gems.Count){
            Instantiate(gems[keyID],gemHole.transform);
        }
        else{
            Debug.LogError("INVALID KEY ID");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
