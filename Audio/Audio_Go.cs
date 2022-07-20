using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Go : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip clip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("bola"))
        {
            TocaAudio.TocadordeAudio(audioS, clip);
        }
    }
}
