using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneName;

    public void LoadScene() => LoadScene(SceneName);

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
