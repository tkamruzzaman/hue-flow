using UnityEngine;

public class Wardrobe : MonoBehaviour
{
     WardrobeController wardrobeController;

    void Awake()
    {
        wardrobeController = GetComponentInParent<WardrobeController>();
    }

    void OnMouseDown()
    {
        OpenWardrobe();
    }

    void OpenWardrobe()
    {
        wardrobeController.OpenWardrobe();
    }
}
