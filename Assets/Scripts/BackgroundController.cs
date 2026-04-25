using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Vector3 startPos;
    private float length;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 distance = new Vector3(cam.transform.position.x, cam.transform.position.y, 0) * parallaxEffect;
        Vector3 movement = new Vector3(cam.transform.position.x, cam.transform.position.y, 0) * (1 - parallaxEffect);

        transform.position = startPos + distance;

        if (movement.x > startPos.x + length)
        {
            startPos.x += length;
        }
        else if (movement.x < startPos.x - length)
        {
            startPos.x -= length;
        }

    }
}
