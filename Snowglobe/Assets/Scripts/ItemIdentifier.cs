using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdentifier : MonoBehaviour
{
    // Start is called before the first frame update
    public enum itemType
    {
        key,
        chest
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

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select(){
        audioSource.PlayOneShot(selectSound);
        if(!selected && selectable){
            selected = true;
            transform.position += moveOnSelect;
        }
        

    }

    public void unselect(){
        audioSource.PlayOneShot(unselectSound);
        if(selected){
            selected = false;
            transform.position -= moveOnSelect;
        }
        
    }
}
