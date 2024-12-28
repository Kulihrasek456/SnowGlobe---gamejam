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
    public Transform prefabDisplay; 
    public GameObject storedPrefab;

    private Material baseMaterial;

    public AudioClip openSound;
    private AudioSource audioSource;


    private int locksLeft = 0;

    public bool reloadNow;

    void changeMaterials(GameObject parent, Material material){
        MeshRenderer renderer = parent.GetComponent<MeshRenderer>();
        if (renderer != null){
            Material[] materialsCopy = renderer.materials;
            for (int i = 0; i < materialsCopy.Length; i++)
            {
                materialsCopy[i] = material;
            }
            renderer.materials = materialsCopy;
        }
        foreach (Transform child in parent.transform){
            changeMaterials(child.gameObject, material);
        }   
    }

    void Awake() {
        baseMaterial = Resources.Load<Material>("Materials/MaterialChest");
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        locksLeft = 0;
        for (int i = 0; i < lockIDs.Count; i++){
            foreach (Transform child in locks[i].transform)
            {
                Destroy(child.gameObject);
            }
            if(lockIDs[i] >= 0 ){
                locksLeft++;
                GameObject instance = Instantiate(gems[lockIDs[i]],locks[i]);
                instance.transform.Rotate(new Vector3(0f,0f,-90f));
            }
        }
        GameObject prefabDisplayInstance = Instantiate(storedPrefab,prefabDisplay);
        changeMaterials(prefabDisplayInstance,baseMaterial);
        prefabDisplayInstance.transform.localScale = new(0.5f,0.5f,0.5f);
        prefabDisplayInstance.transform.localPosition=Vector3.zero;
        prefabDisplayInstance.transform.rotation = transform.rotation;
        prefabDisplayInstance.tag = "Untagged";
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
            newInstance.transform.position = transform.position;
            audioSource.PlayOneShot(openSound);
            gameObject.SetActive(false);
            Destroy(gameObject, openSound.length);
        }
        return result;
    }
    
}
