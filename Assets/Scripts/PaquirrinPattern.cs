using UnityEngine;
using System.Collections;

public class PaquirrinPattern : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Pattern")]
    public int bulletCount = 13;
    public float spreadAngle = 120f;
    public float shootCooldown = 2f;

    [Header("Gap")]
    public int gapSize = 2; // hueco de balas que faltan

    private void Start()
    {
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            ShootSemiCircle();
            yield return new WaitForSeconds(shootCooldown);
        }
    }

    void ShootSemiCircle()
    {
        float startAngle = -spreadAngle / 2f;
        float angleStep = spreadAngle / (bulletCount - 1);

        int randomGapStart = Random.Range(0, bulletCount - gapSize + 1);

        Vector3 flatForward = transform.forward;
        flatForward.y = 0;
        flatForward.Normalize();

        Quaternion baseRotation = Quaternion.LookRotation(flatForward);

        for (int i = 0; i < bulletCount; i++)
        {
            if (i >= randomGapStart && i < randomGapStart + gapSize)
                continue;

            float angle = startAngle + angleStep * i;

            Quaternion rotation =
                baseRotation *
                Quaternion.Euler(0, angle, 0);

            Instantiate(
                bulletPrefab,
                firePoint.position,
                rotation
            );
        }
    }
}