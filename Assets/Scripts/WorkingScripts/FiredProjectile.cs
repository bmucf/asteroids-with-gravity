using Unity.VisualScripting;
using UnityEngine;

public class FiredProjectile : MonoBehaviour
{
    public ParticleSystem explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        newExplosion.transform.localScale = Vector3.one * 0.05f;
        Destroy(gameObject);
    }
}
