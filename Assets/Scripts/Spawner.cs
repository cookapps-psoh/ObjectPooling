using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxPoolSize;
    private ObjectPool<GameObject> _pool;

    public int CountAll => _pool.CountAll;
    public int CountActive => _pool.CountActive;
    public int CountInactive => _pool.CountInactive;
    public int MaxPoolSize => maxPoolSize;
    
    void Start()
    {
        _pool = new ObjectPool<GameObject>(OnCreatePoolObject,
            OnGetPoolObject,
            OnReleasePoolObject,
            OnDestroyPoolObject, 
            false, 
            maxPoolSize, 
            maxPoolSize);
    }

    GameObject OnCreatePoolObject()
    {
        var obj = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.SetParent(this.transform);
        return obj;
    }

    private void OnGetPoolObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReleasePoolObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject Spawn()
    {
        Debug.Log(_pool);
        if (_pool.CountActive == maxPoolSize)
        {
            Debug.LogWarning("Max pool size reached! spawn() will be ignored.");
            return null;
        }
        
        GameObject obj = _pool.Get();
        return obj;
    }

    public void Despawn(GameObject obj)
    {
        _pool.Release(obj);
    }
}
