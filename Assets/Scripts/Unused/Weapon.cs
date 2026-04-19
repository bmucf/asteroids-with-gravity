using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform originPoint;
    public GameObject projectile;
    public float fireRate = 0.5f;
    private bool canFire = true;


    private void Update()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            StartCoroutine(Fire());
        }
    }
    IEnumerator Fire()
    {
        canFire = false;

        Instantiate(projectile, originPoint.position, originPoint.rotation);

        yield return new WaitForSeconds(fireRate);

        canFire = true;
    }
}
