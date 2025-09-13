using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Starter : MonoBehaviour
{
    private readonly UnityEvent<int> NewCountdownNumberReached;

    [SerializeField] MatchManager matchManager;
    [SerializeField] private int countDownAmount = 3;

    private int currentTime;
    private readonly CancellationTokenSource cancellationTokenSource = new();

    private async void Start()
    {
        currentTime = countDownAmount;
        while (currentTime >= 0)
        {
            print($"Starting in: {currentTime}");
            await UniTask.WaitForSeconds(1).AttachExternalCancellation(cancellationTokenSource.Token);
            currentTime--;
            NewCountdownNumberReached?.Invoke(currentTime);
        }
        matchManager.gameMode.StartGame();
    }

    private void OnDestroy()
    {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
    }
}
