using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public PNJ pnj;

    public GameObject inventaire;

    public GameObject jeu;
    public AudioSource sonJeu;

    public GameObject ecranTitre;
    public AudioSource sonTitre;


    public GameObject ecranGameOver;
    public AudioSource sonGameOver;
    public AudioClip sfxGameOver;
    public AudioClip themeGameOver;

    public GameObject ecranGagnant;
    public AudioSource sonGagnant;
    public AudioClip themeGagnant;

    public Button recommencerBtn1;
    public Button recommencerBtn2;
    public Button quitterBtn1;
    public Button quitterBtn2;

    public bool enJeu;
    private bool gameOver = false; // Variable de contrôle pour GameOver
    private bool win = false; // Variable de contrôle pour Win



    // Start is called before the first frame update
    void Start()
    {
        ecranGameOver.SetActive(false);
        ecranGagnant.SetActive(false);
        pnj.dialogueUI.SetActive(false);
        inventaire.SetActive(false);
        sonJeu.Stop();


        ecranTitre.SetActive(true);
        sonTitre.Play();
        pnj.audioSource.Stop();

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Commencer();
    }

    public void Commencer()
    {
        if (!enJeu && Input.anyKeyDown)
        {
            ecranTitre.SetActive(false);
            sonTitre.Pause();
            sonJeu.Play();
            enJeu = true;
            Time.timeScale = 1;
            pnj.DemarrerCourse(); // Appeler la méthode pour démarrer le son de course
        }
    }

    public void Recommencer()
    {
        SceneManager.LoadScene("Niveau principal");
    }

    public void Quitter()
    {
#if UNITY_EDITOR
        // Arrête le jeu dans l'éditeur de Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Ferme l'application
            Application.Quit();
#endif
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;

            jeu.SetActive(false);
            ecranTitre.SetActive(false);
            ecranGagnant.SetActive(false);
            inventaire.SetActive(false);

            ecranGameOver.SetActive(true);
            StartCoroutine(JouerSonGameOver());
        }
    }

    public void Win()
    {
        if (!win)
        {
            win = true;

            jeu.SetActive(false);
            ecranTitre.SetActive(false);
            ecranGameOver.SetActive(false);
            inventaire.SetActive(false);

            ecranGagnant.SetActive(true);
            StartCoroutine(JouerSonGagnant());
        }
    }

    private IEnumerator JouerSonGameOver()
    {
        if (sfxGameOver != null)
        {
            sonGameOver.clip = sfxGameOver;
            sonGameOver.Play();
            yield return new WaitForSeconds(sfxGameOver.length);
        }

        if (themeGameOver != null)
        {
            sonGameOver.clip = themeGameOver;
            sonGameOver.Play();
        }
    }

    private IEnumerator JouerSonGagnant()
    {
        if (themeGagnant != null)
        {
            if (!sonGagnant.enabled)
            {
                sonGagnant.enabled = true;
            }

            sonGagnant.clip = themeGagnant;
            sonGagnant.Play();
            yield return new WaitForSeconds(themeGagnant.length);
        }
    }

}
