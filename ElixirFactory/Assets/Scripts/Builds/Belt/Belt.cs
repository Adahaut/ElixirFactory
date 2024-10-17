using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public BeltItem carriedItem; // Item que le convoyeur transporte
    public Belt[] neighbors = new Belt[4]; // 0: Haut, 1: Bas, 2: Gauche, 3: Droite
    public GridModel gridModel;

    public BeltDirection direction;
    public List<BeltItem> waitingItemList = new List<BeltItem>();

    void Start()
    {
        if (carriedItem != null)
        {
            waitingItemList.Add(carriedItem, NextBelt());
            //MoveItem(carriedItem)
        }
    }

    Belt NextBelt()
    {
        switch (direction)
        {
            case BeltDirection.TOP:
                if (gridModel.grid[transform.position.x, transform.position.y + 1])
                break;
            default:
                return null;
        }
    }

    private void MoveItem(BeltItem item, Belt nextBelt)
    {
        itemsToReceive.Remove(item);
        //Ajouter l'item � la liste du prochain belt
    }

    public void UpdateItemList()
    {

    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Belt neighbor = other.GetComponent<Belt>();
    //    if (neighbor != null)
    //    {
    //        // Calcule la position relative du voisin par rapport � ce convoyeur
    //        Vector3 directionToNeighbor = neighbor.transform.position - transform.position;

    //        if (Mathf.Abs(directionToNeighbor.y) > Mathf.Abs(directionToNeighbor.x))
    //        {
    //            if (directionToNeighbor.y > 0)
    //            {
    //                neighbors[0] = neighbor; // Haut
    //                Debug.Log("Voisin ajout� en haut: " + neighbor.name);
    //            }
    //            else
    //            {
    //                neighbors[1] = neighbor; // Bas
    //                Debug.Log("Voisin ajout� en bas: " + neighbor.name);
    //            }
    //        }
    //        else
    //        {
    //            if (directionToNeighbor.x > 0)
    //            {
    //                neighbors[3] = neighbor; // Droite
    //                Debug.Log("Voisin ajout� � droite: " + neighbor.name);
    //            }
    //            else
    //            {
    //                neighbors[2] = neighbor; // Gauche
    //                Debug.Log("Voisin ajout� � gauche: " + neighbor.name);
    //            }
    //        }
    //    }
    //}


    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    Belt neighbor = other.GetComponent<Belt>();
    //    BeltItem item = other.GetComponent<BeltItem>();

    //    // Si c'est un voisin qui quitte le trigger
    //    if (neighbor != null)
    //    {
    //        for (int i = 0; i < neighbors.Length; i++)
    //        {
    //            if (neighbors[i] == neighbor)
    //            {
    //                neighbors[i] = null;
    //                Debug.Log("Voisin retir�: " + neighbor.name);
    //                break;
    //            }
    //        }
    //    }

    //    // Si c'est l'item qui quitte le trigger, le retirer du convoyeur
    //    if (item != null && item == carriedItem)
    //    {
    //        carriedItem = null;
    //        Debug.Log("Item " + item.name + " a quitt� le convoyeur " + gameObject.name);
    //    }
    //}

    void TransferItem(Belt neighbor)
    {
        if (carriedItem != null && neighbor.carriedItem == null)
        {
            neighbor.carriedItem = carriedItem;
            carriedItem.MoveToPosition(neighbor.transform.position);
            carriedItem = null;
        }
    }

    void Update()
    {
        // V�rifie � chaque mise � jour pour transf�rer les items
        foreach (Belt neighbor in neighbors)
        {
            if (neighbor != null)
            {
                TransferItem(neighbor);
            }
        }
    }
}

public enum BeltDirection
{
    TOP,
    DOWN,
    LEFT,
    RIGHT
}
