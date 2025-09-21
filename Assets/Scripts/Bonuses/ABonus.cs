using System;

[Serializable]
public abstract class ABonus
{
    public Action OnBallScored;
    public abstract int AdditiveBonus { get; }
    public abstract int MultiplierBonus { get; }
}
