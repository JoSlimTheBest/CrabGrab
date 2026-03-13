using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaNoise : MonoBehaviour
{
    public AudioClip seaNoise;


    public void FixedUpdate()
    {
        if(!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = seaNoise;
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
    }
}
