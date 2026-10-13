using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public void SetScore(int scoreCount)
    {
        scoreText.text = "Score: " + scoreCount;
    }
}
