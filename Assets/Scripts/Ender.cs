using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ender : MonoBehaviour
{
    [SerializeField] private MatchManager matchManager;
    [SerializeField] private Player UserPlayer;
    [SerializeField] private Animator GameOverAnimator;

    [SerializeField] private string victorySceneName;
    [SerializeField] private string defeatSceneName;

    private void Awake()
    {
        matchManager.GameEnded += scoresByPlayers => OnMatchEnded(scoresByPlayers).Forget(); 
    }

    private async UniTask OnMatchEnded(Dictionary<Player, float> scoresByPlayers)
    {
        GameOverAnimator.SetTrigger("Blink");
        await UniTask.WaitForSeconds(2);
        LoadResultScene(scoresByPlayers);
    }

    private void LoadResultScene(Dictionary<Player, float> scoresByPlayers)
    {
        if (scoresByPlayers == null || scoresByPlayers.Count == 0)
        {
            SceneManager.LoadScene(defeatSceneName);
            return;
        }

        if (!scoresByPlayers.TryGetValue(UserPlayer, out var userScore))
        {
            SceneManager.LoadScene(defeatSceneName);
            return;
        }

        var top = scoresByPlayers.Max(x => x.Value);
        if (userScore == top && scoresByPlayers.Count(x => x.Value == top) == 1)
            SceneManager.LoadScene(victorySceneName);
        else if (userScore == top)
            // TODO: decide what to do in a tie
            SceneManager.LoadScene(victorySceneName);
        else
            SceneManager.LoadScene(defeatSceneName);
    }

}
