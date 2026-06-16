using UnityEngine;

public class NPCInteractionZone : MonoBehaviour
{
    public GameObject interactionUI;

    [TextArea(3, 10)]
    public string dialogueText;

    private bool playerInZone;

    void Start()
    {
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.OpenDialogue(dialogueText);
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