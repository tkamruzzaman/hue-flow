using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] GameObject endScreen;
    [SerializeField] TMP_Text endText;

    void Awake()
    {
        endScreen.SetActive(false);
    }

    void OnMouseDown()
    {
        if (DialogueManager.Instance.isDialoguePlaying) { return; }

        endScreen.SetActive(true);
    }
}
