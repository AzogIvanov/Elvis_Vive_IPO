using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Referencias a las habilidades del jugador")]
    public PlayerMovement playerMovement;      // controla el Dash (hasDash)
    public PlayerAreaAttack playerAreaAttack;   // controla el ataque de área (hasArea)
    public AttackBuff attackBuff;               // controla el ataque especial (hasSpecial)

    public void ActivateDash()
    {
        if (playerMovement != null)
            playerMovement.hasDash = true;
    }

    public void ActivateAreaAttack()
    {
        if (playerAreaAttack != null)
            playerAreaAttack.hasArea = true;
    }

    public void ActivateSpecial()
    {
        if (attackBuff != null)
            attackBuff.hasSpecial = true;
    }
}