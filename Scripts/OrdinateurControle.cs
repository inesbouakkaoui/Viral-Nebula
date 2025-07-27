using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinateurControle : MonoBehaviour
{
    public Menus menus; // Référence au script Menus

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            menus.Win(); // Afficher le menu Win
        }
    }
}
