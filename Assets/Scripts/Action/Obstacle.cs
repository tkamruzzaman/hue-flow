using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int layerGroup = 1;

    [HideInInspector] public SpriteRenderer _sr;
    [HideInInspector] public Collider2D _collider;
    [Space]
    [TextArea(3, 10)][SerializeField] string[] dialogues;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        _sr.sortingLayerName = "Game";
        _sr.sortingOrder = layerGroup;

        _collider.enabled = false;
    }

    public void EnableInteraction(bool enable)
    {
        _collider.enabled = enable;
    }

    void OnMouseDown()
    {
        if (DialogueManager.Instance.isDialoguePlaying) { return; }

        //print("Clickeddddd");
        GameManager.Instance.RemoveObstacle(this);
        StartCoroutine(IE_StartDialouge());
    }

    IEnumerator IE_StartDialouge()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.StartDialogue(dialogues);
        gameObject.SetActive(false);
    }

}