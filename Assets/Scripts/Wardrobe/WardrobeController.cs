using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class WardrobeController : MonoBehaviour
{
    [SerializeField] bool isOpen;
    [SerializeField] GameObject wardrobeOpenObj;
    [SerializeField] GameObject wardrobeClosedObj;
    [SerializeField] ClothItem[] clothes;
    [SerializeField] CinemachineCamera wardrobeCamera;
    [SerializeField] CinemachineBrain cinemachineBrain;

    void Awake()
    {
        CloseWardrobe();
    }

    void Start()
    {
        InvokeRepeating(nameof(CheckIfAllShown), 0, 5);
    }

    void CheckIfAllShown()
    {
        bool flag = true;
        foreach (ClothItem clothItem in clothes)
        {
            flag = flag && clothItem.isShown;
        }
        //print(flag);
        if (flag)
        {
            UIManager.Instance.ActiveNextButton();
        }
    }

    public void CloseWardrobe()
    {
        isOpen = false;
        wardrobeOpenObj.SetActive(false);
        wardrobeClosedObj.SetActive(true);
        wardrobeCamera.gameObject.SetActive(false);
    }
    public void OpenWardrobe()
    {
        StartCoroutine(CameraSwitching());
    }

    private IEnumerator CameraSwitching()
    {
        wardrobeCamera.gameObject.SetActive(true);

        yield return new WaitUntil(() => cinemachineBrain.IsBlending);

        yield return new WaitUntil(() => !cinemachineBrain.IsBlending);

        isOpen = true;
        wardrobeOpenObj.SetActive(true);
        wardrobeClosedObj.SetActive(false);
    }

    public void DeselectAllClothes()
    {
        foreach (ClothItem cloth in clothes)
        {
            cloth.DeselectItem();
        }
    }
}
