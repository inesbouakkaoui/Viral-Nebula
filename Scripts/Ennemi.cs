using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemi : MonoBehaviour
{
    public Transform pointDeDepart; // Point de départ ou de patrouille
    public Animator ennemiAnimator;
    public Transform pnj;
    public float distancePnj = 1.0f;
    public bool retourPointDeDepart = false;

    public AudioSource audioRespiration;
    public AudioClip sonRespiration;
    public AudioClip sonSaccade;


    public AudioSource audioPas;
    public AudioClip sonAttaque;
    public AudioClip sonPas;

    private NavMeshAgent agent;
    private bool pnjTue = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Jouer le son de marche en boucle
        audioPas.clip = sonPas;
        audioPas.loop = true;
        audioPas.Play();

        audioRespiration.clip = sonRespiration;
        audioRespiration.loop = true;
        audioRespiration.Play();
    }

    public void AttaquerPNJ(Transform pnjTransform)
    {
        pnj = pnjTransform;
        agent.SetDestination(pnj.position);
        ennemiAnimator.SetBool("courir", true);

        // Changer le son à celui de poursuite
        if (audioRespiration.clip != sonSaccade)
        {
            audioPas.clip = sonSaccade;
            audioPas.Play();
        }
    }

    void Update()
    {
        if (!pnjTue && pnj != null)
        {
            float distanceToPnj = Vector3.Distance(transform.position, pnj.position);

            if (distanceToPnj <= distancePnj)
            {
                TuerPnj();
            }
        }

        if (retourPointDeDepart && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) // Détruire le GameObject après avoir tué le PNJ
        {
            Destroy(gameObject);

            // Envoyez un message pour activer la patrouille des autres ennemis
            EnvoyerMessagePatrouille("ActiverPatrouille");

            Deplacement.peutSeDeplacer = true;
        }
    }

    private void TuerPnj()
    {
        ennemiAnimator.SetTrigger("attaquer");
        // Jouer le son d'attaque
        audioPas.PlayOneShot(sonAttaque);
        pnjTue = true;
        Invoke("RetournerPointDeDepart", 0.5f);
        pnj.GetComponent<PNJ>().Mourir();
    }

    void RetournerPointDeDepart()
    {
        agent.SetDestination(pointDeDepart.position);
        ennemiAnimator.SetBool("courir", true);
        retourPointDeDepart = true;

        if (audioRespiration.clip != sonRespiration)
        {
            audioPas.clip = sonRespiration;
            audioPas.Play();
        }
    }

    void EnvoyerMessagePatrouille(string message)
    {
        EnnemiPatrouille[] patrouilleurs = FindObjectsOfType<EnnemiPatrouille>();
        foreach (EnnemiPatrouille patrouilleur in patrouilleurs)
        {
            patrouilleur.SendMessage(message);
        }
    }
}
