using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private TypewriterEffect typewriter;
    [SerializeField] private TMP_Text speakerNameText;

    [Header("Dialogue")]
    [TextArea(3, 10)]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string[] speakerNames;

    void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(string[] dialogues)
    {
        if (typewriter == null)
        {
            Debug.LogError("Typewriter Effect component not assigned!");
            return;
        }

        UIManager.Instance.ShowDialoguePopup();

        typewriter.ClearAll();

        StartCoroutine(DialogueSequence(dialogues));
    }

    private IEnumerator DialogueSequence(string[] dialogues)
    {
        print("::, " + dialogues);
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (speakerNameText != null && i < speakerNames.Length)
            {
                speakerNameText.text = speakerNames[i];
            }

            // Enqueue the next line and wait for it to complete
            typewriter.EnqueueText(dialogues[i]);

            // Wait until the line is fully displayed and player has clicked
            yield return new WaitUntil(() => !typewriter.isDisplayingText && !typewriter.isWaitingForClick);

            // Small delay between lines
            yield return new WaitForSeconds(0.2f);
        }

        print("Dialogue sequence complete");
        yield return new WaitForSeconds(3);

        UIManager.Instance.HideDialoguePopup();
    }
}