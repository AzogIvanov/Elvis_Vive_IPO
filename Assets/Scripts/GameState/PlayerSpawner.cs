using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public Transform defaultSpawnPoint; // por si es la primera vez que entras (sin datos guardados)
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Vector3 targetPosition;

        if (GameManager.Instance != null &&
            GameManager.Instance.TryGetScenePosition(currentScene, out Vector3 savedPos))
        {
            targetPosition = savedPos;
        }
        else if (defaultSpawnPoint != null)
        {
            targetPosition = defaultSpawnPoint.position;
        }
        else
        {
            return; // no hay dato guardado ni spawn por defecto, se queda donde está
        }

        // Desactivamos el CharacterController un frame para evitar
        // que "pelee" con la física al teletransportar
        if (controller != null) controller.enabled = false;
        transform.position = targetPosition;
        if (controller != null) controller.enabled = true;
    }
}