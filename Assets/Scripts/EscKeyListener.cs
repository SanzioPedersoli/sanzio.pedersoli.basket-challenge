using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class EscKeyListener : MonoBehaviour
{
    public UnityEvent OnEscapePressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapePressed?.Invoke();
        }
    }
}
