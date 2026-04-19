using Unity.VisualScripting;
using UnityEngine;

public class FiredProjectile : MonoBehaviour
{
    public ParticleSystem explosion;

    //Optimization Change
    TestShip player;
    float threshold = 120;
    private void Awake()
    {
        player = FindAnyObjectByType<TestShip>();
    }
    private void Update()
    {
        if (transform.position.y > threshold || transform.position.y < -threshold || transform.position.x > threshold || transform.position.x < -threshold)
        {
            player.ReturnObject(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        newExplosion.transform.localScale = Vector3.one * 0.05f;

        //Optimization Change
        player.ReturnObject(this.gameObject);
        //Destroy(gameObject);
    }
}
