using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    // Синглтон — один на всю игру, доступен из любой сцены
    public static ProgressManager Instance;

    // Какие квесты пройдены
    public bool quest1Done = false;
    public bool quest2Done = false;
    public bool quest3Done = false;
    public bool quest4Done = false;

    // Сколько квестов пройдено (0-4) — определяет стадию растения
    public int QuestsCompleted()
    {
        int count = 0;
        if (quest1Done) count++;
        if (quest2Done) count++;
        if (quest3Done) count++;
        if (quest4Done) count++;
        return count;
    }

    void Awake()
    {
        // Если менеджер уже есть — уничтожаем дубликат
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // Запоминаем себя и не уничтожаемся при смене сцен
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}