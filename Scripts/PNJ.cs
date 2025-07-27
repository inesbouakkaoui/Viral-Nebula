using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PNJ : MonoBehaviour
{
    public Menus menus;

    public Transform joueur; // Référence au joueur
    public Transform ennemi; // Référence à l'ennemi
    public Transform pointDepart; // Point de départ où le PNJ retourne après le dialogue
    public float distanceSeuil = 1.5f; // Distance à laquelle le PNJ atteint le joueur
    public GameObject dialogueUI; // Interface utilisateur pour le dialogue
    public Animator pnjAnimator;
    public Animator ennemiAnimator;

    public AudioSource audioSource;
    public AudioClip sonCourse;
    public AudioClip sonDialogue;
    public AudioClip sonMort;

    private NavMeshAgent agent;
    private bool dialogueDeclenche = false;
    private bool retourPointDepart = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(joueur.position);
        pnjAnimator.SetBool("courir", true);
        if (menus.enJeu) // Vérifier si le jeu est en cours avant de jouer le son de course
        {
            JouerSon(sonCourse);
        }
    }

    public void DemarrerCourse()
    {
        if (menus.enJeu)
        {
            JouerSon(sonCourse);
        }
    }

    void Update()
    {
        // Vérifier si le PNJ est proche du joueur
        if (!dialogueDeclenche && Vector3.Distance(transform.position, joueur.position) < distanceSeuil)
        {
            // Arrêter le mouvement et déclencher le dialogue
            agent.isStopped = true;
            pnjAnimator.SetBool("courir", false);
            pnjAnimator.SetTrigger("parler"); // Déclencher l'animation de dialogue
            dialogueUI.SetActive(true); // Afficher l'UI du dialogue
            dialogueDeclenche = true;
            JouerSon(sonDialogue);
        }

        if (dialogueDeclenche && Input.GetKeyDown(KeyCode.Space))
        {
            FinDialogue();
        }

        // Retourner le PNJ à son point de départ après le dialogue
        if (retourPointDepart && agent.remainingDistance < 0.1f)
        {
            pnjAnimator.SetBool("courir", false); // Désactiver l'animation "courir"
            JouerSon(sonDialogue);
            agent.isStopped = true; // Arrêter le PNJ
            ennemi.GetComponent<Ennemi>().AttaquerPNJ(transform);
            retourPointDepart = false;
        }
    }

    public void FinDialogue()
    {
        dialogueUI.SetActive(false);
        agent.isStopped = false;
        agent.SetDestination(pointDepart.position);
        pnjAnimator.SetBool("courir", true); // Activer l'animation "courir"
        pnjAnimator.ResetTrigger("parler");
        retourPointDepart = true;
        JouerSon(sonCourse);
    }

    public void Mourir()
    {
        pnjAnimator.SetTrigger("mourir"); // Déclencher l'animation de mort
        JouerSon(sonMort); // Jouer le son de mort
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
