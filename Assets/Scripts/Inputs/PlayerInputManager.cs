using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR || UNITY_STANDALONE
using IngamePlayerInput = IngamePlayerMouseInput;
#elif UNITY_ANDROID || UNITY_IOS
    using IngamePlayerInput = IngamePlayerTouchInput;
#endif

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(InputRangeMaker))]
public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private float valuePerPixel = 0.01f;
    [SerializeField] private float maxDragDuration = 1f;

    private Player player;
    private InputRangeMaker inputRangeMaker;

    private void Awake()
    {
        inputRangeMaker = GetComponent<InputRangeMaker>();
        player = GetComponent<Player>();
        var input = gameObject.AddComponent<IngamePlayerInput>();
        input.ValuePerPixel = valuePerPixel;
        input.MaxDragDuration = maxDragDuration;

        input.OnDragFinished.AddListener(value => {
            var error = inputRangeMaker.GetErrorFromRange(value);
            player.StartShot(error);
        });        
    }
}
