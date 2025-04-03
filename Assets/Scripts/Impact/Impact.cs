using UnityEngine;
using UnityEngine.UI;

public class Impact : MonoBehaviour
{
    [SerializeField] RectTransform impactPanelRect;
    [SerializeField] Image impactImage;

    [SerializeField] Sprite[] sprites;

    void Awake()
    {
        HideImpact();
    }

    public void ShowImpact(int index)
    {
        impactImage.sprite = sprites[index];
        impactPanelRect.gameObject.SetActive(true);
    }

    public void HideImpact()
    {
        impactPanelRect.gameObject.SetActive(false);

    }
}
