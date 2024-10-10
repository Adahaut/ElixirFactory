using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName = "";
    public Sprite itemIcon;
    public int maxtack;
}