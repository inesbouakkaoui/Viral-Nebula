using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PorteSalleControle : MonoBehaviour
{
    public Animator porteAnimator;
    public Inventaire inventaire; // Référence à l'inventaire du joueur

    public GameObject codeManquantUI;

    private void Start()
    {
        codeManquantUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventaire.AvoirTousLesChiffresCode())
            {
                porteAnimator.SetBool("joueurProche", true);
            }
            else
            {
                codeManquantUI.SetActive (true);
                Debug.Log("Le joueur n'a pas tous les chiffres du code.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            porteAnimator.SetBool("joueurProche", false);
            codeManquantUI.SetActive(false);
        }
    }
}
