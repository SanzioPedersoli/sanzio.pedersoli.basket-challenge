using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneName;
    [SerializeField] MatchSettings matchSettings;

    public void LoadSceneFromSettings()
    {
        if (matchSettings != null) LoadScene(matchSettings.field);
    }

    public void LoadScene() => LoadScene(SceneName);

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
