using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalHealth : MonoBehaviour
{
    public int maxHealth = 3;

    [Header("Hit Flash")]
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private int currentHealth;

    private Renderer[] renderers;
    private Dictionary<Material, Color> originalColors = new();

    void Start()
    {
        currentHealth = maxHealth;

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
        currentHealth -= amount;

        StopCoroutine(nameof(FlashRed));
        StartCoroutine(nameof(FlashRed));

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        foreach (var pair in originalColors)
        {
            pair.Key.color = flashColor;
        }

        yield return new WaitForSeconds(flashDuration);

        foreach (var pair in originalColors)
        {
            pair.Key.color = pair.Value;
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}