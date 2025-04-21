using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpriteRenderer hiddenObject;

    private List<List<Obstacle>> _layers = new();
    private int _currentLayerIndex = 0;

    Vector3[] binPositions = { new(-7.8f, -3.7f, 0), new(-7.8f, 3.7f, 0), new(7.8f, -3.7f, 0), new(7.8f, 3.7f, 0) };

    void Awake()
    {
        Instance = this;
        hiddenObject.sortingOrder = -1;
    }

    void Start()
    {
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

        obstacle.transform.DOScale(obstacle.transform.localScale * 1.1f, 0.5f).OnComplete(() =>
        {
            obstacle.transform.DOMove(binPositions[UnityEngine.Random.Range(0, binPositions.Length)], 0.5f);
            obstacle.transform.DOScale(obstacle.transform.localScale * 0f, 0.5f).OnComplete(() =>
            {
                //Destroy(obstacle.gameObject);
                //obstacle.gameObject.SetActive(false);
                obstacle._sr.enabled = false;
                obstacle._collider.enabled = false;
            });
        });

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

