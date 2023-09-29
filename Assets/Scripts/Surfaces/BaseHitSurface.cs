using UnityEngine;

public class BaseHitSurface : MonoBehaviour, ISurface
{
    public virtual void OnSurfaceHit(RaycastHit hitPoint)
    {
    }
}
