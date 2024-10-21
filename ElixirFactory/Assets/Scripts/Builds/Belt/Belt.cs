using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : BuildProperties
{
    public GridModel gridModel;

    public BeltItem currentItemOnBelt;
    public BeltDirection direction;
    public List<BeltItem> waitingItemList = new List<BeltItem>();

    private void Start()
    {
        StartCoroutine(StartFunction());
    }

    //Temp function
    IEnumerator StartFunction()
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
    Belt NextBelt()
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
        if(currentItemOnBelt != null && !currentItemOnBelt.isMoving && NextBelt() != null && NextBelt().currentItemOnBelt == null)
        {
            NextBelt().waitingItemList.Add(currentItemOnBelt);
            currentItemOnBelt = null;
        }

        CheckList();
    }

    public void CheckList()
    {
        if(currentItemOnBelt != null || waitingItemList.Count == 0) return;

        currentItemOnBelt = waitingItemList[0];
        waitingItemList[0].SetDestination(this.transform.position);
        waitingItemList.RemoveAt(0);
    }

}

public enum BeltDirection
{
    TOP,
    DOWN,
    LEFT,
    RIGHT
}
