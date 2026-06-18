using UnityEngine;

public class PaquirrinProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalHealth health = other.GetComponentInParent<GlobalHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }


            Destroy(gameObject);
        }

        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}