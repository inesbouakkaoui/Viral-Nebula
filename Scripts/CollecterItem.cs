using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecterItem : MonoBehaviour
{
    public AudioSource bruitCollecte;

    private void Start()
    {
        bruitCollecte = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GestionJoueur joueur = other.GetComponent<GestionJoueur>();
            Inventaire inventaire = joueur.inventaire;

            if (bruitCollecte != null)
            {
                bruitCollecte.Play();
            }

            if (CompareTag("KitSante"))
            {
                inventaire.AddKitSante();
            }
            else if (CompareTag("Recharge"))
            {
                inventaire.AddRecharge();
            }
            else if (CompareTag("Code"))
            {
                inventaire.AddCodeChiffre();
            }

            StartCoroutine(DetruireApresSon(0.3f));
        }
    }

    private IEnumerator DetruireApresSon(float delai)
    {
        if (bruitCollecte != null && bruitCollecte.clip != null)
        {
            yield return new WaitForSeconds(delai);
        }
        Destroy(gameObject);
    }
}
