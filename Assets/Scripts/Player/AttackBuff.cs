using System.Collections;
using UnityEngine;

public class AttackBuff : MonoBehaviour
{
    public PlayerAttack playerAttack;

    [Header("Buff")]
    public float duration = 5f;

    [Header("Special")]
    public bool hasSpecial = false;
    public float specialCooldown = 3f;

    private bool isActive = false;
    private bool isOnCooldown = false;

    private float cooldownTimer;

    public float SpecialCooldownRemaining => cooldownTimer;

    void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (hasSpecial &&
            !isActive &&
            !isOnCooldown &&
            Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ActivateSpecial());
        }
    }

    IEnumerator ActivateSpecial()
    {
        isActive = true;

        playerAttack.isFanAttack = true;

        yield return new WaitForSeconds(duration);

        playerAttack.isFanAttack = false;

        isActive = false;
        isOnCooldown = true;
        cooldownTimer = specialCooldown;

        yield return new WaitForSeconds(specialCooldown);

        isOnCooldown = false;
    }
}