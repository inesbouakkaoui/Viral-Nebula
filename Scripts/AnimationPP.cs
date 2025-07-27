using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPP : MonoBehaviour
{
    public Animator animateur;

    public AudioSource sonJoueur; // Utilisé pour les sons de pas
    public AudioSource sonEffets; // Nouveau AudioSource pour les effets, comme les dégâts

    public AudioClip sonDegat;
    public AudioClip sonPas;

    bool enMouvement = false;

    // Start is called before the first frame update
    void Start()
    {
        animateur = GetComponent<Animator>();
        sonJoueur = GetComponent<AudioSource>();
        sonEffets = GetComponents<AudioSource>()[1];
        sonJoueur.clip = sonPas;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Deplacement.peutSeDeplacer)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        enMouvement = !Mathf.Approximately(horizontal, 0) || !Mathf.Approximately(vertical, 0);
        if (enMouvement)
        {
            animateur.SetBool("courir", true);
            animateur.SetBool("attendre", false);
            if (!sonJoueur.isPlaying)
            {
                sonJoueur.Play(); // Jouer le son des pas s'il ne joue pas déjà
            }
        }
        else
        {
            animateur.SetBool("courir", false);
            animateur.SetBool("attendre", true);
            sonJoueur.Stop(); // Arrêter le son des pas si le personnage s'arrête
        }
    }

    public void AnimationDegat()
    {
        animateur.SetTrigger("degat");
        StartCoroutine(ReinitialiserTrigger("degat"));
        sonEffets.PlayOneShot(sonDegat, 1.0f);
    }

    public void AnimationMort()
    {
        animateur.SetTrigger("mort");

    }

    private IEnumerator ReinitialiserTrigger(string triggerName)
    {
        yield return null; // Attendre la fin de la frame actuelle
        animateur.ResetTrigger(triggerName);
    }
}
