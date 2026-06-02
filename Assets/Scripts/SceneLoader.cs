using UnityEngine;
using UnityEngine.SceneManagement; // нужно для работы со сценами

public class SceneLoader : MonoBehaviour
{
    // Загружает любую сцену по имени — используем везде
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Быстрый возврат в хаб — будем вызывать после каждого квеста
    public void LoadHub()
    {
        SceneManager.LoadScene("02_HubGarden");
    }

    // Следующая сцена по порядку — удобно для интро и концовки
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}