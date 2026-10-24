using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SaveSystem saveSystem;
    [SerializeField]
    private SaveLoadUI saveLoadUI;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button deleteSaveButton;
    private void Start()
    {
        UpdateButton();
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // Ensure the game runs at normal speed
        if (GameSession.Instance != null)
            GameSession.Instance.ResetSession();
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void UpdateButton()
    {
        bool hasSave = saveSystem != null && saveSystem.HasSaveFile();
        if (continueButton != null)
            continueButton.interactable = hasSave;
        if (deleteSaveButton != null)
            deleteSaveButton.interactable = hasSave;
    }

    public void ContinueGame()
    {
        if (saveSystem == null || !saveSystem.HasSaveFile())
        {
            Debug.LogWarning("No save file available.");
            UpdateButton();
            return;
        }

        if (saveLoadUI == null)
        {
            Debug.LogWarning("SaveLoadUI is missing.");
            return;
        }

        saveLoadUI.LoadGame();
    }

    public void DeleteSaveGame()
    {
        saveSystem.DeleteSave();
        UpdateButton();
    }
}