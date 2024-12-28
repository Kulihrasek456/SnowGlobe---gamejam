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

    public List<GameObject> requiredItems;
    public bool requiredInOrder;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
