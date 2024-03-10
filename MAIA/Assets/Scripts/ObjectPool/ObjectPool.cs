using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pool", menuName = "Scriptable Objects/Object Pool", order = 0)]
public class ObjectPool: ScriptableObject
{
    private Stack<GameObject> _pool = new Stack<GameObject>(); 
    public int pooledCount => this._pool.Count;
    [SerializeField] private GameObject _prefab; 
    
    /// <summary>
    /// Destroys all currently pooled objects. DOES NOT destroy objects outside the pool.
    /// </summary>
    public void DestroyPooled() {
        while (this.pooledCount != 0) {
            GameObject gameObject = this._pool.Pop();
            GameObject.Destroy(gameObject);
        }
    }

    public GameObject Get() {
        if (this.pooledCount == 0)
            return this.Create();
        return this._pool.Pop();
    }

    private GameObject Create() {
        GameObject gameObject = GameObject.Instantiate(_prefab);
        GameObject.DontDestroyOnLoad(gameObject);

        // This ensures the integrety of the pool.
        // The object is passed a reference to the stack (aka pool) upon initialization, this Create() method.
        // Using its pool reference, it releases itself into its pool reference, which *is* be the pool that set it.
        gameObject.AddComponent<PooledObject>().Init(this._pool);

        return gameObject;
    }
    
    // Releasing is handled by the PooledObject component
}
