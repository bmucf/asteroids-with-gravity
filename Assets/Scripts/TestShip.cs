using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class TestShip : FloatingBody
{
    Rigidbody2D ship;



    public void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ForwardThrust();
        LateralThrust();
    }

    public void ForwardThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            ship.AddForce(transform.up, ForceMode2D.Force);
        }
    }

    public void LateralThrust()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            ship.AddForce(Vector3.zero, ForceMode2D.Force);
            return;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ship.AddForce(-transform.right, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ship.AddForce(transform.right, ForceMode2D.Force);
        }
    }

}