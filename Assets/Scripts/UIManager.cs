using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] RectTransform dialoguePopup;

    [SerializeField] Button nextButton;
    [SerializeField] int sceneNumber;

    void Awake()
    {
        Instance = this;
        dialoguePopup.gameObject.SetActive(false);

        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneNumber);
        });

        nextButton.gameObject.SetActive(false);
    }

    public void ShowDialoguePopup()
    {
        dialoguePopup.gameObject.SetActive(true);
    }

    public void HideDialoguePopup()
    {
        dialoguePopup.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == 0) //title scene
        {
            ActiveNextButton();
        }
    }

    public void ActiveNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }
}
