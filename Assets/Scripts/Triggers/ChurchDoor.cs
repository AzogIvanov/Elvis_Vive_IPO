using UnityEngine;
using UnityEngine.SceneManagement;

public class ChurchDoor : MonoBehaviour
{
    [Header("Scene")]
    public string sceneToLoad;

    [Header("UI")]
    public GameObject interactText;

    private bool playerNear = false;

    private void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (interactText != null)
                interactText.SetActive(false);
        }
    }
}