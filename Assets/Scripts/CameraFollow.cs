using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;

    [Header("Межі фону")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float extra_min_z = 30f;

    private Vector3 offset;
    private float minX, maxX, minZ, maxZ;

    void Start()
    {
        offset = transform.position - target.position;

        if (background != null)
        {
            Camera cam = Camera.main;
            Vector3 fwd = cam.transform.forward;
            Bounds b = background.bounds;

            float orthH = cam.orthographicSize;
            float halfW = orthH * cam.aspect;
            minX = b.min.x + halfW;
            maxX = b.max.x - halfW;

            // Камера нахилена: fwd.z = cos(кута), fwd.y = -sin(кута)
            // Щоб знайти Z камери, при якому край екрана збігається з краєм фону по Y,
            // використовуємо проекцію через кут нахилу
            float bgZ   = background.transform.position.z;
            float camY  = transform.position.y;
            float cosA  = fwd.z;
            float tanA  = -fwd.y / fwd.z;
            float viewHalfH = orthH / cosA; // покриття по Y фону від центру до краю екрана

            float camZTop    = bgZ + (b.max.y - camY - viewHalfH) / tanA;
            float camZBottom = bgZ + (b.min.y - camY + viewHalfH) / tanA;
            minZ = Mathf.Min(camZTop, camZBottom) - extra_min_z;
            maxZ = Mathf.Max(camZTop, camZBottom);
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