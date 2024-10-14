using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public BuildPlacement buildPlacer;
    public void SelectBuildPrefab(GameObject buildPrefab)
    {
        buildPlacer.SetBuildPrefab(buildPrefab);
        gameObject.SetActive(false);
    }
}
