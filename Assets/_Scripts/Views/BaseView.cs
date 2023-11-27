using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    [SerializeField]
    private GameObject viewRoot;

    public virtual void SetActive(bool _state)
    {
        viewRoot.SetActive(_state);
    }
}
