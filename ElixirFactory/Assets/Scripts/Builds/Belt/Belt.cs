using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : BuildProperties
{
    public GridModel gridModel;

    public BeltItem currentItemOnBelt;
    public BeltDirection direction;
    public BeltItemReference waitingItem;

    [Serializable]
    public struct BeltItemReference
    {
        public BeltItem item;
        public Belt belt;

        public BeltItemReference(BeltItem item, Belt belt)
        {
            this.item = item;
            this.belt = belt;
        }
    }


    private void Awake()
    {
        gridModel = GridModel.instance;
    }

    //Temp function
    public void TryTransferItem()
    {
        // Vérifiez si le convoyeur peut transférer un item
        BuildProperties nextBuild = NextBuild();

        if (nextBuild is IItemReceiver receiver && currentItemOnBelt != null)
        {
            // Transférer l'item immédiatement vers le bâtiment
            receiver.ReceiveItem(currentItemOnBelt);
            currentItemOnBelt = null;
            Debug.Log("Item transféré depuis le convoyeur après placement du bâtiment");
        }
    }
    //Add current item to the next belt list
    public BuildProperties NextBuild()
    {
        int x = 0;
        int y = 0;

        if (direction == BeltDirection.LEFT) x = -1;
        if (direction == BeltDirection.RIGHT) x = 1;
        if (direction == BeltDirection.TOP) y = 1;
        if (direction == BeltDirection.DOWN) y = -1;

        if ((int)transform.position.x + x < 0 || (int)transform.position.y + y < 0)
            return null;

        if (gridModel.grid[(int)transform.position.x + x, (int)transform.position.y + y].TryGetComponent<Case>(out Case c) && c.GetObject())
        {
            BuildProperties nextBuild = c.GetObject();

            if (nextBuild is Belt)
            {
                return nextBuild;
            }
            else if (nextBuild is IItemReceiver receiver && receiver.CanReceiveItem() && nextBuild.GetRecipeSet())
            {
                return nextBuild;
            }
        }

        return null;
    }


    private void Update()
    {
        BuildProperties nextBuild = NextBuild();
        Debug.Log("Next build is: " + nextBuild);

        if (nextBuild is Belt belt)
        {
            if (currentItemOnBelt != null && !currentItemOnBelt.isMoving && belt.waitingItem.item == null)
            {
                Debug.Log("Transferring item to next belt");
                belt.waitingItem = new BeltItemReference(currentItemOnBelt, this);
            }
        }
        else if (nextBuild is IItemReceiver receiver && currentItemOnBelt != null)
        {
            Debug.Log("Transferring item to building");
            receiver.ReceiveItem(currentItemOnBelt);
            currentItemOnBelt = null;
        }

        CheckList();
    }



    public void ItemLeftBelt()
    {
        currentItemOnBelt = null;
    }

    public void CheckList()
    {
        // Ne faire quelque chose que si le convoyeur n'a pas déjà un item et qu'un item attend d'être pris en charge
        if (currentItemOnBelt != null || waitingItem.item == null) return;

        // Le convoyeur récupère l'item en attente
        currentItemOnBelt = waitingItem.item;
        currentItemOnBelt.SetDestination(this.transform.position);
    
        // Une fois l'item récupéré, le retirer du convoyeur précédent
        waitingItem.belt.ItemLeftBelt();
        waitingItem.item = null;
    }


}

public enum BeltDirection
{
    TOP,
    DOWN,
    LEFT,
    RIGHT
}
