using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Button nextButton;

    [SerializeField] RectTransform dialoguePopup;

    void Awake()
    {
        Instance = this;
        dialoguePopup.gameObject.SetActive(false);

        nextButton.onClick.AddListener(()=>{
            SceneManager.LoadScene(2); // Journey
        });
    }

    public void ShowDialoguePopup()
    {
        dialoguePopup.gameObject.SetActive(true);
    }

    public void HideDialoguePopup()
    {
        dialoguePopup.gameObject.SetActive(false);

    }
}
