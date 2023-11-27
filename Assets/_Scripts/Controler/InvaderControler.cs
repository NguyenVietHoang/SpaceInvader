using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderControler : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnInvaderEscape;   

    [Header ("Manager OBJ")]
    [SerializeField]
    private MoveControler moveControler;
    [SerializeField]
    private PoolingElt poolingElt;
    [SerializeField]
    private Animator anim;

    InvaderModel currentInvaderData;
    private float invaderSpd;
    private float invaderAcc;
    /// <summary>
    /// Initialize the invader controler. The invader will start moving 
    /// along the path automatically
    /// </summary>
    /// <param name="path">
    /// Position of the invader when
    /// </param>
    public void Initialize(PathModel path, InvaderModel invaderData)
    {
        if(path != null)
        {
            //Debug.Log("[InvaderControler][Initialize] Init the Invader...");
            moveControler.OnStartMoving = null;
            //moveControler.OnStartMoving += () => ChangeSkin(currentInvaderID);
            moveControler.OnEndMoving = null;
            moveControler.OnEndMoving += ToNextNode;

            
            currentNode = path.GetPathRoot();
            moveControler.Teleport(currentNode.PathPos);

            ChangeSkin(invaderData);

            ToNextNode();
        }
    }

    /// <summary>
    /// Change the skin of the Invader. 
    /// This function will also change the Invader Parameter.
    /// </summary>
    /// <param name="invaderID"></param>
    protected virtual void ChangeSkin(InvaderModel invaderData)
    {
        //Debug.Log("[InvaderControler][ChangeSkin] To Skin " + invaderID);
        currentInvaderData = invaderData;
        invaderSpd = invaderData.InvaderSpd;
        invaderAcc = invaderData.InvaderAcc;

        poolingElt.SetActive(true);
        anim.SetInteger("InvaderId", invaderData.InvaderID);
        anim.SetTrigger("InvaderSkin");      
    }

    PathElt currentNode = null;
    /// <summary>
    /// Move to next node with increasing speed.
    /// </summary>
    protected virtual void ToNextNode()
    {        
        if (currentNode != null && currentNode.Next != null)
        {
            currentNode = currentNode.Next;
            TriggerAbility();

            moveControler.SetTarget(currentNode.PathPos.position, invaderSpd);
            moveControler.StartMoving();
        }
        else
        {
            Debug.Log("Invader reach the end of his road...");
            OnInvaderEscape?.Invoke();
            SetDeath();
        }
    }

    /// <summary>
    /// Basic ability of the invader: acceleration.
    /// </summary>
    protected virtual void TriggerAbility()
    {
        invaderSpd += invaderSpd * invaderAcc;
    }

    /// <summary>
    /// Return the invader to the pool.
    /// </summary>
    public void SetDeath()
    {
        poolingElt.SetActive(false);
    }

    public int GetScore()
    {
        return currentInvaderData.Score;
    }
}
