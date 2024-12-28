using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChestHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Transform> locks;
    public List<GameObject> gems;
    public List<int> lockIDs;
    public GameObject storedPrefab;


    private int locksLeft = 0;

    public bool reloadNow;

    void Start()
    {
        locksLeft = 0;
        for (int i = 0; i < lockIDs.Count; i++){
            foreach (Transform child in locks[i].transform)
            {
                Destroy(child.gameObject);
            }
            if(lockIDs[i] >= 0 ){
                locksLeft++;
                GameObject instance = Instantiate(gems[lockIDs[i]],locks[i]);
                instance.transform.eulerAngles = new Vector3(0f,0f,-90f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(reloadNow){
            Start();
            reloadNow = false;
        }
    }

    public bool CheckForKeyHole(int keyID){
        bool result = false;
        for (int i = 0; i < lockIDs.Count; i++){   
            if(lockIDs[i] == keyID){
                lockIDs[i] = -1;
                Debug.Log("LCOK OPENED");
                reloadNow = true;
                locksLeft--;
                result=true;
                break;
            }
        }
        if(locksLeft == 0){
            GameObject newInstance = Instantiate(storedPrefab,transform.parent);
            newInstance.transform.position += transform.position;
            Destroy(gameObject);
        }
        return result;
    }
}
