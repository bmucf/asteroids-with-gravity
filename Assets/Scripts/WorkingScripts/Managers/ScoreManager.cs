using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    private void Update()
    {
        Debug.Log(score);
        HUDManager.instance.UpdateDisplayedScore(score);
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void UpdateScore(int points)
    {
        score += points;
    }


}
