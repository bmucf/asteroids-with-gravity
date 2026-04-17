using UnityEngine;

public class Movement : MonoBehaviour
{
    public Ship info;

    public Vector3 velocity = Vector3.zero;
    public Vector3 rotation = Vector3.zero;

    public ParticleSystem forwardThruster;
    public ParticleSystem leftThruster;
    public ParticleSystem rightThruster;

    public Ship playerShip;
    public float acceleration = 1;
    public float rotateRate = 45;
    public float mass = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        forwardThruster.Stop();
        leftThruster.Stop();
        rightThruster.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ForwardThrust();
        RotationThrust();        
    }

    public void ForwardThrust()
    {
        if (Input.GetKeyDown(KeyCode.Space) && info.remainingFuel > 0)
        {
            forwardThruster.Play();
        }

        if (Input.GetKey(KeyCode.Space) && info.remainingFuel > 0)
        {
            velocity += transform.up * acceleration * Time.deltaTime;
            playerShip.ConsumeFuel(Mathf.Pow(velocity.magnitude, 0.1f));
        }

        if (Input.GetKeyUp(KeyCode.Space) || info.remainingFuel < 0)
        {
            forwardThruster.Stop();
        }
    }

    public void RotationThrust()
    {
        transform.Rotate(rotation * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q) && info.remainingFuel != 0)
        {
            rightThruster.Play();
        }

        if (Input.GetKey(KeyCode.Q) && info.remainingFuel > 0)
        {
            rotation += Vector3.forward * rotateRate * Time.deltaTime;
            playerShip.ConsumeFuel(Mathf.Pow(velocity.magnitude, 0.1f));
        }

        if (Input.GetKeyUp(KeyCode.Q) || info.remainingFuel < 0)
        {
            rightThruster.Stop();
        }

        if (Input.GetKeyDown(KeyCode.E) && info.remainingFuel > 0)
        {
            leftThruster.Play();
        }

        if (Input.GetKey(KeyCode.E) && info.remainingFuel > 0)
        {
            rotation += Vector3.forward * -rotateRate * Time.deltaTime;
            playerShip.ConsumeFuel(Mathf.Pow(velocity.magnitude, 0.1f));
        }

        if (Input.GetKeyUp(KeyCode.E) || info.remainingFuel < 0)
        {
            leftThruster.Stop();
        }
    }
}