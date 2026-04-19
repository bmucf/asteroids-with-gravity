using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Camera mainCam;
    public Camera mapCam;

    private void Awake()
    {
        instance = this;
    }

    public void FollowTarget(Transform target)
    {
        mainCam.transform.position = new Vector3(target.position.x, target.position.y, -10);
    }

    public void MiniMapFocus(Transform focus)
    {
        mapCam.transform.position = mainCam.transform.position;
    }
}