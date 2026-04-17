using UnityEngine;

public class FloatingBody : MonoBehaviour
{
    [Header("Positional Data")]
    public Vector3 velocity = Vector3.zero;
    public Vector3 rotation = Vector3.zero;

    [Header("Physical Properties")]
    [SerializeField] public float mass;
    [SerializeField] protected float durability;

    protected virtual void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}

