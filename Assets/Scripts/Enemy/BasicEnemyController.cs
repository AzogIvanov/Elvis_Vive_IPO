using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damage = 1;
    public float damageCooldown = 1f;

    private Transform player;
    private float cooldownTimer;

    private EnemyHealth health;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        health = GetComponent<EnemyHealth>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        bool stunned = health != null && health.IsStunned();

        if (stunned)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Stunned", true);
            return;
        }

        animator.SetBool("Stunned", false);

        // MOVEMENT
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        transform.position += direction * moveSpeed * Time.deltaTime;

        // LOOK AT PLAYER
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        // ANIMATION SPEED
        animator.SetFloat("Speed", direction.magnitude);

        // DAMAGE COOLDOWN
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
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

                animator.SetTrigger("Attack");
            }
        }
    }
}