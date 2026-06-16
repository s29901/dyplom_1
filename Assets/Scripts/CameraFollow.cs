using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;

    [Header("Межі фону")]
    [SerializeField] private SpriteRenderer background;

    private Vector3 offset;
    private float minX, maxX, minZ, maxZ;

    void Start()
    {
        offset = transform.position - target.position;

        if (background != null)
        {
            Bounds b = background.bounds;
            minX = b.min.x;
            maxX = b.max.x;
            // спрайт лежить на XY площині, тому вертикаль фону = b.Y → клампимо Z камери
            minZ = b.min.y;
            maxZ = b.max.y;
        }
    }

    void LateUpdate()
    {
        Vector3 desired = target.position + offset;

        if (background != null)
        {
            desired.x = Mathf.Clamp(desired.x, minX, maxX);
            desired.z = Mathf.Clamp(desired.z, minZ, maxZ);
        }

        transform.position = desired;
    }
}