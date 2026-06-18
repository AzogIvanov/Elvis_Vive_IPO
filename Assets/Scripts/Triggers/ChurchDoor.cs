using UnityEngine;
using UnityEngine.SceneManagement;

public class ChurchDoor : MonoBehaviour
{
    [Header("Scene")]
    public string sceneToLoad;

    [Header("UI")]
    public GameObject interactText;

    private bool playerNear = false;
    private Transform playerTransform;

    private void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            // Guardamos la posición del jugador en la escena ACTUAL,
            // justo en el punto donde está la puerta, antes de cambiar de escena
            if (GameManager.Instance != null && playerTransform != null)
            {
                GameManager.Instance.SaveScenePosition(
                    SceneManager.GetActiveScene().name,
                    playerTransform.position
                );
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerTransform = other.transform;
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