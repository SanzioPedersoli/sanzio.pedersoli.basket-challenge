using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BackStopBonus : ABonusInjecter<SimpleBonus>
{
    public event Action<int> NewBonusValue;
    public UnityEvent<float> TimeChanged;

    [SerializeField] private MatchManager matchManager;
    [SerializeField] private Vector2Int minMaxAdditiveBonus;
    [SerializeField] private Vector2 minMaxBonusTime;
    [SerializeField] private Vector2 minMaxWaitTime;

    private int currentAdditiveBonus;
    private readonly CancellationTokenSource cancellationTokenSource = new();
    private float currentTime;

    protected override SimpleBonus GetNewBonus() => new(currentAdditiveBonus, 1);
    private void OnDestroy() => cancellationTokenSource?.Dispose();

    private int GetBonusValue()
    {
        if (currentAdditiveBonus == 0) return Random.Range(minMaxAdditiveBonus.x, minMaxAdditiveBonus.y);
        else return 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Ball>(out var ball) && currentBall != ball)
        {
            currentBall = ball;
            InjectBonus();
        }
    }

    private void Start()
    {
        StartBonusCycle().Forget();
        matchManager.gameMode.GameIsOver.AddListener(() => cancellationTokenSource.Cancel());
    }

    private async UniTask StartBonusCycle()
    {
        currentTime = currentAdditiveBonus != 0 ? Random.Range(minMaxAdditiveBonus.x, minMaxAdditiveBonus.y) : Random.Range(minMaxWaitTime.x, minMaxWaitTime.y);
        while (currentTime > 0)
        {
            await UniTask.WaitForSeconds(1).AttachExternalCancellation(cancellationTokenSource.Token);
            currentTime -= 1;
            TimeChanged?.Invoke(currentTime);
        }
        currentAdditiveBonus = GetBonusValue();
        NewBonusValue?.Invoke(currentAdditiveBonus);
        if (matchManager.gameMode.IsGameOn) StartBonusCycle().Forget();
    }
}