using UnityEngine;

public class BossHealth : EnemyHealth
{
    [Header("Boss")]
    public string bossName = "Boss";

    public GameObject[] objectsToActivate;

    protected override void Die()
    {
        Debug.Log(bossName + " derrotado");

        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        base.Die();
    }
}