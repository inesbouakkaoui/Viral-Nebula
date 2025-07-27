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
            return; // Si le personnage ne peut pas se d�placer, sortir de la m�thode Update

        // R�cup�rer les entr�es utilisateur pour le d�placement et la rotation
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Cr�er le vecteur de d�placement horizontal bas� sur les entr�es utilisateur
        Vector3 deplacement = transform.forward * vertical * vitesseDeplacement * Time.deltaTime;

        // Appliquer la rotation du personnage
        transform.Rotate(Vector3.up * horizontal * vitesseRotation * Time.deltaTime);

        // Appliquer la gravit�
        if (controleurPersonnage.isGrounded)
        {
            vitesseVerticale.y = 0; // Si le personnage est au sol, r�initialiser la vitesse verticale
        }
        else
        {
            vitesseVerticale.y += gravite * Time.deltaTime; // Sinon, appliquer la gravit�
        }

        // Ajouter la vitesse verticale au vecteur de d�placement
        deplacement.y = vitesseVerticale.y * Time.deltaTime;

        // D�placer le personnage
        controleurPersonnage.Move(deplacement);
    }
}
