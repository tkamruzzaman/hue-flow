using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImpactButton : MonoBehaviour
{
    [SerializeField] int impactNo;
    [SerializeField] Button button;
    [SerializeField] Image maskImage;

    [Space]
    Vector3 initialScale;
    [SerializeField] float scaleFactor = 1.1f;
    [SerializeField] float scaleDuration = 1.0f;
    Tween scaleTween;

    void Awake()
    {
        initialScale = transform.localScale;

        DeactiveButton();

        button.onClick.AddListener(() =>
        {
            Impact.Instance.ShowImpact(impactNo);
        });
    }

    public void DeactiveButton()
    {
        maskImage.gameObject.SetActive(false);
        button.interactable = false;
    }

    public void ActiveButton()
    {
        scaleTween = transform.DOScale(initialScale * scaleFactor, scaleDuration).SetLoops(-1, LoopType.Yoyo);
        maskImage.gameObject.SetActive(true);
        button.interactable = true;
    }

    void OnDisable()
    {
        scaleTween?.Kill();
    }

}
