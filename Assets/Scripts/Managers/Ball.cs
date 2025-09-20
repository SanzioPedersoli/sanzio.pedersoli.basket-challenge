using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] private float DestructionDelay = 2f;
    [SerializeField] private float DestructionFloorLevel = -1f;

    public UnityEvent BecameOutOfGame;

    public Player owner;
    [SerializeField] private int InitialScore = 2;
    [SerializeField] private Rigidbody rb;

    public Rigidbody Rigidbody => rb;

    public bool isInGame = false;
    private bool isDisposing = false;
    private List<ABonus> bonuses = new();

    public int GetScore()
    {
        int score = InitialScore;
        int multiplier = 1;
        foreach (var bonus in bonuses)
        {
            multiplier += bonus.MultiplierBonus;
            score += bonus.AdditiveBonus;
        }
        return score*multiplier;
    }

    public void AddBonus(ABonus bonus)
    {
        bonuses.Add(bonus);
    }

    private void Update()
    {
        if (!isDisposing && transform.position.y <= DestructionFloorLevel)
        {
            isDisposing = true;
            isInGame = false;
            Dispose().Forget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDisposing && other.CompareTag(TagHelper.KillZone)) 
        {
            isDisposing = true;
            isInGame = false;
            Dispose().Forget(); 
        }      
    }

    private async UniTask Dispose()
    {
        await UniTask.WaitForSeconds(DestructionDelay);
        BecameOutOfGame?.Invoke();
        Destroy(gameObject);
    }
}
