using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Case : MonoBehaviour
{
    public Sprite sprite;
    private string name;
    public bool isOccupied = false;
    public void ShowCase()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
