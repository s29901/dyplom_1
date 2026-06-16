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
            Camera cam = Camera.main;
            float halfH = cam.orthographicSize;
            float halfW = halfH * cam.aspect;

            Bounds b = background.bounds;
            minX = b.min.x + halfW;
            maxX = b.max.x - halfW;
            minZ = b.min.y + halfH;
            maxZ = b.max.y - halfH;
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