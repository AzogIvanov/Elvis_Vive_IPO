using UnityEngine;
using UnityEngine.UI;

public class AbilityHUD : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAreaAttack playerArea;
    public AttackBuff playerSpecial;

    [Header("Dash")]
    public GameObject dashContainer;
    public Image cooldownDashImage;

    [Header("Area")]
    public GameObject areaContainer;
    public Image cooldownAreaImage;

    [Header("Special")]
    public GameObject specialContainer;
    public Image cooldownSpecialImage;

    void Update()
    {
        DashManager();
        AreaManager();
        SpecialManager();
    }

    void DashManager()
    {
        dashContainer.SetActive(playerMovement.hasDash);

        if (!playerMovement.hasDash)
            return;

        float fill = playerMovement.DashCooldownRemaining / playerMovement.dashCooldown;
        cooldownDashImage.fillAmount = Mathf.Clamp01(fill);
    }

    void AreaManager()
    {
        areaContainer.SetActive(playerArea.hasArea);

        if (!playerArea.hasArea)
            return;

        float fill = playerArea.AreaCooldownRemaining / playerArea.cooldown;
        cooldownAreaImage.fillAmount = Mathf.Clamp01(fill);
    }

    void SpecialManager()
    {
        specialContainer.SetActive(playerSpecial.hasSpecial);

        if (!playerSpecial.hasSpecial)
            return;

        float fill = playerSpecial.SpecialCooldownRemaining / playerSpecial.specialCooldown;
        cooldownSpecialImage.fillAmount = Mathf.Clamp01(fill);
    }
}