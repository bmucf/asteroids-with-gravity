using System.Collections;
using UnityEngine;

public class TestShip : MonoBehaviour
{
    Rigidbody2D shipRB;
    public Transform firePoint;

    [Header("Stats")]
    public float powerCapacity = 100f;
    private float remainingPower;

    public float fullIntegrity = 100f;
    private float remainingIntegrity;

    public float storageCapacity = 100f;
    private float remainingStorage;

    [Header("Thrusters")]
    public float thrustFwd = 0.5f;
    public float thrustLat = 0.5f;
    public float thrustAdj = 0.5f;
    private float thrustMult = 1f;

    [Header("Weapon Data")]
    public GameObject slug;
    public float fireRate = 1f;
    bool canFire = true;

    public void Start()
    {
        shipRB = GetComponent<Rigidbody2D>();

        remainingPower = powerCapacity;
        remainingIntegrity = fullIntegrity;
        remainingStorage = storageCapacity;
    }

    private void FixedUpdate()
    {
        Turn();
        ForwardThrust();
        LateralThrust();
    }

    private void Update()
    {
        CameraManager.instance.FollowTarget(transform);
        CameraManager.instance.MiniMapFocus(transform);

        HUDManager.instance.UpdateDisplayedCoordinates((Vector2)transform.position);

        ConsumePower(0.01f);
        HUDManager.instance.UpdateFuelGauge(remainingPower, powerCapacity);

        Afterburner();

        if (Input.GetMouseButton(0) && canFire)
        {
            StartCoroutine(Fire());
        }
    }

    public void ForwardThrust()
    {
        if (Input.GetKey(KeyCode.W) && remainingPower > 0)
        {
            shipRB.AddForce(transform.up * thrustFwd * thrustMult, ForceMode2D.Force);
            ConsumePower(Mathf.Pow(remainingPower, 0.1f));
        }
    }

    public void LateralThrust()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && remainingPower > 0)
        {
            shipRB.AddForce(Vector3.zero, ForceMode2D.Force);
            ConsumePower(1f);
            return;
        }
        else if (Input.GetKey(KeyCode.A) && remainingPower > 0)
        {
            shipRB.AddForce(-transform.right * thrustLat * thrustMult, ForceMode2D.Force);
            ConsumePower(0.5f);
        }
        else if (Input.GetKey(KeyCode.D) && remainingPower > 0)
        {
            shipRB.AddForce(transform.right * thrustLat * thrustMult, ForceMode2D.Force);
            ConsumePower(0.5f);
        }

    }

    public void Turn()
    {
        Vector2 mousePos = CameraManager.instance.mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = shipRB.position - mousePos;
        
        if (direction.magnitude > 0.5f && remainingPower > 0)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shipRB.rotation = Mathf.LerpAngle(shipRB.rotation, angle + 90, ((thrustAdj * thrustMult) / shipRB.mass) * Time.deltaTime);
        }
    }

    public void Afterburner()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thrustMult = 4f;
            ConsumePower(2f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            thrustMult = 1f;
        }
    }

    public void ConsumePower(float usage)
    {
        remainingPower -= usage * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        remainingIntegrity -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        float speed = collision.relativeVelocity.magnitude;

        if (speed < 2f) return; // ignore micro-collisions

        float damage = rb.mass * speed * speed * 0.01f;
        
        TakeDamage(damage);
        HUDManager.instance.UpdateArmorGauge(remainingIntegrity, fullIntegrity);

        if (remainingIntegrity <= 0.01f)
        {
            Destroy(gameObject);
            GameManager.instance.SwitchScenes("GameOver");
        }
    }

    IEnumerator Fire()
    {
        canFire = false;

        GameObject newSlug = Instantiate(slug, firePoint.position, firePoint.rotation);
        Rigidbody2D firedSlug = newSlug.GetComponent<Rigidbody2D>();

        firedSlug.linearVelocity = shipRB.linearVelocity;

        // add firing direction
        firedSlug.AddForce(transform.up, ForceMode2D.Impulse);

        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }


    public void NullMovement()
    {
        float xToZero = 0 + shipRB.linearVelocityX;
        float yToZero = 0 + shipRB.linearVelocityY;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (shipRB.linearVelocityX <= 0.05f)
            {
                shipRB.linearVelocityX = 0;
            }
            else shipRB.linearVelocityX += -xToZero * thrustAdj * Time.deltaTime;


            if (shipRB.linearVelocityY <= 0.05f)
            {
                shipRB.linearVelocityY = 0;
            }
            else shipRB.linearVelocityY += -yToZero * thrustAdj * Time.deltaTime;
        }
    }

}