using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPooler : MonoBehaviour
{
    public static ExplosionPooler Instance;

    public GameObject explosionPrefab;
    public int poolSize = 10;
    public float explosionLifetime = 2f;

    private Queue<GameObject> explosions = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, new Quaternion(), transform);
            explosion.transform.position = Vector3.zero;
            explosion.SetActive(false);
            explosions.Enqueue(explosion);
        }
    }

    public GameObject SpawnExplosion(Vector3 position)
    {
        if (explosions.Count == 0)
        {
            Debug.LogWarning("Explosion pool empty! Consider increasing pool size.");
            return null;
        }

        GameObject explosion = explosions.Dequeue();
        explosion.transform.position = position;
        explosion.SetActive(true);
        return explosion;
    }

    public void SpawnExplosionWithAutoDisable(Vector3 position)
    {
        StartCoroutine(AutoDisable(position));
    }

    IEnumerator AutoDisable(Vector3 position)
    {
        GameObject explosion = SpawnExplosion(position);
        yield return new WaitForSeconds(explosionLifetime);
        ReturnExplosion(explosion);
    }

    public void ReturnExplosion(GameObject explosion)
    {
        explosion.SetActive(false);
        explosions.Enqueue(explosion);
    }
}