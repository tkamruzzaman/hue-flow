using UnityEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int layerGroup = 1;
    
    [SerializeField]private SpriteRenderer _sr;
   [SerializeField] private Collider2D _collider;

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
        print("Clickeddddd");
        GameManager.Instance.RemoveObstacle(this);
    }

    void OnDrawGizmos()
    {
        if (_sr != null)
        {
            GUI.color = Color.red;
            Handles.Label(transform.position, $"Layer: {layerGroup}\nOrder: {_sr.sortingOrder}");
        }
    }
}