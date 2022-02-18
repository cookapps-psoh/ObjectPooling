using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    
    [Header("Boundary")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    [SerializeField] private TMP_Text logText;
 
    private List<GameObject> _spawnedGameObjects;

    private void Start()
    {
        _spawnedGameObjects = new List<GameObject>();
    }

    private void UpdateStatus()
    {
        var status = $"Active = {spawner.CountActive}, Inactive = {spawner.CountInactive}, All = {spawner.CountAll}";
        logText.text = status;
    }
    
    public void OnClickSpawnButton()
    {
        var obj = spawner.Spawn();
        if (!obj)
        {
            Debug.LogWarning("No gameobject returned from pool");
            return;
        }
        
        _spawnedGameObjects.Add(obj);

        var x = Random.Range(xMin, xMax);
        var y = Random.Range(yMin, yMax);

        obj.transform.position = new Vector3(x, y);
        UpdateStatus();
    }

    public void OnClickDespawnButton()
    {
        if (_spawnedGameObjects.Count == 0)
        {
            Debug.LogWarning("No spawned gameobject!");
            return;
        }

        spawner.Despawn(_spawnedGameObjects.Last());
        _spawnedGameObjects.RemoveAt(_spawnedGameObjects.Count - 1);
        UpdateStatus();
    }
}