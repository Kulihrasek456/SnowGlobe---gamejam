using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool initialized;
    public List<AudioClip> musicPLaylist;

    private AudioSource audioSource;
    void Start()
    {
        if(initialized){
            Debug.Log("Allready playing music");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        initialized = true;

        audioSource = GetComponent<AudioSource>();

        if (musicPLaylist.Count > 0)
        {
            StartCoroutine(PlaySoundsSequentially());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator PlaySoundsSequentially()
    {
        foreach (AudioClip clip in musicPLaylist)
        {
            audioSource.PlayOneShot(clip); 
            yield return new WaitForSeconds(clip.length);
        }
        StartCoroutine(PlaySoundsSequentially());
    }
}
