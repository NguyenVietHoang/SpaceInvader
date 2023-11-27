using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingElt : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnObjActive;

    private ObjectPooling poolingMaster;

    /// <summary>
    /// Initialize the elt, and set the pool for tracking.
    /// </summary>
    /// <param name="pool"></param>
    public void Init(ObjectPooling pool)
    {
        poolingMaster = pool;
        SetActive(false);
    }

    /// <summary>
    /// Use this to replace the Destroy function.
    /// </summary>
    /// <param name="_state">
    /// false: Deactivate the object and reset the event OnObjActive.
    /// true: Activate the object and trigger the event OnObjActive.
    /// </param>
    public void SetActive(bool _state)
    {
        if(!_state)
        {
            transform.SetParent(poolingMaster.transform);
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            OnObjActive = null;
        }
        else
        {
            OnObjActive?.Invoke();
        }
        gameObject.SetActive(_state);
    }
}
