public class SimpleBonus : ABonus
{
    private readonly int additiveBonus;
    private readonly int multiplierBonus;

    public override int AdditiveBonus => additiveBonus;
    public override int MultiplierBonus => multiplierBonus;

    public SimpleBonus(int additiveBonus, int multiplierBonus) 
    {
        this.additiveBonus = additiveBonus;
        this.multiplierBonus = multiplierBonus;
    }
}