using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

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

    void Start()
    {
        _spawnedGameObjects = new List<GameObject>();
    }

    void UpdateStatus()
    {
        string status = $"Active = {spawner.CountActive}, Inactive = {spawner.CountInactive}, All = {spawner.CountAll}";
        logText.text = status;
    }
    
    public void OnClickSpawnButton()
    {
        GameObject obj = spawner.Spawn();
        if (!obj)
        {
            Debug.LogWarning("No gameobject returned from pool");
            return;
        }
        
        _spawnedGameObjects.Add(obj);

        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        float z = 0;

        obj.transform.position = new Vector3(x, y, z);
        UpdateStatus();
    }

    public void OnClickDespwanButton()
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