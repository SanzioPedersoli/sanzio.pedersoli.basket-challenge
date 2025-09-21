using System;
using UnityEngine;

public class OnFireBonusInjecter : ABonusInjecter<SimpleBonus>
{
    public event Action BecomeOnFire;
    public event Action InterrupdtedOnFire;
    public event Action<float> FireTimeChanged;
    public event Action<float> FirePercentageChanged;

    [SerializeField] private PlayerBallManager playerBallManager;
    [SerializeField] private int multiplier = 2;
    [SerializeField] private float incrementPerScoredShot = 0.25f;
    [SerializeField] private float onFireDuration = 7;
    [SerializeField] private GameObject fireVFX;
    [SerializeField] private MatchManager matchManager;

    private float onFirePercentage;
    private float currentOnFireTime;
    private bool isOnfire;
    private bool haveScoredInthisShot;

    public bool IsOnFire 
    {
        get => isOnfire;
        private set 
        { 
            isOnfire = value;
            if (value)
            {
                CurrentOnFireTime = onFireDuration;
                BecomeOnFire?.Invoke();
            }
            else
            {
                InterrupdtedOnFire?.Invoke();
            }
        } 
    }

    public float CurrentOnFireTime 
    { 
        get => currentOnFireTime; 
        set 
        {
            FireTimeChanged?.Invoke(value);
            currentOnFireTime = value;
        } 
    }

    public float OnFirePercentage 
    { 
        get => onFirePercentage; 
        set 
        {
            onFirePercentage = value;
            FirePercentageChanged?.Invoke(value);
        } 
    }

    private void Update()
    {
        if (isOnfire)
        {
            if (CurrentOnFireTime <= 0) IsOnFire = false;            
            else CurrentOnFireTime -= Time.deltaTime;
        }
    }

    protected void Awake()
    {
        playerBallManager.NewBallReady.AddListener(OnNewBallReady);        
        matchManager.ScoreUpdated += OnScoreUpdated;
    }

    private void OnBallBecameOutOfGame()
    {
        if (!haveScoredInthisShot)
        {
            OnFirePercentage = 0;
            CurrentOnFireTime = 0;
        }
    }

    private void OnScoreUpdated((Player, float) _)
    {
        haveScoredInthisShot = true;
        OnFirePercentage += incrementPerScoredShot;
        if (OnFirePercentage >= 1)
        {
            IsOnFire = true;
            OnFirePercentage = 0;
        }
    }

    private void OnNewBallReady(Ball ball)
    {
        currentBall = ball;
        currentBall.BecameOutOfGame.AddListener(OnBallBecameOutOfGame);
        if (isOnfire)
        {
            InjectBonus();
            Instantiate(fireVFX, currentBall.transform);
        }
        haveScoredInthisShot = false;
    }

    protected override SimpleBonus GetNewBonus()
    {
        return new(0, multiplier);
    }
}
