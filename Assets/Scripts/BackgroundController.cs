using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Vector3 startPos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 distance = new Vector3(cam.transform.position.x, cam.transform.position.y, 0) * parallaxEffect;
        transform.position = startPos + distance;
    }
}
