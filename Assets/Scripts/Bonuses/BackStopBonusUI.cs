using TMPro;
using UnityEngine;

public class BackStopBonusUI : MonoBehaviour
{
    [SerializeField] private BackStopBonus backStopBonus;
    [SerializeField] private GameObject activeBonusPanel;
    [SerializeField] private TMP_Text activeBonusPanelText;

    private void Awake()
    {
        backStopBonus.NewBonusValue += OnNewBonusValue;
    }

    private void OnNewBonusValue(int newValue)
    {
        if (newValue > 0)
        {
            activeBonusPanelText.text = $"+{newValue}";
            activeBonusPanel.SetActive(true);
        }
        else
        {
            activeBonusPanel.SetActive(false);
        }
    }
}
