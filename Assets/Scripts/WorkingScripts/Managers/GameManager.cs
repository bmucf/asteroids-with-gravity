using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerShip;
    public GameObject planetoid;
    public float levelTimeSeconds;
    private float timeRemaining;
    private bool gameOverTriggered = false;

    [Header("World Generation")]
    public int innerBounds;
    public int outerBounds;
    public int minAsteroid;
    public int maxAsteroid;
    private int asteroidCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Testing")
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        gameOverTriggered = false;

        timeRemaining = levelTimeSeconds;
        ScoreManager.instance.ResetScore();

        asteroidCount = Random.Range(minAsteroid, maxAsteroid);

        Instantiate(playerShip, Vector3.zero, Quaternion.identity);

        for (int i = 0; i < asteroidCount; ++i)
        {
            Instantiate(planetoid, RandomPointInRing(innerBounds, outerBounds), Quaternion.identity);
        }

        timeRemaining = levelTimeSeconds;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Testing")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                LoadGame();
            }

            timeRemaining -= Time.deltaTime;
            HUDManager.instance.UpdateDisplayedTimer(timeRemaining);

            if (timeRemaining <= 0f && !gameOverTriggered)
            {
                gameOverTriggered = true;
                SwitchScenes("GameOver");
            }
        }
    }

    Vector2 RandomPointInRing(float innerRadius, float outerRadius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // uniform area sampling between two radii
        float u = Random.value;
        float radius = Mathf.Sqrt(u * (outerRadius * outerRadius - innerRadius * innerRadius) + innerRadius * innerRadius);

        return new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
    }

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Testing");
    }
}

