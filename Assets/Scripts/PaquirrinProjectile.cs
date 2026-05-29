using UnityEngine;

public class PaquirrinProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;

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
        Debug.Log("Hit: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER HIT");

            // daño aquí luego
            Destroy(gameObject);
        }
    }
}