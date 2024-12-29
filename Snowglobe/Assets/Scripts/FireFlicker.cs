using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 1.0f; 
    public float maxIntensity = 2.0f; 

    private float targetIntensity; 

    private void Start()
    {
        if (fireLight == null)
        {
            fireLight = GetComponent<Light>();
        }
        SetNewTargetIntensity();
    }

    private void Update()
    {
        fireLight.intensity = Mathf.Lerp(fireLight.intensity, targetIntensity, 0.5f*40*Time.deltaTime);

        if (Mathf.Abs(fireLight.intensity - targetIntensity) < 0.05f)
        {
            SetNewTargetIntensity();
        }
    }

    private void SetNewTargetIntensity()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }
}
