using System;
using UnityEngine;

public abstract class ABonusInjecter<Bonus> : MonoBehaviour 
where Bonus : ABonus
{
    protected event Action<Bonus> BonusInjected;

    protected Ball currentBall;

    protected abstract Bonus GetNewBonus();

    protected void InjectBonus()
    {
        var bonus = GetNewBonus();
        BonusInjected?.Invoke(bonus);
        currentBall.AddBonus(bonus);
    }
}
