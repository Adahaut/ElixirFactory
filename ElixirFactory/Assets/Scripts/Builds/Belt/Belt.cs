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


    private void Start()
    {

        StartCoroutine(StartFunction());
    }

    //Temp function
    public IEnumerator StartFunction()
    {
        yield return new WaitForSeconds(0.2f);

        OnPlace(gridModel);

    }

    //Call this function at Belt spawn
    public void OnPlace(GridModel model)
    {
        gridModel = model;
        gridModel.grid[(int)transform.position.x, (int)transform.position.y].GetComponent<Case>().SetObjectInCase(this);
    }

    //Add current item to the next belt list
    public Belt NextBelt()
    {
        int x = 0;
        int y = 0;

        if (direction == BeltDirection.LEFT) x = -1;
        if (direction == BeltDirection.RIGHT) x = 1;

        if (direction == BeltDirection.TOP) y = 1;
        if (direction == BeltDirection.DOWN) y = -1;

        //Rajouter condition > grid size
        if ((int)transform.position.x + x < 0 || (int)transform.position.y + y < 0)
            return null;

        if (gridModel.grid[(int)transform.position.x + x, (int)transform.position.y + y].TryGetComponent<Case>(out Case c) && c.GetObject() is Belt)
        {
            return c.GetObject() as Belt;
        }

        return null;
    }

    private void Update()
    {
        if (currentItemOnBelt != null && !currentItemOnBelt.isMoving && NextBelt() != null && NextBelt().waitingItem.item == null)
        {
            NextBelt().waitingItem = new(currentItemOnBelt, this);
        }

        CheckList();
    }

    public void ItemLeftBelt()
    {
        currentItemOnBelt = null;
    }

    public void CheckList()
    {
        if(currentItemOnBelt != null || waitingItem.item == null) return;

        currentItemOnBelt = waitingItem.item;
        currentItemOnBelt.SetDestination(this.transform.position);
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
