using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileTestButton : MonoBehaviour
{
    public void NextScene()
    {   
        var loadIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(loadIndex);
    }
}