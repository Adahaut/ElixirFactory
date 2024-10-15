using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Case : MonoBehaviour
{
    public Sprite sprite;
    private string name;
    private bool isOccupied;
    public void ShowCase()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
