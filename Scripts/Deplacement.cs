using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float vitesseDeplacement = 7.5f;
    [SerializeField] private float vitesseRotation = 120f;
    [SerializeField] private float gravite = -9.81f;
    private Vector3 vitesseVerticale; // Vitesse verticale du personnage

    private CharacterController controleurPersonnage;

    public static bool peutSeDeplacer = false;

    void Start()
    {
        controleurPersonnage = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!peutSeDeplacer)
            return; // Si le personnage ne peut pas se déplacer, sortir de la méthode Update

        // Récupérer les entrées utilisateur pour le déplacement et la rotation
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Créer le vecteur de déplacement horizontal basé sur les entrées utilisateur
        Vector3 deplacement = transform.forward * vertical * vitesseDeplacement * Time.deltaTime;

        // Appliquer la rotation du personnage
        transform.Rotate(Vector3.up * horizontal * vitesseRotation * Time.deltaTime);

        // Appliquer la gravité
        if (controleurPersonnage.isGrounded)
        {
            vitesseVerticale.y = 0; // Si le personnage est au sol, réinitialiser la vitesse verticale
        }
        else
        {
            vitesseVerticale.y += gravite * Time.deltaTime; // Sinon, appliquer la gravité
        }

        // Ajouter la vitesse verticale au vecteur de déplacement
        deplacement.y = vitesseVerticale.y * Time.deltaTime;

        // Déplacer le personnage
        controleurPersonnage.Move(deplacement);
    }
}
