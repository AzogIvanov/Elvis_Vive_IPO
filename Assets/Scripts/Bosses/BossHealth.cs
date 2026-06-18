using UnityEngine;

public class BossHealth : EnemyHealth
{
    [Header("Boss")]
    public string bossName = "Boss";

    protected override void Die()
    {
        Debug.Log(bossName + " derrotado");
        base.Die();
    }
}