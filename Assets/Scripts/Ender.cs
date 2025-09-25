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
        var firstPlayer = scoresByPlayers.OrderByDescending(x => x.Value).First().Key;
        if (UserPlayer == firstPlayer)
        {
            SceneManager.LoadScene(victorySceneName);
        }
        else
        {
            SceneManager.LoadScene(defeatSceneName);
        }
    }
}
