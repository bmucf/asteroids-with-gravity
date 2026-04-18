using UnityEngine;

public class Planet : FloatingBody
{
    public GameObject gravPrefab;
    public ParticleSystem explosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mass = Random.Range(1f, 20f);

        transform.localScale = Vector3.one * mass;

        GameObject gravField = Instantiate(gravPrefab, transform.position, Quaternion.identity);
        gravField.transform.localScale *= 2;
        gravField.transform.SetParent(transform.parent);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(explosion, other.transform.position, Quaternion.identity);
        Destroy(other.gameObject);
    }
}
