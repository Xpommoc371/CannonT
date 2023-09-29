using UnityEngine;

public class FieldSurface : BaseHitSurface
{
    public override void OnSurfaceHit(RaycastHit hitPoint)
    {
        base.OnSurfaceHit(hitPoint);
        Debug.Log("Projectile hit grass plane");
    }
}
