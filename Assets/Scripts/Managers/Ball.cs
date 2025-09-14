using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private const float DestructionDelay = 2f;
    private const float DestructionFloorLevel = -1f;

    public UnityEvent BecameOutOfGame;

    public Player owner;
    public float score;

    [SerializeField] private Rigidbody rb;

    public Rigidbody Rigidbody => rb;


    public bool isInGame = false;
    private bool isDisposing = false;

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
