using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    [SerializeField]
    private ScoreUI scoreUI;
    private int score;
    public int Score
    {
        get { return score; }
    }
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void SetScore(int value)
    {
        score = Mathf.Max(value, 0);

        if (GameSession.Instance != null)
            GameSession.Instance.Score = score;

        if (PersistentGameManager.Instance != null)
            PersistentGameManager.Instance.UpdateHighScore(score);

        UpdateUI();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (GameSession.Instance != null)
        {
            score = GameSession.Instance.Score;
        }
        UpdateUI();
        if (PersistentGameManager.Instance != null)
        {
            PersistentGameManager.Instance.UpdateHighScore(score);
        }
    }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        score += amount;
        SetScore(score + amount);

        if (GameSession.Instance != null)
        {
            GameSession.Instance.Score = score;
        }
        if (PersistentGameManager.Instance != null)
        {
            PersistentGameManager.Instance.UpdateHighScore(score);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreUI != null)
            scoreUI.SetScore(score);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            AddScore(10);
    }

    public void GameOver()
    {
        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + score;
    }
}
