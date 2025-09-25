using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

[RequireComponent(typeof(Player))]
public class BotBrain : MonoBehaviour
{
    [SerializeField] private BotDifficultyData difficulty;
    [SerializeField] private MatchManager matchManager;

    private Player player;
    private CancellationTokenSource cancellationTokenSource;

    private void Awake()
    {
        player = GetComponent<Player>();
        cancellationTokenSource = new CancellationTokenSource();        
    }

    private void Start()
    {
        matchManager.GameMode.GameIsOver.AddListener(StopCycle);
        StartCycle().Forget();
    }

    private void OnDestroy() => StopCycle();

    private async UniTaskVoid StartCycle()
    {
        try
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                float waitTime = difficulty.GetCurrentWait();
                await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: cancellationTokenSource.Token);
                if (cancellationTokenSource.Token.IsCancellationRequested) break;
                if (matchManager.GameMode.IsGameOn) TryToShoot();
            }
        }
        catch (OperationCanceledException){}
    }

    private void TryToShoot()
    {
        if (!player.IsReadyToShoot) return;
        var error = difficulty.GetError();
        player.StartShot(error);
    }

    public void StopCycle()
    {
        if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }
}
