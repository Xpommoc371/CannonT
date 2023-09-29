using System;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    public Transform Barrel;
    public float rotationSpeed = 10;
    public Vector2 RotationLimitX = new Vector2(-45, 45);
    public Vector2 RotationLimitY = new Vector2(0, 45);
    public float projectileSpeedMultiplayer = 2f;

    float rotXAxis, rotYAxis;
    private Vector3 _shotDirection;

    float baseLaunchSpeed = 5f;
    float launchSpeedMultiplayer = 5f;
    float launchSpeed { get; set; } = 1;

    public Vector3 ShotDirection { get { return _shotDirection; } }

    public Vector3 GetFireVelocity
    {
        get { return Barrel.transform.forward * launchSpeed * projectileSpeedMultiplayer; }
    }

    public Quaternion GetFireRotation { get { return Barrel.rotation; } }

    void Start()
    {
        rotYAxis = transform.eulerAngles.y;
        rotXAxis = transform.eulerAngles.x;

        StaticEvents.OnPowerChanged.AddListener(OnPowerChanged);
    }

    private void OnPowerChanged(float newVal)
    {
        launchSpeed = baseLaunchSpeed + newVal*launchSpeedMultiplayer/100;
    }

    private  void Update()
    {
        GetInput();
        CalculateRotation();
        CalculateTrajectory();
    }

    private void GetInput()
    {
        rotXAxis += Input.GetAxis("Mouse X") * rotationSpeed;
        rotYAxis -= Input.GetAxis("Mouse Y") * rotationSpeed;
        rotXAxis = ClampAngle(rotXAxis, RotationLimitX.x, RotationLimitX.y);
        rotYAxis = ClampAngle(rotYAxis, RotationLimitY.x, RotationLimitY.y);
    }

    private void CalculateRotation()
    {
        Quaternion lafetRotation = Quaternion.Euler(0, rotXAxis, 0);
        Quaternion barrelRotation = Quaternion.Euler(rotYAxis, rotXAxis, 0);
        transform.rotation = lafetRotation;
        Barrel.rotation = barrelRotation;
    }

    private void CalculateTrajectory()
    {
        float verticalLaunchAngleRad = (Barrel.rotation.x - 45) * Mathf.Deg2Rad;
        float horizontalLaunchAngleRad = transform.rotation.y * Mathf.Deg2Rad;

        float x = launchSpeed * Mathf.Cos(verticalLaunchAngleRad) * Mathf.Sin(horizontalLaunchAngleRad);
        float y = -10 / launchSpeed * Mathf.Sin(verticalLaunchAngleRad);
        float z = launchSpeed * Mathf.Cos(verticalLaunchAngleRad) * Mathf.Cos(horizontalLaunchAngleRad);
        _shotDirection = new Vector3(x, y, z);
    }

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
