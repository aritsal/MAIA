using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pool", menuName = "Scriptable Objects/Object Pool", order = 0)]
public class ObjectPool: ScriptableObject
{
    private Stack<PooledObject> _pool = new Stack<PooledObject>(); 
    private HashSet<PooledObject> _activeObjects = new HashSet<PooledObject>();
    public int pooledCount => this._pool.Count;
    public int activeCount => this._activeObjects.Count;
    public int totalCount => this._pool.Count + this._activeObjects.Count;


    [SerializeField] private GameObject _prefab; 

    /// <summary>
    /// Retrieves a pooled object, or creates one if the pool is empty
    /// </summary>
    /// <returns></returns>
    public GameObject Get() {
        PooledObject pooledObject;
        if (this.pooledCount == 0)
            pooledObject = this.Create();
        else 
            pooledObject = this._pool.Pop();

        this._activeObjects.Add(pooledObject);
        pooledObject.gameObject.SetActive(true);
        return pooledObject.gameObject;
    }

    protected PooledObject Create() {
        GameObject gameObject = GameObject.Instantiate(_prefab);
        GameObject.DontDestroyOnLoad(gameObject);

        PooledObject pooledObject = gameObject.AddComponent<PooledObject>();

        // This ensures the integrety of the pool.
        // The object is passed a reference to the stack (aka pool) in this Create() method.
        // Using its pool reference, it releases itself into its pool reference, which *is* be the pool that created it.
        // It also ensures you can't pollute the pool with random objects.        
        pooledObject.Init(this._pool, this._activeObjects);

        return pooledObject;
    }
    
    // Releasing is handled by the PooledObject component
}
