using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int playerHealth = 100;
    public int maxHealth = 100;

    [Header("Habilidades desbloqueadas")]
    public bool hasDash = false;
    public bool hasAreaAttack = false;
    public bool hasSpecial = false;

    [Header("Respawn")]
    private Vector3 lastSpawnPoint;
    private bool hasSpawnPoint = false;


    // Flags de progreso
    private Dictionary<string, bool> flags = new Dictionary<string, bool>();

    // Última posición del jugador 
    private Dictionary<string, Vector3> scenePositions = new Dictionary<string, Vector3>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetLastSpawnPoint(Vector3 position)
    {
        lastSpawnPoint = position;
        hasSpawnPoint = true;
    }

    public bool TryGetLastSpawnPoint(out Vector3 position)
    {
        position = lastSpawnPoint;
        return hasSpawnPoint;
    }

    // ---------------- FLAGS ----------------
    public void SetFlag(string flagId, bool value)
    {
        flags[flagId] = value;
    }

    public bool GetFlag(string flagId)
    {
        return flags.TryGetValue(flagId, out bool value) && value;
    }

    // ---------------- POSICIONES ----------------
    public void SaveScenePosition(string sceneName, Vector3 position)
    {
        scenePositions[sceneName] = position;
    }

    public bool TryGetScenePosition(string sceneName, out Vector3 position)
    {
        return scenePositions.TryGetValue(sceneName, out position);
    }
}