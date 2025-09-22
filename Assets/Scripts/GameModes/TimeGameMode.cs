using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class TimeGameMode : AGameMode
{
    public UnityEvent<float> TimeChanged;

    [SerializeField] private float gameDurationSeconds = 60f;
    [SerializeField] private float UpdateInterval = 1f;

    private readonly CancellationTokenSource cancellationTokenSource = new();
    private float currentTime;

    public override Player[] GetPodium()
    {
        var scores = MatchManager.GetAllScores();
        scores.OrderBy(x => x.Value);
        return scores.Keys.ToArray();
    }

    private void OnDestroy()
    {
        cancellationTokenSource.Dispose();
    }

    public async override void StartGame()
    {
        currentTime = gameDurationSeconds;
        base.StartGame();
        while (currentTime > 0) 
        {
            await UniTask.WaitForSeconds(UpdateInterval).AttachExternalCancellation(cancellationTokenSource.Token);
            currentTime -= UpdateInterval;
            TimeChanged?.Invoke(currentTime);
        }
        GameIsOver?.Invoke();
        print("GameOver");
    }
}
