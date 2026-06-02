using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    // Скорость движения — можно менять в Inspector не трогая код
    [SerializeField] private float speed = 5f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Запоминаем компонент при старте, чтобы не искать каждый кадр
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Считываем стрелки клавиатуры (-1, 0 или 1)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Собираем направление движения в 3D-пространстве
        // Y = 0 потому что герой не летает
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        // Двигаем героя
        transform.position += direction * speed * Time.deltaTime;

        // Сортировка глубины: чем дальше герой — тем позже рисуется
        // Это и есть замена depth shader для простых сцен
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.z * 10);
    }
}
