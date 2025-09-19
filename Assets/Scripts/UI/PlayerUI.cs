using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    public void SetScore(float newScore)
    {
        scoreLabel.text = newScore.ToString();
    }
}
