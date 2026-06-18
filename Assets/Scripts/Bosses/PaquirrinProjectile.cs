using UnityEngine;

public class PaquirrinProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public int damage = 1;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalHealth health = other.GetComponentInParent<GlobalHealth>();

            if (health != null)
                health.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}