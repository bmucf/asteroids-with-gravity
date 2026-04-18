using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravConstant = 6;
    public FloatingBody affectedBody;
    public FloatingBody gravitySource;

    private void OnTriggerStay2D(Collider2D other)
    {
        gravitySource = transform.parent.GetComponent<FloatingBody>();

        affectedBody = other.GetComponent<FloatingBody>();

        Vector3 direction = transform.position - other.transform.position;
        float distance = direction.magnitude;

        if (distance < 0.1)
        {
            Destroy(other.gameObject);
        }

        direction.Normalize();

        float forceMultiplier = gravConstant * ((affectedBody.mass * gravitySource.mass) / (distance * distance));

        affectedBody.velocity += direction * forceMultiplier * Time.deltaTime;

        Debug.Log(affectedBody.velocity);
    }
}
