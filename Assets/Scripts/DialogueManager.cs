using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework.Internal;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private TypewriterEffect typewriter;
    [SerializeField] private TMP_Text speakerNameText;

    [Header("Dialogue")]
    [TextArea(3, 10)]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string[] speakerNames;

    [HideInInspector]public bool isDialoguePlaying;
    bool isTesting;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) //title scene
        {
            StartDialogue(dialogueLines);
        }
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
        //print("::, " + dialogues);
        isDialoguePlaying = true;

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
            if (!isTesting) { yield return new WaitForSeconds(0.2f); }
        }

        //print("Dialogue sequence complete");
        if (!isTesting) { yield return new WaitForSeconds(3); }

        isDialoguePlaying = false;

        UIManager.Instance.HideDialoguePopup();
    }
}