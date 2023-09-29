using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class BaseMesh : MonoBehaviour
{
	[SerializeField]
	protected float size = 1;
	[SerializeField]
	protected float randomOffset = 0.1f;
	protected float fluctuationOffset = 0.1f;
	protected Mesh mesh;
	protected Vector3[] vertices;
	protected int[] triangles;

	public float RandomSize { get { return size + Random.Range(-randomOffset, randomOffset); } }

	void Awake()
	{
		mesh = GetComponent<MeshFilter>().mesh;
	}

	private void Update()
	{
		//fluctuateMesh();
	}

	protected void RebuildMesh()
	{
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.Optimize();
		mesh.RecalculateNormals();
	}

	private void fluctuateMesh()
	{

		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] += Vector3.one * Random.Range(-fluctuationOffset, fluctuationOffset);
		}
		RebuildMesh();
	}
}
