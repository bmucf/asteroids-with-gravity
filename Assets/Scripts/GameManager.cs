using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject planetoid;
    public int minAsteroid;
    public int maxAsteroid;
    public int asteroidCount;

    private void Start()
    {
        asteroidCount = Random.Range(minAsteroid, maxAsteroid);

        for (int i = 0; i < asteroidCount; ++i)
        {
            Instantiate(planetoid, RandomPointInRing(25, 100), Quaternion.identity);
            planetoid.transform.localScale = Vector3.one * Random.Range(3, 7);
        }

        Instantiate(playerShip, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
