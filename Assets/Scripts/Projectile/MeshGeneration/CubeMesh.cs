using UnityEngine;

public class CubeMesh : BaseMesh
{
    private void Start()
    {
		CreateCube();
	}

    private void CreateCube()
	{
		vertices = new Vector3[]{
			new Vector3 (0, 0, 0),
			new Vector3 (RandomSize, 0, 0),
			new Vector3 (RandomSize, RandomSize, 0),
			new Vector3 (0, RandomSize, 0),
			new Vector3 (0, RandomSize, RandomSize),
			new Vector3 (RandomSize, RandomSize, RandomSize),
			new Vector3 (RandomSize, 0, RandomSize),
			new Vector3 (0, 0, RandomSize),
		};

		triangles = new int[]{
			0, 2, 1, //face front
			0, 3, 2,
			2, 3, 4, //face top
			2, 4, 5,
			1, 2, 5, //face right
			1, 5, 6,
			0, 7, 4, //face left
			0, 4, 3,
			5, 4, 7, //face back
			5, 7, 6,
			0, 6, 7, //face bottom
			0, 1, 6
		};
		RebuildMesh();
	}
}
