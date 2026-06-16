using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public Text dialogueTextUI;

    private bool isOpen;

    void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDialogue();
        }
    }

    public void OpenDialogue(string text)
    {
        isOpen = true;

        dialoguePanel.SetActive(true);
        dialogueTextUI.text = text;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseDialogue()
    {
        isOpen = false;

        dialoguePanel.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}