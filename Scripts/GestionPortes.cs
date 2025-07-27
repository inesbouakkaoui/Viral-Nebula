using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPortes : MonoBehaviour
{
    public Animator porteAnimator;
    public AudioSource audioSource;
    public AudioClip sonOuverture;
    public AudioClip sonFermeture;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is missing on " + gameObject.name);
            }
        }

        if (sonOuverture == null || sonFermeture == null)
        {
            Debug.LogError("Audio clips are missing on " + gameObject.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            porteAnimator.SetBool("joueurProche", true);
            JouerSon(sonOuverture);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            porteAnimator.SetBool("joueurProche", false);
            JouerSon(sonFermeture);
        }
    }

    private void JouerSon(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
