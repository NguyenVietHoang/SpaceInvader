using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnInvaderCollide;

    [Header ("Params")]
    [SerializeField]
    private Transform BulletSpawnPos;    

    [Header("Player Infos")]
    [SerializeField]
    private float speed = 2f;

    // Start is called before the first frame update
    public void Initialize()
    {
        
    }

    /// <summary>
    /// Move the player to the left/right base on the player Speed.
    /// </summary>
    /// <param name="_left"></param>
    public void Move(bool _left)
    {
        if(_left)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    public Vector3 GetBulletSpawnPos()
    {
        return BulletSpawnPos.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Invader"))
        {
            //Debug.Log("[PlayerControler] Collided with Invader");
            OnInvaderCollide?.Invoke();
        }
    }
}
