using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public GestionJoueur joueur;
    public ZoneInfectee zoneInfectee;

    // Quantités des items
    public int kitSanteQuantite = 0;
    public int rechargeQuantite = 0;
    public int codeChiffreQuantite = 0;

    // UI Elements
    public GameObject inventaireUI;

    public Text kitSanteQuantiteTxt;
    public Button utiliserKitSanteBtn;


    public Text rechargeQuantiteTxt;
    public Button utiliserRechargeBtn;

    public Text codeChiffreQuantiteTxt;

    // Audio
    public AudioSource utiliserSon; // AudioSource pour jouer les sons d'utilisation

    private void Start()
    {
        inventaireUI.SetActive(false);
    }

    void Update()
    {
        UpdateInventaireUI();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OuvrirInventaire();
        }
    }

    public void OuvrirInventaire()
    {
        bool isActive = inventaireUI.activeSelf;
        inventaireUI.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0; // Mettre le jeu en pause ou reprendre
    }

    void UpdateInventaireUI()
    {
        kitSanteQuantiteTxt.text = kitSanteQuantite.ToString();
        utiliserKitSanteBtn.interactable = kitSanteQuantite > 0;

        rechargeQuantiteTxt.text = rechargeQuantite.ToString();
        utiliserRechargeBtn.interactable = rechargeQuantite > 0;

        codeChiffreQuantiteTxt.text = codeChiffreQuantite.ToString();
    }

    public void AddKitSante()
    {
        kitSanteQuantite++;
        UpdateInventaireUI();
    }

    public void AddRecharge()
    {
        rechargeQuantite++;
        UpdateInventaireUI();
    }

    public void AddCodeChiffre()
    {
        codeChiffreQuantite++;
        UpdateInventaireUI();
    }

    public void Soin()
    {
        if (kitSanteQuantite > 0)
        {
            kitSanteQuantite--;
            Debug.Log("Kit de Santé utilisé");
            joueur.GagnerVie();
            UpdateInventaireUI();
            JouerSon(utiliserSon);
        }
    }

    public void RechargerMasque()
    {
        if (rechargeQuantite > 0)
        {
            rechargeQuantite--;
            Debug.Log("Recharge utilisée");
            zoneInfectee.AjouterTemps(10f); // Ajouter 10 secondes au timer de la zone infectée
            UpdateInventaireUI();
            JouerSon(utiliserSon);
        }
        //zone infectee +10s
    }

    private void JouerSon(AudioSource audioSource)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public bool AvoirTousLesChiffresCode()
    {
        return codeChiffreQuantite == 5;
    }
}
