using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private Slider onFireSlider;
    [SerializeField] private Image Background;

    [HideInInspector] public OnFireBonusInjecter onFireBonusInjecter;

    private void Start()
    {
        onFireBonusInjecter.BecomeOnFire += OnBecomeOnFire;
        onFireBonusInjecter.InterrupdtedOnFire += OnInterrupdtedOnFire;
        onFireBonusInjecter.FirePercentageChanged += OnFirePercentageChanged;
        onFireBonusInjecter.FireTimeChanged += OnFireTimeChanged;
    }

    private void OnFireTimeChanged(float newTime)
    {
        onFireSlider.value = newTime;
    }

    private void OnFirePercentageChanged(float fraction)
    {
        onFireSlider.value = fraction;
    }

    private void OnInterrupdtedOnFire()
    {
        onFireSlider.value = 0;
    }

    private void OnBecomeOnFire()
    {
        onFireSlider.value = 1;
    }

    public void SetScore(float newScore)
    {
        scoreLabel.text = newScore.ToString();
    }
}