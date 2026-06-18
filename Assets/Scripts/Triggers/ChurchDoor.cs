using UnityEngine;
using UnityEngine.SceneManagement;

public class ChurchDoor : MonoBehaviour
{
    [Header("Scene")]
    public string sceneToLoad;

    [Header("UI")]
    public GameObject interactText;

    [Header("Optional Objects")]
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToActivate;

    private bool playerNear = false;
    private Transform playerTransform;
    private bool alreadyTriggered = false;

    private void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);

        ApplyState();
    }

    void ApplyState()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.GetFlag("ChurchDoorOpened"))
        {
            SetObjects(objectsToActivate, true);
            SetObjects(objectsToDeactivate, false);
        }
    }

    void SetObjects(GameObject[] list, bool state)
    {
        if (list == null) return;

        foreach (var obj in list)
        {
            if (obj != null)
                obj.SetActive(state);
        }
    }

    private void Update()
    {
        if (alreadyTriggered || !playerNear || !Input.GetKeyDown(KeyCode.F))
            return;

        alreadyTriggered = true;

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

        SetObjects(objectsToDeactivate, false);

        SceneManager.LoadScene(sceneToLoad);
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