using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChestHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public void SetTimeout(System.Action action, float delay)
    {
        StartCoroutine(ExecuteAfterDelay(action, delay));
    }

    private IEnumerator ExecuteAfterDelay(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public List<Transform> locks;
    public List<GameObject> gems;
    public List<int> lockIDs;
    public Transform prefabDisplay; 
    public GameObject storedPrefab;

    private Material baseMaterial;

    public AudioClip openSound;
    public AudioClip unlockSound;
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
        foreach (Transform child in prefabDisplay)
        {
            Destroy(child.gameObject);
        }
        GameObject prefabDisplayInstance = Instantiate(storedPrefab,prefabDisplay);
        changeMaterials(prefabDisplayInstance,baseMaterial);
        prefabDisplayInstance.transform.localScale = new(0.5f,0.5f,0.5f);
        prefabDisplayInstance.transform.localPosition=Vector3.zero;
        prefabDisplayInstance.transform.rotation = transform.rotation;
        prefabDisplayInstance.tag = "Untagged";

        ItemIdentifier itemIdentifier = prefabDisplayInstance.GetComponent<ItemIdentifier>();
        if(itemIdentifier){
            if(itemIdentifier.type == ItemIdentifier.itemType.key){
                prefabDisplayInstance.transform.Rotate(new(100,0,-90));
                prefabDisplayInstance.transform.localPosition+=new Vector3(0,0.5f,-1.2f);
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
                reloadNow = true;
                locksLeft--;
                result=true;
                audioSource.PlayOneShot(unlockSound);
                break;
            }
        }
        if(locksLeft == 0){
            
            audioSource.PlayOneShot(openSound);
            SetTimeout(()=>{
                GameObject newInstance = Instantiate(storedPrefab,transform.parent);
                newInstance.transform.position = transform.position;
                Destroy(gameObject);
            },openSound.length);
        }
        return result;
    }
    
}
