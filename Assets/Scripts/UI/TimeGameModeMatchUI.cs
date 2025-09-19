using TMPro;
using UnityEngine;

public class TimeGameModeMatchUI : AMatchUI<TimeGameMode>
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Animator gameModeAnimator;

    protected override void Awake()
    {
        base.Awake();
        gameMode.TimeChanged.AddListener(OnTimeChanged);
        gameMode.GameStarted.AddListener(OnGameStarted);
        gameMode.GameIsOver.AddListener(OnGameIsOver);
    }

    private void OnGameIsOver()
    {
        gameModeAnimator.SetTrigger("GameIsOver");
    }

    private void OnGameStarted()
    {
        gameModeAnimator.SetTrigger("GameStarded");
    }

    private void OnTimeChanged(float newTime)
    {
        int minutes = Mathf.FloorToInt(newTime / 60f);
        int seconds = Mathf.FloorToInt(newTime % 60f);
        int hundredths = Mathf.FloorToInt((newTime * 100f) % 100f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    private void OnDestroy()
    {
        gameMode.TimeChanged.RemoveAllListeners();
        gameMode.GameStarted.RemoveAllListeners();
        gameMode.GameIsOver.RemoveAllListeners();
    }
}