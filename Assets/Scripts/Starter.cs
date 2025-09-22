using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public event Action<int> NewCountdownNumberReached;

    [SerializeField] MatchManager matchManager;
    [SerializeField] private int countDownAmount = 3;

    private int currentTime;
    private readonly CancellationTokenSource cancellationTokenSource = new();

    private async void Start()
    {
        currentTime = countDownAmount;
        while (currentTime >= 0)
        {
            NewCountdownNumberReached?.Invoke(currentTime);
            await UniTask.WaitForSeconds(1).AttachExternalCancellation(cancellationTokenSource.Token);
            currentTime--;
        }
        matchManager.gameMode.StartGame();
    }

    private void OnDestroy()
    {
        cancellationTokenSource.Dispose();
    }
}
