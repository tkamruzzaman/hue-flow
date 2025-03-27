using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpriteRenderer hiddenObject;

    private List<List<Obstacle>> _layers = new();
    private int _currentLayerIndex = 0;

    void Awake()
    {
        Instance = this;
        hiddenObject.sortingOrder = -1; 
    }

    void Start(){
        InitializeLayers();

    }

    void InitializeLayers()
    {
        var groups = FindObjectsOfType<Obstacle>()
            .GroupBy(o => o.layerGroup)
            .OrderByDescending(g => g.Key);

        foreach (var group in groups)
        {
            _layers.Add(group.ToList());
        }

        // activate top layer
        SetLayerInteraction(_currentLayerIndex, true);
    }

    public void RemoveObstacle(Obstacle obstacle)
    {
        _layers[_currentLayerIndex].Remove(obstacle);
        Destroy(obstacle.gameObject);

        //  if layer cleared
        if (_layers[_currentLayerIndex].Count == 0)
        {
            SetLayerInteraction(_currentLayerIndex, false);
            _currentLayerIndex++;

            if (_currentLayerIndex < _layers.Count)
            {
                SetLayerInteraction(_currentLayerIndex, true);
            }
            else
            {
                // all layers cleared
                hiddenObject.sortingOrder = 0; // Make visible
            }
        }
    }

    void SetLayerInteraction(int layerIndex, bool enabled)
    {
        foreach (var obstacle in _layers[layerIndex])
        {
            obstacle.EnableInteraction(enabled);
        }
    }

    
}

