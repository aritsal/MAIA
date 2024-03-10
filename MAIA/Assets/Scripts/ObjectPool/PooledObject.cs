using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private Stack<GameObject> _pool;

    public void Init(Stack<GameObject> pool) {
        if (this._pool == null) {
            this._pool = pool;
            return;
        }
        throw new System.Exception("Can't set pool multiple times on a pooled object!!");
    }

    public void Release() {
        this._pool.Push(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
