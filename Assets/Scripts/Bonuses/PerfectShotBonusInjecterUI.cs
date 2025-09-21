using UnityEngine;

public class PerfectShotBonusInjecterUI : MonoBehaviour
{
    [SerializeField] private PerfectShotBonusInjecter perfectShotBonusInjecter;
    [SerializeField] private GameObject perfectVFXsTrail;
    [SerializeField] private GameObject perfectVFXsCelebration;

    private void Awake()
    {
        perfectShotBonusInjecter.OnBonusApplied += OnBonusApplied;
        perfectShotBonusInjecter.BonusInjected += OnBonusInjected;
    }

    private void OnBonusInjected(SimpleBonus bonus)
    {
        Instantiate(perfectVFXsTrail, perfectShotBonusInjecter.Ball.transform);
    }

    private void OnBonusApplied()
    {
        Instantiate(perfectVFXsCelebration, perfectShotBonusInjecter.Ball.transform);

    }
}