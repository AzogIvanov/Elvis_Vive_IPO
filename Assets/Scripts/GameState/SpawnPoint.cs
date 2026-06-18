using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetLastSpawnPoint(transform.position);
        }
    }
}