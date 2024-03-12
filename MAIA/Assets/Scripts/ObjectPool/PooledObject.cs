using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private Stack<PooledObject> _pool;
    private HashSet<PooledObject> _active;

    public void Init(Stack<PooledObject> pool, HashSet<PooledObject> activeObjects) {
        if (this._pool == null) {
            this._pool = pool;
        } else throw new System.Exception("Can't set the pool multiple times on a pooled object!!");
        
        if (this._active == null) {
            this._active = activeObjects;
        } else throw new System.Exception("Can't set the active set multiple times on a pooled object!!");
    }

    public void Release() {
        // with how my code is, pooled objects would just cause more of a headache
        GameObject.Destroy(this.gameObject);

        // this._pool.Push(this);
        // _active.Remove(this);
        // this.gameObject.SetActive(false);
    }
}
