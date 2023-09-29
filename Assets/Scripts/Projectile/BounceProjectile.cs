using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BounceProjectile : MonoBehaviour
{
    public float speed = 10f;
    public UnityEvent<Vector3> OnReflectStarted = new UnityEvent<Vector3>();

    public Vector3 currentDirection;

    void Update()
    {
        //transform.position += currentDirection * speed * Time.deltaTime;

        Ray ray = new Ray(transform.position, currentDirection);
        RaycastHit hitInfo;
        Debug.DrawRay(transform.position, currentDirection * speed * Time.deltaTime, Color.red);

        if (Physics.Raycast(ray, out hitInfo, speed * Time.deltaTime))
        {
            ReflectOffSurface(hitInfo.normal);
        }
    }

    void ReflectOffSurface(Vector3 normal)
    {
        currentDirection = Vector3.Reflect(currentDirection, normal);
        OnReflectStarted.Invoke(currentDirection);
    }
}
