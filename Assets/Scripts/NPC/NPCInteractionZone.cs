using UnityEngine;
using UnityEngine.Events;

public class NPCInteractionZone : MonoBehaviour
{
    public GameObject interactionUI;
    [TextArea(3, 10)]
    public string dialogueText;

    public UnityEvent onDialogueClosed; // <- aquí enchufas dar objeto / abrir tienda / iniciar quest

    private bool playerInZone;

    void Start()
    {
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.OpenDialogue(dialogueText, () => onDialogueClosed?.Invoke());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            interactionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            interactionUI.SetActive(false);
        }
    }
}