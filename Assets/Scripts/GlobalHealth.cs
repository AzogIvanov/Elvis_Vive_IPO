using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GlobalHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public bool isPlayer = false;

    private bool isDead = false;

    [Header("Hit Flash")]
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    [Header("Respawn (solo jugador)")]
    public float respawnDelay = 3f;
    public bool reloadSceneOnDeath = false; // marca esto en escenas como la iglesia

    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public System.Action onHealthChanged;

    private Renderer[] renderers;
    private Dictionary<Material, Color> originalColors = new();

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (isPlayer && GameManager.Instance != null)
        {
            currentHealth = GameManager.Instance.playerHealth;
        }
        else
        {
            currentHealth = maxHealth;
        }

        onHealthChanged?.Invoke();

        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                if (mat.HasProperty("_Color") && !originalColors.ContainsKey(mat))
                {
                    originalColors.Add(mat, mat.color);
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (!enabled)
            return;

        if (isDead)
            return;

        currentHealth -= amount;

        if (isPlayer && GameManager.Instance != null)
            GameManager.Instance.playerHealth = currentHealth;

        onHealthChanged?.Invoke();

        StopCoroutine(nameof(FlashRed));
        StartCoroutine(nameof(FlashRed));

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        foreach (var pair in originalColors)
            pair.Key.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        foreach (var pair in originalColors)
            pair.Key.color = pair.Value;
    }

    protected virtual void Die()
    {
        if (isPlayer)
        {
            StartCoroutine(RespawnPlayer());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator RespawnPlayer()
    {
        SetPlayerControl(false);

        yield return new WaitForSeconds(respawnDelay);

        currentHealth = maxHealth;
        isDead = false;

        if (GameManager.Instance != null)
            GameManager.Instance.playerHealth = currentHealth;

        if (reloadSceneOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            yield break;
        }

        // Comportamiento normal: reaparecer en el último spawn point
        if (GameManager.Instance != null &&
            GameManager.Instance.TryGetLastSpawnPoint(out Vector3 spawnPos))
        {
            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;
            Debug.Log("RESPAWN A: " + spawnPos);
            transform.position = spawnPos;
            if (controller != null) controller.enabled = true;
        }

        onHealthChanged?.Invoke();
        SetPlayerControl(true);
    }

    private void SetPlayerControl(bool active)
    {
        foreach (Renderer r in renderers)
            r.enabled = active;

        PlayerMovement pm = GetComponent<PlayerMovement>();
        if (pm != null) pm.enabled = active;

        PlayerAttack pa = GetComponent<PlayerAttack>();
        if (pa != null) pa.enabled = active;
    }
}