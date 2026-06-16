using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    void OnTriggerEnter(Collider other)
    {
        // Выводим в консоль ЧТО вошло в триггер
        Debug.Log("Триггер сработал! Объект: " + other.gameObject.name + " Тег: " + other.tag);
        
        if (other.CompareTag("Hero"))
        {
            Debug.Log("Загружаем сцену: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}