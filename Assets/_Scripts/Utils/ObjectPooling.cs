using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private List<PoolingElt> pooledObjects;
    public PoolingElt objectToPool;
    public int amountToPool;

    /// <summary>
    /// Initialize the pooling
    /// </summary>
    public void Initialize()
    {
        Debug.Log("[ObjectPooling][Initialize] Init the pool...");
        if (pooledObjects != null && pooledObjects.Count > 0)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                Destroy(pooledObjects[i]);
            }
        }

        pooledObjects = new List<PoolingElt>();
        for (int i = 0; i < amountToPool; i++)
        {
            PoolingElt elt = Instantiate(objectToPool);
            elt.Init(this);
            pooledObjects.Add(elt);
        }
    }

    /// <summary>
    /// Get the pool element
    /// </summary>
    /// <returns></returns>
    public PoolingElt GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
