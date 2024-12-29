using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorHandler : MonoBehaviour
{
    private Vector3 targetScale = new(1,1,1);

    private Vector3 targetPosition = new(0,0,0);
    public GameObject switchButton;

    public float pointedAt;
    

    // Start is called before the first frame update
    void Start()
    {
        
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
        targetPosition = new(0,0,-5);
    }

    public void unselect()
    {
        targetScale = new(1,1,1);
        targetPosition = new(0,0,0);
    }
}
