using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildPlacement : MonoBehaviour
{
    private GameObject currentBuildPrefab;
    private GameObject currentBuildPreview;

    public void SetBuildPrefab(GameObject buildPrefab)
    {
        currentBuildPrefab = buildPrefab;
        currentBuildPreview = Instantiate(currentBuildPrefab);
    }

    private void SetBuildPreviewPosition()
    {
        
    }
}
