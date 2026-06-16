using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [Header("Escena del juego")]
    public string gameSceneName = "Game";

    [Header("Paneles")]
    public GameObject optionsPanel;

    private bool isMuted;

    private void Start()
    {
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        AudioListener.volume = isMuted ? 0f : 1f;

        if (optionsPanel != null)
            optionsPanel.SetActive(false);

    }

    // PLAY
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // OPCIONES
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    // MUTE GLOBAL
    public void ToggleMute()
    {
        isMuted = !isMuted;

        AudioListener.volume = isMuted ? 0f : 1f;

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    // EXIT
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
