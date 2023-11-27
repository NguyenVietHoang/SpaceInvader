using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPooling : MonoBehaviour
{
    [SerializeField]
    private ObjectPooling BulletPool;
    // Start is called before the first frame update
    void Start()
    {
        BulletPool.Initialize();
    }

    #region Testing
    List<PoolingElt> testElt = new List<PoolingElt>();
    public void GetBullet()
    {
        PoolingElt tmp = BulletPool.GetPooledObject();       

        if (tmp == null)
        {
            Debug.Log("Empty Pool...");
        }
        else
        {
            tmp.OnObjActive += () =>
            {
                int id = testElt.Count;
                Debug.Log("Object Active at: " + id);
            };

            tmp.SetActive(true);
            testElt.Add(tmp);
        }
    }

    public void DestroyBullet()
    {
        if (testElt.Count > 0)
        {
            PoolingElt tmp = testElt[0];
            testElt.Remove(tmp);
            tmp.SetActive(false);
        }
        else
        {
            Debug.Log("No bullet to Destroy...");
        }
    }
    #endregion
}
