using UnityEngine;
using System.Collections;

public class CloudDrag : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 1f;    // скорость патруля
    [SerializeField] private float patrolRange = 2f;    // ширина качания
    [SerializeField] private float rainDuration = 3f;   // секунд удержания над деревом
    [SerializeField] private Transform tree;             // дерево
    [SerializeField] private float treeRadius = 2f;     // зона над деревом
    [SerializeField] private GameObject rainObject;      // дочерний объект с анимацией дождя

    private Vector3 startPosition;
    private bool isDragging = false;
    private bool isReturning = false;
    private bool isDone = false;
    private float rainProgress = 0f;
    private float patrolAngle = 0f;
    private Plane dragPlane;
    private SpriteRenderer spriteRenderer;

    // Сообщаем QuestManager когда облако пролито
    public System.Action OnCloudDone;

    void Start()
    {
        startPosition = transform.position;
        dragPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        if (rainObject != null) rainObject.SetActive(false);

        // Облака рисуются поверх героя (они выше по сцене)
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.sortingOrder = 200;
    }

    void Update()
    {
        if (isDone) return;

        if (isDragging)
            HandleDrag();
        else if (!isReturning)
            HandlePatrol();
    }

    void HandlePatrol()
    {
        // Плавное качание по X и Z — покрывает всю сцену
        patrolAngle += Time.deltaTime * patrolSpeed;
        transform.position = startPosition + new Vector3(
            Mathf.Sin(patrolAngle) * patrolRange,
            0,
            Mathf.Cos(patrolAngle * 0.7f) * patrolRange * 0.5f
        );
    }

    void HandleDrag()
    {
        // Тащим облако за мышкой
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist;
        if (dragPlane.Raycast(ray, out dist))
            transform.position = ray.GetPoint(dist);

        // Над деревом?
        float distToTree = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.z),
            new Vector2(tree.position.x, tree.position.z)
        );

        if (distToTree < treeRadius)
        {
            // Дождь идёт
            if (rainObject != null) rainObject.SetActive(true);
            rainProgress += Time.deltaTime;
            if (rainProgress >= rainDuration)
            {
                CompleteRain();
                return;
            }
        }
        else
        {
            // Не над деревом — дождь стоп
            if (rainObject != null) rainObject.SetActive(false);
        }

        // Игрок отпустил мышку
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rainProgress = 0f;
            if (rainObject != null) rainObject.SetActive(false);
            StartCoroutine(ReturnToStart());
        }
    }

    void OnMouseDown()
    {
        if (!isDone && !isReturning)
            isDragging = true;
    }

    void CompleteRain()
    {
        isDone = true;
        isDragging = false;
        if (rainObject != null) rainObject.SetActive(false);
        OnCloudDone?.Invoke(); // сообщаем QuestManager
        StartCoroutine(DisappearEffect());
    }

    IEnumerator DisappearEffect()
    {
        // Облако плавно уменьшается и исчезает
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, elapsed / 0.5f);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    IEnumerator ReturnToStart()
    {
        isReturning = true;
        float elapsed = 0f;
        Vector3 currentPos = transform.position;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPos, startPosition, elapsed);
            yield return null;
        }
        patrolAngle = 0f; // чтобы не было прыжка
        isReturning = false;
    }
}