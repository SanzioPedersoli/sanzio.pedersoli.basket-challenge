using System;
using UnityEngine;

public abstract class ABonusInjecter<Bonus> : MonoBehaviour 
where Bonus : ABonus
{
    protected event Action<Bonus> BonusInjected;

    [SerializeField] protected PlayerBallManager playerBallManager;
    protected Ball currentBall;

    protected virtual void Awake()
    {
        playerBallManager.NewBallReady.AddListener(ball => currentBall = ball);
    }

    protected abstract Bonus GetNewBonus();

    protected void InjectBonus()
    {
        var bonus = GetNewBonus();
        BonusInjected?.Invoke(bonus);
        currentBall.AddBonus(bonus);
    }
}
