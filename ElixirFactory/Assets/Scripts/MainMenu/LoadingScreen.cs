using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider; // Reference to the UI slider for progress
    public TextMeshProUGUI loadingText;    // Reference to the UI text for progress percentage

    public string sceneToLoad;  // Name of the scene to load

    public void Load()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Normalize progress

            // Update UI elements
            loadingSlider.value = progress;
            loadingText.text = $"Loading: {Mathf.Round(progress * 100)}%";

            yield return null; // Wait for the next frame
        }
        yield return null;
    }
}