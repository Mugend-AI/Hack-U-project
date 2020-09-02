using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{

    public AudioClip sound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Onbreak()
    {

        audioSource.PlayOneShot(sound);
    }
}
