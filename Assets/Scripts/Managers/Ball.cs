using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private const string KillZone = "KillZone";
    private const float DestructionDelay = 2f;
    private const float DestructionFloorLevel = -1f;

    public UnityEvent BecameOutOfGame;

    public Player owner;
    public float score;

    [SerializeField] private Rigidbody rb;

    public Rigidbody Rigidbody => rb;

    private bool isInGame = true;

    private void Update()
    {
        if (isInGame && transform.position.y <= DestructionFloorLevel)
        {
            isInGame = false;
            Dispose().Forget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInGame && other.CompareTag(KillZone)) 
        {
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
