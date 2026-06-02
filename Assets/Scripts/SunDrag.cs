using UnityEngine;

public class SunDrag : MonoBehaviour
{
    // Плоскость на высоте Y=3 — солнышко летает здесь
    private Plane sunPlane;

    void Start()
    {
        sunPlane = new Plane(Vector3.up, new Vector3(0, 3, 0));
    }

    void Update()
    {
        // Пускаем луч из камеры через позицию мыши
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        // Находим где луч пересекает плоскость
        if (sunPlane.Raycast(ray, out distance))
        {
            Vector3 worldPos = ray.GetPoint(distance);
            // Солнышко следует за мышкой
            transform.position = worldPos;
        }
    }
}