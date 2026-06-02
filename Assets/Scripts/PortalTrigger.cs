using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    // Название сцены — заполняем в Inspector для каждого портала
    [SerializeField] private string sceneName;

    void OnTriggerEnter(Collider other)
    {
        // Реагируем только на героя
        if (other.CompareTag("Hero"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}