using UnityEngine;
using System.Collections;

public class PaquirrinPattern : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Pattern")]
    public int bulletCount = 21;
    public float spreadAngle = 155f;
    public float shootCooldown = 2f;

    [Header("Gap")]
    public int gapSize = 3;

    private void Start()
    {
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(1f); // IMPORTANTE: evita primer frame bug

        while (true)
        {
            ShootFan();
            yield return new WaitForSeconds(shootCooldown);
        }
    }

    void ShootFan()
    {
        float startAngle = -spreadAngle / 2f;
        float angleStep = spreadAngle / (bulletCount - 1);

        int gapStart = Random.Range(3, bulletCount - gapSize - 3);

        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        Quaternion baseRotation = Quaternion.LookRotation(forward);


        for (int i = 0; i < bulletCount; i++)
        {
            if (i >= gapStart && i < gapStart + gapSize)
                continue;

            float angle = startAngle + angleStep * i;

            Quaternion rot = baseRotation * Quaternion.Euler(0, angle, 0);

            Instantiate(bulletPrefab, firePoint.position, rot);
        }
    }
}