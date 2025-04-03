using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text textDisplay;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float delayAfterText = 0.5f;
    [SerializeField] private AudioSource typingSound;
    [SerializeField] private GameObject continueIndicator;

    private Queue<string> textQueue = new Queue<string>();
    public bool isDisplayingText = false;
    public bool isWaitingForClick = false;
    private Coroutine typingCoroutine;

    public UnityEvent onTextComplete;

    private void Start()
    {
        if (textDisplay == null)
            textDisplay = GetComponent<TMP_Text>();

        if (continueIndicator != null)
            continueIndicator.SetActive(false);
    }

    private void Update()
    {
        // Check for click input when waiting between texts
        if (isWaitingForClick && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            isWaitingForClick = false;
            if (continueIndicator != null)
                continueIndicator.SetActive(false);
            
            DisplayNextText();
        }
    }

    /// <summary>
    /// Add a new text to the queue
    /// </summary>
    public void EnqueueText(string text)
    {
        textQueue.Enqueue(text);
        
        // If not already displaying text, start displaying
        if (!isDisplayingText && !isWaitingForClick)
        {
            DisplayNextText();
        }
    }

    /// <summary>
    /// Add multiple texts to the queue
    /// </summary>
    public void EnqueueTexts(string[] texts)
    {
        foreach (string text in texts)
        {
            EnqueueText(text);
        }
    }

    /// <summary>
    /// Skip the current typewriter effect and show the full text immediately
    /// </summary>
    public void SkipTypewriter()
    {
        if (isDisplayingText && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            textDisplay.maxVisibleCharacters = textDisplay.textInfo.characterCount;
            isDisplayingText = false;
            StartCoroutine(WaitForClick());
        }
    }

    /// <summary>
    /// Clear all pending texts and stop any current display
    /// </summary>
    public void ClearAll()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        
        textQueue.Clear();
        isDisplayingText = false;
        isWaitingForClick = false;
        textDisplay.text = "";
        
        if (continueIndicator != null)
            continueIndicator.SetActive(false);
    }

    /// <summary>
    /// Display the next text in the queue with a typewriter effect
    /// </summary>
    private void DisplayNextText()
    {
        if (textQueue.Count > 0)
        {
            string nextText = textQueue.Dequeue();
            typingCoroutine = StartCoroutine(AnimateText(nextText));
        }
        else
        {
            isDisplayingText = false;
        }
    }

    /// <summary>
    /// Coroutine that animates the text one character at a time
    /// </summary>
    private IEnumerator AnimateText(string text)
    {
        isDisplayingText = true;
        
        // Set the text but make all characters invisible
        textDisplay.text = text;
        textDisplay.maxVisibleCharacters = 0;
        
        // Wait a frame to let TextMeshPro calculate the text info
        yield return null;
        
        int totalVisibleCharacters = textDisplay.textInfo.characterCount;
        
        // Reveal one character at a time
        for (int visibleCount = 0; visibleCount <= totalVisibleCharacters; visibleCount++)
        {
            textDisplay.maxVisibleCharacters = visibleCount;
            
            // Play typing sound (if assigned)
            if (typingSound != null && visibleCount < totalVisibleCharacters)
                typingSound.Play();
                
            yield return new WaitForSeconds(typingSpeed);
        }
        
        isDisplayingText = false;
        onTextComplete?.Invoke();
        
        // Wait for player click to continue
        yield return StartCoroutine(WaitForClick());
    }

    /// <summary>
    /// Coroutine that waits for player click before continuing
    /// </summary>
    private IEnumerator WaitForClick()
    {
        // Short delay before showing the continue indicator
        yield return new WaitForSeconds(delayAfterText);
        
        isWaitingForClick = true;
        
        if (continueIndicator != null)
            continueIndicator.SetActive(true);
    }
}