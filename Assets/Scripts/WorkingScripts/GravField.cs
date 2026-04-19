using UnityEngine;

public class GravField : MonoBehaviour
{
    Rigidbody2D sourceMass;

    private void Start()
    {
        sourceMass = GetComponentInParent<Rigidbody2D>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Projectile"))
        {
            Rigidbody2D affectedBody = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 toCenter = (Vector2)transform.position - affectedBody.position;
            float distance = toCenter.magnitude;
            float forceMult = (affectedBody.mass * sourceMass.mass) / (distance * distance);

            if (distance < 0.01f)
            {
                return;
            }

            affectedBody.AddForce(toCenter.normalized * forceMult, ForceMode2D.Force);
        }
    }
}
