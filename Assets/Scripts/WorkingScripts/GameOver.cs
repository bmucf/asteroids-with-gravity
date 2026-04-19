using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = $"- Final Score -\n{ScoreManager.instance.score}";
    }

    public void RestartGame()
    {
        GameManager.instance.SwitchScenes("Testing");
    }

    public void ToMenu()
    {
        GameManager.instance.SwitchScenes("MainMenu");
    }
}