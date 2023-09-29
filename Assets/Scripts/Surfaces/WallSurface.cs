using UnityEngine;

public class WallSurface : BaseHitSurface
{

    public int textureWidth = 256;
    public int textureHeight = 256;
    public GameObject renderTexturePlanePrefab;
    public bool UseRenderTexture = false;
    public Material defaultDecalMat;

    public override void OnSurfaceHit(RaycastHit hitPoint)
    {
        base.OnSurfaceHit(hitPoint);
        Debug.Log("Projectile hit wall");
        
        if(UseRenderTexture)
            CreateDecal(hitPoint);
        else
            CreateTexture(hitPoint);
    }

    private void CreateDecal(RaycastHit hitInfo)
    {
        RenderTexture renderTex = new RenderTexture(textureWidth, textureHeight, 16);
        renderTex.Create();
        GameObject rtPlane = Instantiate(renderTexturePlanePrefab, hitInfo.point, Quaternion.LookRotation(-hitInfo.normal));
        Renderer rtRenderer = rtPlane.GetComponent<Renderer>();
        if (rtRenderer)
        {
            rtRenderer.material = new Material(Shader.Find("Standard"));
            rtRenderer.material.mainTexture = renderTex;
        }
    }

    private void CreateTexture(RaycastHit hitInfo)
    {
        GameObject rtPlane = Instantiate(renderTexturePlanePrefab, hitInfo.point, Quaternion.LookRotation(-hitInfo.normal));
        rtPlane.transform.position -= rtPlane.transform.forward * 0.05f;//no clip texture
        Renderer rtRenderer = rtPlane.GetComponent<Renderer>();
        if (rtRenderer)
        {
            rtRenderer.material = new Material(defaultDecalMat);
        }
    }
}
