using Unity.VisualScripting;
using UnityEngine;

public class DestructibleThing : MonoBehaviour
{
    Rigidbody2D rb;

    public float minChunkMass = 0.5f;
    public float maxChunkMass = 5f;
    bool isChunk = false;

    private float pi = Mathf.PI;

    //Optimization Change
    float threshold = 120;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // only sets mass this way if this isnt a fragment of the original object
        if (!isChunk)
        {
            rb.mass = Random.Range(10, 500);
        }

        transform.localScale = CalculateRadiusFromMass(rb.mass);


    }

    private void Update()
    {
        //Optimization Changes
        if (transform.position.y > threshold || transform.position.y < -threshold || transform.position.x > threshold || transform.position.x < -threshold)
        {
            Destroy(gameObject);
        }
    }
    //Optimization Changes
    private void FixedUpdate()
    {
        transform.localScale = CalculateRadiusFromMass(rb.mass);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        if (rb.mass <= minChunkMass)
        {
            Destroy(gameObject);
            ScoreManager.instance.UpdateScore(500);
            return;
        }

        ContactPoint2D collisionPoint = collision.GetContact(0);
        Vector2 direction = -collisionPoint.normal;

        float chunkMass = Random.Range(minChunkMass, rb.mass * 0.25f);
        rb.mass -= chunkMass;

        GameObject chunk = Instantiate(gameObject, collisionPoint.point, Quaternion.identity);

        Rigidbody2D chunkRb = chunk.GetComponent<Rigidbody2D>();
        DestructibleThing chunkDt = chunk.GetComponent<DestructibleThing>();

        chunkDt.isChunk = true;
        chunkRb.mass = chunkMass;

        int smallPieces = Random.Range(3, 7);

        float remainingMass = chunkMass;

        for (int i = 0; i < smallPieces; i++)
        {
            float pieceMass = (i == smallPieces - 1) ? remainingMass : Random.Range(minChunkMass, chunkMass * 0.4f);

            remainingMass -= pieceMass;

            GameObject frag = Instantiate(chunk, chunk.transform.position, Quaternion.identity);

            Rigidbody2D fragRb = frag.GetComponent<Rigidbody2D>();
            DestructibleThing fragDt = frag.GetComponent<DestructibleThing>();

            fragDt.isChunk = true;
            fragRb.mass = pieceMass;

            Vector2 dir = Quaternion.Euler(0, 0, Random.Range(-30f, 30f)) * direction;

            fragRb.AddForce(dir * Random.Range(5f, 12f), ForceMode2D.Impulse);
        }

        Destroy(chunk);
        ScoreManager.instance.UpdateScore(100);
    }

    public Vector3 CalculateRadiusFromMass (float mass)
    {
        Vector3 size = (Vector3.one * (Mathf.Sqrt(mass / pi)));
        return size;
    }
}
