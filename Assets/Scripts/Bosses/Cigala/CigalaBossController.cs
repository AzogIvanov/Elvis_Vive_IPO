using UnityEngine;

public class CigalaBossController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float attackRange = 2f;

    [Header("Combat")]
    public int damage = 2;
    public float damageCooldown = 1.5f;

    private Transform player;
    private EnemyHealth health;
    private Animator animator;

    private float cooldownTimer;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        health = GetComponent<EnemyHealth>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (player == null)
            return;

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        bool stunned = health != null && health.IsStunned();

        if (stunned)
        {
            if (animator != null)
                animator.SetBool("IsAttacking", false);

            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > attackRange)
        {
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);

            if (animator != null)
                animator.SetBool("IsAttacking", false);
        }
        else
        {
            if (animator != null)
                animator.SetBool("IsAttacking", true);
        }
    }

        void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && cooldownTimer <= 0f)
            {
                GlobalHealth playerHealth = collision.gameObject.GetComponent<GlobalHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    cooldownTimer = damageCooldown;

                }
            }
        }
    }