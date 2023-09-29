using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public CannonControl cannonballScript;
    public LineRenderer lineRenderer;
    public int resolution = 100; // Number of segments for the trajectory line
    public float gravity = 9.81f;

    void Start()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector3[] points = new Vector3[resolution];

        float maxTime = CalculateMaxTime();

        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)resolution * maxTime;
            points[i] = CalculateTrajectoryPoint(t);
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points);
    }

    Vector3 CalculateTrajectoryPoint(float time)
    {
        float x = cannonballScript.ShotDirection.x * time;
        float y = cannonballScript.ShotDirection.y * time - (0.5f * gravity * time * time);
        float z = cannonballScript.ShotDirection.z * time;
        return new Vector3(x, y, z);
    }

    float CalculateMaxTime()
    {
        return 2 * cannonballScript.ShotDirection.y / gravity;
    }
}