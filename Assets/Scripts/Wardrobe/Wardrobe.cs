using DG.Tweening;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    WardrobeController wardrobeController;
    Vector3 initialScale;
    [SerializeField] float scaleFactor = 1.05f;
    [SerializeField] float scaleDuration = 0.5f;
    Tween scaleTween;

    void Awake()
    {
        initialScale = transform.localScale;
        wardrobeController = GetComponentInParent<WardrobeController>();
    }

    void Start()
    {
        scaleTween = transform.DOScale(initialScale * scaleFactor, scaleDuration).SetLoops(-1, LoopType.Yoyo);
    }

    void OnMouseDown()
    {
        OpenWardrobe();
    }

    void OpenWardrobe()
    {
        wardrobeController.OpenWardrobe();
    }

    void OnDisable()
    {
        scaleTween?.Kill();
    }
}
