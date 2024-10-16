using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Case : MonoBehaviour
{
    public Sprite sprite;
    private string name;
    public int x, y;
    public bool isOccupied = false;
    public float gCost, hCost;
    public float fCost => gCost + hCost;
    public Case parent;
    public void ShowCase()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
