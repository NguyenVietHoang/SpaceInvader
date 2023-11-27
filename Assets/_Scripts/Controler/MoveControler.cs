using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControler : MonoBehaviour
{
    public delegate void OnEventCalled();
    public OnEventCalled OnStartMoving;
    public OnEventCalled OnEndMoving;

    [Header("Object Setting")]
    [Tooltip("The Object to Move by this Script")]
    [SerializeField] private GameObject ObjectToMove;
    private Vector3 startPos;
    private Vector3 target;

    [SerializeField]
    private float Speed;
    private bool inTarget = true;
    private bool isMoving = false;

    private Vector3 dir;
    private void Update()
    {
        if (isMoving)
        {
            Vector3 curPos = ObjectToMove.transform.position;
            dir = (target - curPos).normalized;
            Vector3 newPos = curPos + dir * (Time.deltaTime * Speed);
            ObjectToMove.transform.position = newPos;
        }

        if (!inTarget && Vector3.Distance(ObjectToMove.transform.position, target) < 0.01f)
        {
            TargetReaching();
        }
    }

    #region Initialize
    public void SetObjectToMove(GameObject _newObjToMove)
    {
        ObjectToMove = _newObjToMove;
    }

    /// <summary>
    /// Initialize the Module
    /// </summary>
    /// <param name="_target">The target to move forward to</param>
    /// <param name="_speed">The time that the Object take to reach the target </param>
    public void SetTarget(Vector3 _target, float _speed)
    {
        startPos = ObjectToMove.transform.position;
        Speed = _speed;
        target = _target;
        inTarget = false;

        //Debug.Log("[MoveControler][SetTarget] " + startPos + " - " + target);
    }
    #endregion

    #region Moving Event
    /// <summary>
    /// Trigger the Move event to move the Object
    /// </summary>
    public void StartMoving()
    {
        isMoving = true;
        OnStartMoving?.Invoke();
        //Debug.Log("Start Moving:" + Speed);
    }

    /// <summary>
    /// Stop the movement and trigger the event OnEndMoving
    /// </summary>
    public void TargetReaching()
    {
        inTarget = true;
        isMoving = false;

        OnEndMoving?.Invoke();
    }

    /// <summary>
    /// Move the object immediatly to the target
    /// </summary>
    /// <param name="target"></param>
    public void Teleport(Transform target)
    {
        //Debug.Log("Teleport to " + target.position);
        ObjectToMove.transform.position = target.position;
        //ObjectToMove.transform.rotation = target.rotation;
    }
    #endregion
}
