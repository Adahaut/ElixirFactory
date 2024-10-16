using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public BeltItem carriedItem; // Item que le convoyeur transporte
    private Belt[] neighbors = new Belt[4]; // Pour stocker les voisins

    void Start()
    {
        // Si un item est déjà sur le convoyeur au début du jeu, on lui assigne sa position initiale
        if (carriedItem != null)
        {
            carriedItem.MoveToPosition(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Belt neighbor = other.GetComponent<Belt>();
        if (neighbor != null)
        {
            // Ajouter le voisin selon sa position relative
            if (other.transform.position.y > transform.position.y) neighbors[0] = neighbor; // Haut
            else if (other.transform.position.y < transform.position.y) neighbors[1] = neighbor; // Bas
            else if (other.transform.position.x < transform.position.x) neighbors[2] = neighbor; // Gauche
            else if (other.transform.position.x > transform.position.x) neighbors[3] = neighbor; // Droite

            TransferItem(neighbor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Belt neighbor = other.GetComponent<Belt>();
        if (neighbor != null)
        {
            // Retirer le voisin de la liste
            for (int i = 0; i < neighbors.Length; i++)
            {
                if (neighbors[i] == neighbor)
                {
                    neighbors[i] = null;
                    break;
                }
            }
        }
    }

    void TransferItem(Belt neighbor)
    {
        // Vérifier si le voisin a un item et que ce conveyor en transporte un
        if (carriedItem != null && neighbor.carriedItem == null)
        {
            // Transférer l'item au voisin
            neighbor.carriedItem = carriedItem;
            carriedItem.MoveToPosition(neighbor.transform.position); // Déplacer l'item vers le nouveau conveyor
            carriedItem = null; // Ce convoyeur ne transporte plus d'item
        }
    }

    void Update()
    {
        // Vérifie à chaque mise à jour pour transférer les items
        foreach (Belt neighbor in neighbors)
        {
            if (neighbor != null)
            {
                TransferItem(neighbor);
            }
        }
    }
}
