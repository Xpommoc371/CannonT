using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float gravity = 9.81f;
    public float reflectionDampening = 0.9f;
    public int maxBounceNum = 2;


    public Vector3 Velocity { get; set; }

    private bool inMotion = false;
    private int curBounceNum = 0;

    void Update()
    {
        if (inMotion)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Velocity.normalized, out hit, Velocity.magnitude * Time.deltaTime))
            {
                if (curBounceNum >= maxBounceNum) Disable();
                ReflectOffSurface(hit);
                if (hit.collider.TryGetComponent<ISurface>(out ISurface surf)) ;
                surf.OnSurfaceHit(hit);
            }
            else
            {
                SimulateProjectileMotion();
            }
        }
        Disable();
    }

    private void Disable()
    {
        if (transform.position.y < 0)
        {
            gameObject.SetActive(false);
            curBounceNum = 0;
            OnExplode();
        }
    }

    public void Launch(Vector3 direction)
    {
        Velocity = direction;
        inMotion = true;
    }

    void SimulateProjectileMotion()
    {
        Vector3 displacement = Velocity * Time.deltaTime;
        transform.position += displacement;
        Velocity -= Vector3.up * gravity * Time.deltaTime;
    }

    void ReflectOffSurface(RaycastHit hit)
    {
        curBounceNum += 1;
        transform.position = hit.point;
        Velocity = Vector3.Reflect(Velocity, hit.normal);
        Velocity *= reflectionDampening;
    }

    private void OnExplode()
    {
        ExplosionPooler.Instance.SpawnExplosionWithAutoDisable(transform.position);
    }
}
