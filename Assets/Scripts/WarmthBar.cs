using UnityEngine;
using UnityEngine.UI;

public class WarmthBar : MonoBehaviour
{
    // Ссылка на заливку — перетащим в Inspector
    [SerializeField] private Image fillImage;

    // Устанавливает заполнение от 0 до 1
    public void SetFill(float value)
    {
        fillImage.fillAmount = Mathf.Clamp01(value);
    }
}
