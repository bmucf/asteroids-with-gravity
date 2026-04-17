using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    public Image fuelGauge;
    public Image armorGauge;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateFuelGauge(float remainingFuel, float maxFuel)
    {
        fuelGauge.fillAmount = remainingFuel / maxFuel;
    }

    public void UpdateArmorGauge(float remainingArmor, float totalArmor, float amount) 
    { 
        remainingArmor = amount; 
        armorGauge.fillAmount = remainingArmor / totalArmor;
    }
}
