using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public List<MeshRenderer> glowMeshes;
    private Material glowMaterial;
    private Material transMaterial;
    public bool setGlowVar;

    public bool isSelected;
    private bool glowing;
    // Start is called before the first frame update

    void Awake()
    {
        glowMaterial = Resources.Load<Material>("Materials/Glow");
        transMaterial = Resources.Load<Material>("Assets/Materials/Transparent.mat");
    }

    void Update(){
        setGlow(setGlowVar);
    }

    public void setSelected(){
        isSelected = true;
    }
    
    public void setGlow(bool state){
        if(state == true && glowing == false){
            Debug.Log("Glowing");
            for (int i = 0; i < glowMeshes.Count; i++)
            {
                Debug.Log(glowMeshes[i].materials[i]);
                glowMeshes[i].materials[glowMeshes[i].materials.Length-1] = glowMaterial;
            }
            glowing = true;
        }
        if(state == false && glowing == true){
            Debug.Log("Not Glowing");
            for (int i = 0; i < glowMeshes.Count; i++)
            {
                Debug.Log(glowMeshes[i].materials[i]);
                glowMeshes[i].materials[glowMeshes[i].materials.Length-1] = transMaterial;
            }
            glowing = false;
        }
    }

}
