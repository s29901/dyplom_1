using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // герой
    [SerializeField] private float smoothSpeed = 5f; // плавность

    // Смещение камеры относительно героя — не меняем
    private Vector3 offset;

    void Start()
    {
        // Запоминаем начальное смещение
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Камера мгновенно следует за героем — без задержки
        transform.position = target.position + offset;
    }
}