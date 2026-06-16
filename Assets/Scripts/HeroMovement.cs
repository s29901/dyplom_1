using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    // Скорость движения — можно менять в Inspector не трогая код
    [SerializeField] private float speed = 5f;

    private SpriteRenderer spriteRenderer;
    private Camera cam;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Проєктуємо напрям камери на горизонтальну площину XZ,
        // щоб рух "вперед/назад/вбік" збігався з тим, що видно на екрані
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight   = cam.transform.right;
        camForward.y = 0f; camForward.Normalize();
        camRight.y   = 0f; camRight.Normalize();

        Vector3 direction = (camRight * h + camForward * v).normalized;

        transform.position += direction * speed * Time.deltaTime;

        // Глибина: менше Z (ближче до камери) → вищий sortingOrder → малюється поверх
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.z * 10) + 100;
    }
}
