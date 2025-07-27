using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionJoueur : MonoBehaviour
{
    public Inventaire inventaire;
    public Menus menu;
    public AnimationPP animationPerso;

    public int maxVie = 3;
    public int currentVie;
    public GameObject[] imagesVie;

    public bool dansSAS = false;
    public bool estMort = false;

    // Start is called before the first frame update
    void Start()
    {
        currentVie = maxVie;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SAS"))
        {
            dansSAS = true;
            Debug.Log("Le joueur est dans le SAS.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SAS"))
        {
            dansSAS = false;
            Debug.Log("Le joueur a quitté le SAS.");
        }
    }

    public void UpdateImagesVie()
    {
        for (int i = 0; i < imagesVie.Length; i++)
        {
            if (i < currentVie)
            {
                imagesVie[i].SetActive(true);
            }
            else
            {
                imagesVie[i].SetActive(false);
            }
        }
    }

    public void GagnerVie()
    {
        if (currentVie < maxVie)
        {
            currentVie += 1;
            UpdateImagesVie();
        }
    }

    public void PerdreVie() //Methode qui fait perdre une vie
    {
        if (currentVie > 0)
        {
            currentVie -= 1;
            UpdateImagesVie();
            animationPerso.AnimationDegat();
        }
        if (currentVie <= 0 && !estMort)
        {
            estMort = true;
            JouerAnimationMort();
        }
    }

    public void JouerAnimationMort()
    {
        animationPerso.AnimationMort();
        StartCoroutine(DeclencherGameOver());
    }

    private IEnumerator DeclencherGameOver()
    {
        yield return new WaitForSeconds(1.5f); // Attendre que l'animation de mort se termine
        menu.GameOver();
    }
}
