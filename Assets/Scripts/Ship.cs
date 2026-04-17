using System.Collections;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class Ship : FloatingBody
{
    [SerializeField] private ShipType shipType;
    public ParticleSystem forwardThruster;
    public ParticleSystem leftThruster;
    public ParticleSystem rightThruster;
    public GameObject torpedoPrefab;

    [SerializeField] public float remainingFuel;
    [SerializeField] private float maxFuel = 100;
    [SerializeField] private float idleConsumption;
    [SerializeField] protected float acceleration;
    public float rotateRate = 45;
    public Transform firePoint;
    bool canFire = true;
    public float fireRate = 0.5f;


    void Start()
    {
        forwardThruster.Stop();
        leftThruster.Stop();
        rightThruster.Stop();

        remainingFuel = maxFuel;
    }


    //switch (shipType)
    //{
    //    case ShipType.Agile:
    //        mass = 0.5f;
    //        acceleration = 1;
    //        idleConsumption = 0.01f;
    //        maxFuel = 100;
    //        break;

    //    case ShipType.Fast:
    //        mass = 1;
    //        acceleration = 3;
    //        idleConsumption = 0.01f;
    //        maxFuel = 100;
    //        break;
    //}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        CameraManager.instance.FollowTarget(transform);
        CameraManager.instance.MiniMapFocus(transform);

        ForwardThrust();
        //RotationThrust();

        ConsumeFuel(idleConsumption);
        HUDManager.instance.UpdateFuelGauge(remainingFuel, maxFuel);

        if (Input.GetMouseButton(0) && canFire)
        {
            StartCoroutine(Fire());
        }
    }

    public void ConsumeFuel(float amount)
    {
        remainingFuel -= amount * Time.deltaTime;
    }

    public void ForwardThrust()
    {
        if (Input.GetKeyDown(KeyCode.Space) && remainingFuel > 0)
        {
            forwardThruster.Play();
        }

        if (Input.GetKey(KeyCode.Space) && remainingFuel > 0)
        {
            velocity += transform.up * acceleration * Time.deltaTime;
            ConsumeFuel(Mathf.Pow(velocity.magnitude, 0.1f));
        }

        if (Input.GetKeyUp(KeyCode.Space) || remainingFuel < 0)
        {
            forwardThruster.Stop();
        }
    }

    public void RotationThrust()
    {
        transform.Rotate(rotation * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q) && remainingFuel != 0)
        {
            rightThruster.Play();
        }

        if (Input.GetKey(KeyCode.Q) && remainingFuel > 0)
        {
            rotation += Vector3.forward * rotateRate * Time.deltaTime;
            ConsumeFuel(Mathf.Pow(velocity.magnitude, 0.1f));
        }

        if (Input.GetKeyUp(KeyCode.Q) || remainingFuel < 0)
        {
            rightThruster.Stop();
        }

        if (Input.GetKeyDown(KeyCode.E) && remainingFuel > 0)
        {
            leftThruster.Play();
        }

        if (Input.GetKey(KeyCode.E) && remainingFuel > 0)
        {
            rotation += Vector3.forward * -rotateRate * Time.deltaTime;
            ConsumeFuel(Mathf.Pow(velocity.magnitude, 0.1f));
        }

        if (Input.GetKeyUp(KeyCode.E) || remainingFuel < 0)
        {
            leftThruster.Stop();
        }
    }

    IEnumerator Fire()
    {
        canFire = false;

        GameObject torpedoObj = Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation); ;
        Torpedo torpedo = torpedoObj.GetComponent<Torpedo>();
        torpedo.SetOwner(this);
        torpedo.velocity += velocity + (transform.up * 5);

        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}

public enum ShipType
{
    Agile,
    Fast
}
