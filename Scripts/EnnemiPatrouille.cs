using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiPatrouille : MonoBehaviour
{
    public GestionJoueur joueurGestion;

    public Transform[] pointsDePatrouille; // Points de patrouille
    public Transform joueur; // Référence au joueur
    public Animator ennemiAnimator;
    public float distancePourchasse = 10.0f; // Distance à partir de laquelle l'ennemi pourchasse le joueur
    public float distanceAttaque = 1.0f; // Distance à partir de laquelle l'ennemi attaque le joueur
    public float delaiEntreAttaques = 2.0f;

    public AudioSource audioRespiration;
    public AudioClip sonRespiration;
    public AudioClip sonSaccade;


    public AudioSource audioPas;
    public AudioClip sonAttaque;
    public AudioClip sonPas;

    private NavMeshAgent agent;
    private int destinationActuelle = 0;
    private bool joueurEnVue = false;
    private bool patrouilleActive = false;
    private bool peutAttaquer = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; // Permet à l'agent de ne pas ralentir près des points de patrouille
        // Jouer le son de marche en boucle
        audioPas.clip = sonPas;
        audioPas.loop = true;
        audioPas.Play();

        audioRespiration.clip = sonRespiration;
        audioRespiration.loop = true;
        audioRespiration.Play();
    }

    public void ActiverPatrouille()
    {
        patrouilleActive = true;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (pointsDePatrouille.Length == 0)
            return;

        agent.destination = pointsDePatrouille[destinationActuelle].position;
        destinationActuelle = (destinationActuelle + 1) % pointsDePatrouille.Length;
    }

    void Update()
    {
        if (!patrouilleActive)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        float distanceToPlayer = Vector3.Distance(transform.position, joueur.position);

        if (distanceToPlayer <= distancePourchasse && !joueurGestion.dansSAS)
        {
            joueurEnVue = true;
            agent.SetDestination(joueur.position);
            ennemiAnimator.SetBool("courir", true);

            // Changer le son à celui de poursuite
            if (audioRespiration.clip != sonSaccade)
            {
                audioPas.clip = sonSaccade;
                audioPas.Play();
            }
        }
        else if (joueurEnVue)
        {
            joueurEnVue = false;
            GotoNextPoint();
            ennemiAnimator.SetBool("courir", false);

            if (audioPas.clip != sonPas)
            {
                audioPas.clip = sonPas;
                audioPas.Play();
            }
            if (audioRespiration.clip != sonRespiration)
            {
                audioPas.clip = sonRespiration;
                audioPas.Play();
            }
        }

        if (joueurEnVue && distanceToPlayer <= distanceAttaque && peutAttaquer && !joueurGestion.dansSAS)
        {
            peutAttaquer = false;
            ennemiAnimator.SetTrigger("attaquer");
            // Jouer le son d'attaque
            audioPas.PlayOneShot(sonAttaque);
            joueurGestion.PerdreVie();
            StartCoroutine(DelaiEntreAttaques());
        }
    }

    private IEnumerator DelaiEntreAttaques()
    {
        yield return new WaitForSeconds(delaiEntreAttaques);
        peutAttaquer = true;
    }
}
