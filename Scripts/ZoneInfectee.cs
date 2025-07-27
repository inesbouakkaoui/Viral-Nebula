using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneInfectee : MonoBehaviour
{
    public GestionJoueur joueur;

    public float maxTemps = 30f;
    private float currentTemps;
    private int joueursDansZone = 0;


    public Text timerText;
    public Menus menus; // Référence au script Menus pour afficher le menu game over

    void Start()
    {
        currentTemps = maxTemps;
        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (joueursDansZone > 0)
        {
            currentTemps -= Time.deltaTime;
            timerText.text = Mathf.Ceil(currentTemps).ToString();

            if (currentTemps <= 0)
            {
                joueur.animationPerso.AnimationMort();
                Deplacement.peutSeDeplacer = false;
                StartCoroutine(DeclencherGameOver());
            }
        }
    }

    public void AjouterTemps(float extraTemps)
    {
        currentTemps += extraTemps;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                joueursDansZone++;
                if (joueursDansZone == 1)
                {
                    timerText.gameObject.SetActive(true);
                    Debug.Log("Entrée dans la zone infectée.");
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueursDansZone--;
            if (joueursDansZone == 0)
            {
                currentTemps = maxTemps; // Réinitialiser le timer lorsqu'on quitte la zone infectée
                timerText.gameObject.SetActive(false);
                Debug.Log("Sortie de la zone infectée.");
            }
        }
    }

    private IEnumerator DeclencherGameOver()
    {
        yield return new WaitForSeconds(1.5f); // Attendre que l'animation de mort se termine
        menus.GameOver();
    }
}

