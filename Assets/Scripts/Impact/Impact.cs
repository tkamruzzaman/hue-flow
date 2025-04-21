using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Impact : MonoBehaviour
{
    public static Impact Instance;
    [SerializeField] RectTransform impactPanelRect;
    [SerializeField] Image impactImage;

    [SerializeField] Sprite[] sprites;

    [Space]
    [SerializeField] ImpactButton[] impactButtons;

    [Header("Next Button")]
    [SerializeField] Button nextButton;
    [SerializeField] int sceneNumber;


    void Awake()
    {
        Instance = this;

        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneNumber);
        });

        nextButton.gameObject.SetActive(false);

        HideImpact();
    }

    int currentActive;

    void Start()
    {
        ActiveNextImpact();
    }

    public void ActiveNextImpact()
    {
        if (currentActive > 0) impactButtons[currentActive - 1].DeactiveButton();
        
        if (currentActive >= impactButtons.Length)
        {
            //reach end
            nextButton.gameObject.SetActive(true);
            return;
        }
        impactButtons[currentActive].ActiveButton();
        currentActive++;
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
