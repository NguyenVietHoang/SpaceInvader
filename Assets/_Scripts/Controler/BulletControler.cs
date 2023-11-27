using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public delegate void OnEventTrigger<T>(T data);
    public delegate void OnEventTrigger<T1,T2>(T1 data1, T2 data2);
    public OnEventTrigger<BulletControler> OnMapBoundCollide;
    public OnEventTrigger<BulletControler, InvaderControler> OnInvaderCollide;

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float deathDelay = 0.5f;

    [Header("Manager OBJ")]
    [SerializeField]
    private PoolingElt poolingElt;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody2D rb;

    int onMove;
    float onDeathdelay;

    /// <summary>
    /// Teleport the bullet to the Spawn Pos, then let it auto move toward.
    /// </summary>
    /// <param name="spawnPos"></param>
    public void Initialize(Vector3 spawnPos)
    {
        onMove = 0;
        onDeathdelay = deathDelay;

        poolingElt.SetActive(true);
        transform.position = spawnPos;
        anim.SetTrigger("Bullet");

        rb.simulated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(onMove == 0)
        {
            AutoMove();
        }
        else if (onMove == 1)
        {
            onDeathdelay -= Time.deltaTime;
            if(onDeathdelay <= 0)
            {
                onMove = 2;
            }
        }
        else
        {
            SetDeath();
        }
    }

    protected virtual void AutoMove()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    /// <summary>
    /// Trigger the explosion fx before death.
    /// </summary>
    public virtual void TriggerDeathProcess()
    {
        onMove = 1;
        anim.SetTrigger("Death");

        rb.simulated = false;
    }

    protected virtual void SetDeath()
    {
        poolingElt.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Invader"))
        {
            //Debug.Log("[BulletControler] Collided with Invader");
            InvaderControler invaderControler = other.GetComponent<InvaderControler>();
            OnInvaderCollide?.Invoke(this, invaderControler);
        }
        else if (other.CompareTag("Bound"))
        {
            //Debug.Log("[BulletControler] Collided with Bound");
            OnMapBoundCollide?.Invoke(this);
        }
    }
}
