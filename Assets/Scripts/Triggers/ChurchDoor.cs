using UnityEngine;
using UnityEngine.SceneManagement;

public class ChurchDoor : MonoBehaviour
{
    [Header("Scene")]
    public string sceneToLoad;

    [Header("UI")]
    public GameObject interactText;

    [Header("Optional Object")]
    public GameObject objectToDeactivate;

    private bool playerNear = false;
    private Transform playerTransform;

    private void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);

        // restaurar estado si ya fue activado antes
        if (GameManager.Instance != null &&
            GameManager.Instance.GetFlag("ChurchDoorOpened"))
        {
            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetFlag("ChurchDoorOpened", true);

                if (playerTransform != null)
                {
                    GameManager.Instance.SaveScenePosition(
                        SceneManager.GetActiveScene().name,
                        playerTransform.position
                    );
                }
            }

            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);

            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerNear = true;
        playerTransform = other.transform;

        if (interactText != null)
            interactText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerNear = false;

        if (interactText != null)
            interactText.SetActive(false);
    }
}