using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdentifier : MonoBehaviour
{
    // Start is called before the first frame update
    public enum itemType
    {
        key,
        chest,
        win
    }
    public itemType type;

    public int keyID;

    private bool selected;

    public bool selectable;

    private Vector3 moveOnSelect = new(0f,5f,0f);

    public List<GameObject> requiredItems;
    public bool requiredInOrder;

    public AudioClip selectSound;
    public AudioClip unselectSound;
    private AudioSource audioSource;

    private Vector3 targetPosition;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        targetPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition,targetPosition,0.2f*40*Time.deltaTime);
    }

    public void select(){
        if(selectSound){
            audioSource.PlayOneShot(selectSound);
        }
        if(!selected && selectable){
            selected = true;
            targetPosition += moveOnSelect;
        }
        

    }

    public void unselect(){
        if(unselectSound){
            audioSource.PlayOneShot(unselectSound);
        }
        if(selected){
            selected = false;
            targetPosition -= moveOnSelect;
        }
        
    }
}
