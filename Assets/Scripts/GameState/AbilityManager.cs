using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAreaAttack playerAreaAttack;
    public AttackBuff attackBuff;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            playerMovement.hasDash = GameManager.Instance.hasDash;
            playerAreaAttack.hasArea = GameManager.Instance.hasAreaAttack;
            attackBuff.hasSpecial = GameManager.Instance.hasSpecial;
        }
    }

    public void ActivateDash()
    {
        playerMovement.hasDash = true;
        GameManager.Instance.hasDash = true;
    }

    public void ActivateAreaAttack()
    {
        playerAreaAttack.hasArea = true;
        GameManager.Instance.hasAreaAttack = true;
    }

    public void ActivateSpecial()
    {
        attackBuff.hasSpecial = true;
        GameManager.Instance.hasSpecial = true;
    }
}