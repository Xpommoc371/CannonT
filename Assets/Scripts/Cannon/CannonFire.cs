using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    public Transform launchPoint;
    public ProjectilePool pool;

    [SerializeField]
    public CannonControl cannon;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        pool.LaunchProjectile(launchPoint.position, cannon.GetFireRotation, cannon.GetFireVelocity);
        StaticEvents.OnCannonShot.Invoke();
    }
}
