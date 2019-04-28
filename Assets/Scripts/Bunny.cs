using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    bool fleeing = false;
    Vector3 fleeDirection = Vector3.zero;

    [SerializeField]
    float fleeSpeed = 10f;

    [SerializeField]
    float alertDistance = 15f;

    [SerializeField]
    float walkSpeed = 5f;

    [SerializeField]
    float turnSpeed = 5f;

    [SerializeField]
    Vector3 bounty = new Vector3(10, 0, 0);

    bool caught;
    int health = 1;

    [SerializeField]
    int maxHealth = 10;

    [SerializeField]
    Rigidbody rigidbody;

    [SerializeField]
    GameObject net;

    [SerializeField]
    GameObject model;

    private Animator animator;

    int PROJECTILE_LAYER;
    int PLAYER_LAYER;
    int BULLET_LAYER;

    int jumps = 0;
    float waitUntil = 0.0f;
    float fleeTimer = 0.0f;
    Vector3 targetDirection;

    GameObject player;

    [SerializeField]
    GameObject blood1;

    [SerializeField]
    bool isred, isblack;

    bool active = true;

    float respawn;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        PROJECTILE_LAYER = LayerMask.NameToLayer("Net");
        PLAYER_LAYER = LayerMask.NameToLayer("Player");
        BULLET_LAYER = LayerMask.NameToLayer("Bullet");
        health = maxHealth;
        animator = model.GetComponent<Animator>();
        targetDirection = gameObject.transform.forward;
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active && respawn < Time.time)
        {
            Reset();
        }

        Vector3 thisToPlayer = player.transform.position - transform.position;
        if (alertDistance * (360 - Vector3.Angle(transform.forward, thisToPlayer))/360 > thisToPlayer.magnitude)
        {
            if (fleeTimer < Time.time)
            {
                Flee(player.transform.position);
            }
        }

        if (fleeing)
        {
            targetDirection = fleeDirection;
        }
        else
        {
            if (jumps > 0)
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("jump", false);
                if (waitUntil < Time.time)
                {
                    jumps = Random.Range(1, 10);
                    targetDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
                    if (targetDirection == Vector3.zero)
                    {
                        targetDirection = Vector3.forward;
                    }
                }
            }
        }

        if (!caught)
        {
            if (Vector3.Angle(transform.forward, targetDirection) < 0.1)
            {
                transform.LookAt(transform.position + targetDirection);
            }
            else
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
                transform.LookAt(transform.position + newDir);
            }
        }

        if (active && transform.position.y < -90)
        {
            Kill();
        }

    }

    void FixedUpdate()
    {
        float verticalVelocity = rigidbody.velocity.y;
        if (!caught)
        {
            if (fleeDirection != Vector3.zero)
            {
                rigidbody.velocity = fleeDirection.normalized * fleeSpeed * health / maxHealth;
            }
            else
            {
                if (jumps > 0)
                {
                    rigidbody.velocity = transform.forward.normalized * walkSpeed;
                }
                else
                {
                    rigidbody.velocity = Vector3.zero;
                }
            }
        }
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, verticalVelocity, rigidbody.velocity.z);
    }

    public void Flee(Vector3 point)
    {
        fleeDirection = transform.position - point;
        fleeDirection.y = 0;
        animator.SetBool("jump", true);
        fleeing = true;
        fleeTimer = Time.time + 0.5f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == PLAYER_LAYER)
        {
            if (caught)
            {
                ShopManager.INSTANCE.coins += health / maxHealth * bounty;
                if (isred && health == maxHealth)
                {
                    ShopManager.INSTANCE.redsUnhurt++;
                }
                if (isblack && health == maxHealth)
                {
                    ShopManager.INSTANCE.blacksUnhurt++;
                }
                Kill();
            }
        }
        else if (collision.gameObject.layer == PROJECTILE_LAYER)
        {
            caught = true;
            net.SetActive(true);
            animator.SetBool("caught", true);
        }
        else if (collision.gameObject.layer == BULLET_LAYER)
        {
            health -= collision.gameObject.GetComponent<Bullet>().damage;
            var blood = Instantiate(blood1);
            blood.transform.position = collision.transform.position;
            if (health < 0)
            {
                health = 0;
                Kill();
            }
            Flee(player.transform.position);
        }
    }

    public void JumpFinished()
    {
        jumps--;
        if (jumps <= 0)
        {
            waitUntil = Time.time + Random.Range(2.0f, 10.0f);
        }
    }

    private void Kill()
    {
        transform.position = new Vector3(0, -1000, 0);
        active = false;
        respawn = Time.time + 3.0f;
        animator.SetBool("reset", true);
        animator.SetBool("caught", false);
    }

    private void Reset()
    {
        transform.position = startPosition;
        active = true;
        caught = false;
        fleeing = false;
        health = maxHealth;
        animator.SetBool("reset", false);
        net.SetActive(false);
        animator.SetBool("jump", false);
        jumps = 0;
        fleeDirection = Vector3.zero;
    }

}
