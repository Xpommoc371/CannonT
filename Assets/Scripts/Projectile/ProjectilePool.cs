using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 10;
    private List<GameObject> projectiles;

    private void Start()
    {
        projectiles = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateProjectile();
        }
    }

    public GameObject GetProjectile()
    {
        foreach (var projectile in projectiles)
        {
            if (!projectile.activeInHierarchy)
            {
                return projectile;
            }
        }
        return CreateProjectile();
    }

    private GameObject CreateProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab);
        proj.SetActive(false);
        proj.transform.parent = transform;
        proj.transform.localPosition = Vector3.zero;
        projectiles.Add(proj);
        return proj;
    }

    public void LaunchProjectile(Vector3 position, Quaternion orientation, Vector3 direction)
    {
        GameObject projectile = GetProjectile();
        if (projectile != null)
        {
            projectile.transform.position = position;
            projectile.transform.rotation = orientation;
            projectile.SetActive(true);
            projectile.GetComponent<Projectile>().Launch(direction);
        }
    }
}