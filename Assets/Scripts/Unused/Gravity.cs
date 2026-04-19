using UnityEngine;

//public class Gravity : MonoBehaviour
//{
//    public Ship detectedShip;
//    public Movement shipMovement;
//    public GameObject gravitySource;
//    public float thisMass;

//    [SerializeField] private float gravConstant;

//    private void Start()
//    {
//        thisMass = gravitySource.transform.localScale.x * 10;
//    }

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            detectedShip = other.GetComponent<Ship>();
//            shipMovement = other.GetComponent<Movement>();

//            Vector3 direction = transform.position - other.transform.position;
//            float dist = direction.magnitude;

//            if (dist < 0.1)
//            {
//                Destroy(other.gameObject);
//            }

//            direction.Normalize();

//            float forceMultiplier = gravConstant * ((detectedShip.mass * thisMass) / (dist * dist));

//            shipMovement.velocity += direction * forceMultiplier * Time.deltaTime;
//        }


//    }
//}
