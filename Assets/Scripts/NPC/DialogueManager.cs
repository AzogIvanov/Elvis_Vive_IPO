using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject dialoguePanel;
    public TMP_Text dialogueTextUI;
    private bool isOpen;
    public bool IsOpen => isOpen;

    private System.Action onCloseCallback; // <- acción a ejecutar al cerrar

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

    // onClose es opcional: si no pasas nada, se comporta como antes
    public void OpenDialogue(string text, System.Action onClose = null)
    {
        isOpen = true;
        dialoguePanel.SetActive(true);
        dialogueTextUI.text = text;
        onCloseCallback = onClose;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void CloseDialogue()
    {
        isOpen = false;
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
        // Cursor.visible = false;

        onCloseCallback?.Invoke();
        onCloseCallback = null;
    }
}