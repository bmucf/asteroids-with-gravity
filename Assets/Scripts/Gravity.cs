using UnityEngine;

public class Gravity : MonoBehaviour
{
    public FloatingBody affectedBody;
    public Movement shipMovement;
    public GameObject gravitySource;
    public float thisMass;

    [SerializeField] private float gravConstant;

    private void Start()
    {
        thisMass = gravitySource.transform.localScale.x * 10;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        affectedBody = other.GetComponent<FloatingBody>();

        Vector3 direction = transform.position - other.transform.position;
        float distance = direction.magnitude;

        if (distance < 0.1)
        {
            Destroy(other.gameObject);
        }

        direction.Normalize();

        float forceMultiplier = gravConstant * ((affectedBody.mass * thisMass) / (distance * distance));

        affectedBody.velocity += direction * forceMultiplier * Time.deltaTime;

        Debug.Log(affectedBody.velocity);
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        detectedShip = other.GetComponent<Ship>();
    //        shipMovement = other.GetComponent<Movement>();

    //        Vector3 direction = transform.position - other.transform.position;
    //        float dist = direction.magnitude;

    //        if (dist < 0.1)
    //        {
    //            Destroy(other.gameObject);
    //        }

    //        direction.Normalize();

    //        float forceMultiplier = gravConstant * ((detectedShip.mass * thisMass) / (dist * dist));

    //        shipMovement.velocity += direction * forceMultiplier * Time.deltaTime;
    //    }


    //}
}
