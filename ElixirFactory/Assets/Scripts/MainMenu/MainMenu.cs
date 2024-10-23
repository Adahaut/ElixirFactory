using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public LoadingScreen loadingScreen;
    
    public void LoadScene()
    {
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.Load();
    }
}
