using UnityEngine;

public class GameElementInstantiator : MonoBehaviour
{
    [SerializeField] private MatchSettings matchSettings;
    [SerializeField] private MatchManager matchManager;
    [SerializeField] private GameObject botPrefab;
    [SerializeField] private AGameMode fallBackGameMode;

    private void Awake()
    {
        SpawnBots(matchSettings.numberOfBots);
        SpawnGameMode(matchSettings.gameMode != null ? matchSettings.gameMode : fallBackGameMode);
    }

    private void SpawnGameMode(AGameMode gameMode)
    {
        var inst = Instantiate(gameMode);
        matchManager.GameMode = inst;
    }

    private void SpawnBots(int numberOfBots)
    {
        if (numberOfBots == 0) numberOfBots = 1;
        for (var i=0; i<numberOfBots; i++) 
            Instantiate(botPrefab, transform.position, Quaternion.identity);
    }
}
