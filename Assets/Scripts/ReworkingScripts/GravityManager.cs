using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] const float gravitation = 6;
    public static GravityManager Instance;
    private List<FloatingBody> attractors = new List<FloatingBody>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void Register(FloatingBody floater)
    {
        if (!attractors.Contains(floater))
            attractors.Add(floater);
    }

    public void Unregister(FloatingBody floater)
    {
        attractors.Remove(floater);
    }

    void Update()
    {
        ApplyGravity();
        MoveBodies();
    }

    void ApplyGravity()
    {
        for (int i = 0; i < attractors.Count; i++)
        {
            for (int j = 0; j < attractors.Count; j++)
            {
                if (i == j) continue;

                FloatingBody a = attractors[i];
                FloatingBody b = attractors[j];

                Vector3 direction = (b.transform.position - a.transform.position);
                float distance = direction.magnitude;

                if (distance < 0.1f) continue;

                direction.Normalize();

                float force = gravitation * (a.mass * b.mass) / (distance * distance);
                Vector3 acceleration = direction * (force / a.mass);

                //a.velocity += acceleration * Time.deltaTime;
            }
        }
    }

    void MoveBodies()
    {
        foreach (var body in attractors)
        {
            //body.transform.position += body.velocity * Time.deltaTime;
        }
    }
}

