using System;
using UnityEngine;

public class GridModel : MonoBehaviour
{
    public GameObject[,] grid;
    public int gridSize;
    public GameObject casePrefab;
    public Sprite grassSprite;
    public Sprite rockyGroundSprite;
    public Sprite rockLimitsSprite;
    public Sprite waterSprite;
    public Transform map;
    public static GridModel instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
