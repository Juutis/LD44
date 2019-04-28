using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Launchable
{
    [SerializeField]
    Rigidbody rigidBody;

    [SerializeField]
    public int damage = 1;

    float launchTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > launchTime + 10)
        {
            Destroy(gameObject);
        }
    }

    override public void Launch(Vector3 direction, float speed)
    {
        rigidBody.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
        launchTime = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
