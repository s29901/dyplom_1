using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    // Сюда перетащишь 5 спрайтов в Inspector — от семечка до дерева
    [SerializeField] private Sprite[] growthStages = new Sprite[5];

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePlant(); // показываем нужную стадию при старте
    }

    // Вызывается при старте и после каждого квеста
    public void UpdatePlant()
    {
        if (ProgressManager.Instance == null) return;

        // Узнаём сколько квестов пройдено (0-4)
        int stage = ProgressManager.Instance.QuestsCompleted();

        // Защита — не выходим за пределы массива
        stage = Mathf.Clamp(stage, 0, growthStages.Length - 1);

        // Меняем спрайт если он назначен
        if (growthStages[stage] != null)
            spriteRenderer.sprite = growthStages[stage];
    }
}