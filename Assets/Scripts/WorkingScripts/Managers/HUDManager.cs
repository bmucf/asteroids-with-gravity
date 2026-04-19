using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public Image armorGauge;
    public Image fuelGauge;

    public TMP_Text score;
    public TMP_Text timer;
    public TMP_Text coord;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateFuelGauge(float remainingFuel, float maxFuel)
    {
        fuelGauge.fillAmount = remainingFuel / maxFuel;
    }

    public void UpdateArmorGauge(float remainingArmor, float totalArmor) 
    { 
        armorGauge.fillAmount = remainingArmor / totalArmor;
    }

    public void UpdateDisplayedCoordinates(Vector2 currentPos)
    {
        coord.SetText($"Coordinates: ({(int)currentPos.x}, {(int)currentPos.y})");
    }

    public void UpdateDisplayedScore(int totalScore)
    {
        score.SetText($"Score: {totalScore}");
    }

    public void UpdateDisplayedTimer(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timer.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }

}
