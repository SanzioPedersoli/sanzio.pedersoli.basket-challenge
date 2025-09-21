using System;
using UnityEngine;

public class PerfectShotBonusInjecter : ABonusInjecter<SimpleBonus>
{
    public event Action OnBonusApplied;

    [SerializeField] private PlayerBallManager playerBallManager;
    [SerializeField] private Player player;
    [SerializeField] private float errorMargin = 0.1f;

    protected override SimpleBonus GetNewBonus()
    {
        SimpleBonus bonus = new(1,1);
        bonus.OnBallScored += () => OnBonusApplied?.Invoke();
        return bonus;
    }

    private void Start()
    {
        playerBallManager.NewBallReady.AddListener(OnBallReady);        
    }

    private void OnBallReady(Ball ball)
    {
        currentBall = ball;
        player.BallShot.AddListener(OnBallShot);
    }

    private void OnBallShot(float error)
    {
        if (Mathf.Abs(error) < errorMargin)
        {
            InjectBonus();
        }
    }
}
