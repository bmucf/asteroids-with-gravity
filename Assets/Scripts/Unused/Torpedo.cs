using UnityEngine;

//public class Torpedo : FloatingBody
//{
//    private FloatingBody owner;

//    private void Start()
//    {
//        owner = GetComponent<FloatingBody>();
//    }

//    protected override void Update()
//    {
//        base.Update();

//        Vector2 dir = velocity.normalized;
//        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90 );
//    }

//    public void SetOwner(FloatingBody ship)
//    {
//        owner = ship;
//    }


//}
