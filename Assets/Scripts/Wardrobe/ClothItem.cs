using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ClothItem : MonoBehaviour
{
    [SerializeField] int clothItemId;
    [SerializeField] bool isSelected;
    [SerializeField] float scaleFactor = 1.1f;

    [TextArea(3, 10)]
    [SerializeField] string[] clothPrompt;

    float initialScale;

    WardrobeController wardrobeController;

    void Awake()
    {
        initialScale = transform.localScale.x;
        wardrobeController = GetComponentInParent<WardrobeController>();
    }

    void OnMouseDown()
    {
        SelectItem();
    }

    private void SelectItem()
    {
        wardrobeController.DeselectAllClothes();
        //Show outline
        //Zoom item
        transform.DOScale(initialScale * scaleFactor, 0.5f).OnComplete(() =>
        {
            isSelected = true;
            StartCoroutine(IE_ShowText());
        });
    }

    private IEnumerator IE_ShowText()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueManager.Instance.StartDialogue(clothPrompt);
    }

    public void DeselectItem()
    {
        if (!isSelected) { return; }

        isSelected = false;
        transform.DOScale(initialScale, 0.2f);

    }
}
