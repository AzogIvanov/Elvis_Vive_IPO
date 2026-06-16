using UnityEngine;
using System.Collections;

public class PlayerAreaAttack : MonoBehaviour
{
    [Header("Ataque")]
    public float radius = 4f;
    public int damage = 2;
    public float cooldown = 3f;
    public float stunDuration = 1f;
    public LayerMask enemyLayer;
    public float attackDuration = 2f;
    public float damageInterval = 0.5f;

    [Header("Visual")]
    public GameObject wavePrefab;
    public float waveExpandSpeed = 6f;

    [Header("Abilities")]
    public bool hasArea = false;

    private bool canAttack = true;
    private float cooldownTimer;

    public float AreaCooldownRemaining => cooldownTimer;

    void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (hasArea &&
            Input.GetKeyDown(KeyCode.Q) &&
            canAttack)
        {
            StartCoroutine(DoAreaAttack());
        }
    }

    IEnumerator DoAreaAttack()
    {
        canAttack = false;
        cooldownTimer = cooldown;

        Vector3 offset = new Vector3(0f, 0.1f, 0f);
        GameObject wave = Instantiate(wavePrefab, transform.position + offset, Quaternion.identity);

        StartCoroutine(ExpandWave(wave));

        float timer = 0f;
        float damageTimer = 0f;

        while (timer < attackDuration)
        {
            if (wave != null)
                wave.transform.position = transform.position + offset;

            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);

                foreach (Collider enemy in enemies)
                {
                    EnemyHealth e = enemy.GetComponent<EnemyHealth>();
                    if (e != null)
                    {
                        e.TakeDamage(damage);
                        e.Stun(stunDuration);
                    }
                }

                damageTimer = 0f;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(wave);
        canAttack = true;
    }

    IEnumerator ExpandWave(GameObject wave)
    {
        float targetScale = radius * 2f;
        float size = 0.2f;

        while (size < targetScale)
        {
            size += Time.deltaTime * waveExpandSpeed;
            wave.transform.localScale = new Vector3(size, 0.1f, size);
            yield return null;
        }

        wave.transform.localScale = new Vector3(targetScale, 0.1f, targetScale);
    }
}