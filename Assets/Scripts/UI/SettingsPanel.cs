using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SettingsPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider numberOfBotsSlider;
    [SerializeField] private TMP_Text numberOfBotsLabel;
    [SerializeField] private TMP_Dropdown fieldSelector;
    [SerializeField] private TMP_Dropdown gameModeSelector;

    [Header("Game References")]
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private List<AGameMode> availableGameModes;
    private MatchSettings matchSettings;

    private void Awake()
    {
        matchSettings = Resources.Load<MatchSettings>("MatchSettings");

        BuildGameModeDropdown();
        ApplyInitialValues();

        numberOfBotsSlider.onValueChanged.AddListener(OnNumberOfBotsChanged);
        fieldSelector.onValueChanged.AddListener(OnFieldChanged);
        gameModeSelector.onValueChanged.AddListener(OnGameModeChanged);
    }

    private void BuildGameModeDropdown()
    {
        gameModeSelector.ClearOptions();

        List<string> optionNames = new();
        foreach (var mode in availableGameModes)
        {
            optionNames.Add(mode.name);
        }
        gameModeSelector.AddOptions(optionNames);
    }

    private void ApplyInitialValues()
    {
        matchSettings.numberOfBots = Mathf.RoundToInt(numberOfBotsSlider.value);
        matchSettings.field = fieldSelector.options[fieldSelector.value].text;

        if (availableGameModes.Count > 0) matchSettings.gameMode = availableGameModes[gameModeSelector.value];
    }

    private void OnNumberOfBotsChanged(float value)
    {
        matchSettings.numberOfBots = Mathf.RoundToInt(value);
        numberOfBotsLabel.text = ((int)value).ToString();
    }

    private void OnFieldChanged(int index)
    {
        matchSettings.field = fieldSelector.options[index].text;
    }

    private void OnGameModeChanged(int index)
    {
        if (index >= 0 && index < availableGameModes.Count) matchSettings.gameMode = availableGameModes[index];
    }

    public void StartMatch()
    {
        sceneLoader.LoadScene(matchSettings.field);
    }
}
