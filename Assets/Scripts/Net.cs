using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : Launchable
{
    [SerializeField]
    Rigidbody rigidBody;

    [SerializeField]
    GameObject model;

    [SerializeField]
    GameObject predicterTemplate;

    float predicterTimer = 0.0f;
    float launchTime;

    int terrainMask = 1 << 11 | 1 << 13;

    float rotation, rotSpeed;


    [SerializeField]
    SphereCollider collider;

    // Update is called once per frame
    void Update()
    {
        if (predicterTimer < Time.time)
        {
            predict(rigidBody.velocity);
            Vector3 forwardDown = rigidBody.velocity;
            forwardDown.y = 0;
            forwardDown = forwardDown.normalized;
            forwardDown.y = -1;
            predict(forwardDown);
            predicterTimer = Time.time + 0.1f;
        }
        model.transform.LookAt(transform.position + rigidBody.velocity);
        rotation += rotSpeed * Time.deltaTime;
        model.transform.Rotate(Vector3.forward, rotation);

        if (Time.time > launchTime + 10)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(Vector3.down * 15, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    override public void Launch(Vector3 direction, float speed)
    {
        float speedUpgr = 1 + ShopManager.INSTANCE.speedLevel * 0.5f;
        rigidBody.AddForce(direction.normalized * speed * speedUpgr, ForceMode.VelocityChange);
        launchTime = Time.time;
        rotSpeed = Random.Range(-200, 200);

        float size = 1 + ShopManager.INSTANCE.sizeLevel * 0.5f;
        gameObject.transform.localScale = transform.localScale * size;
        //collider.radius = collider.radius * size;
    }

    private void predict(Vector3 direction)
    {
        Debug.DrawLine(transform.position, transform.position + 100 * direction.normalized, Color.red, 0.1f);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 1000, terrainMask))
        {
            GameObject predicter = Instantiate(predicterTemplate);
            predicter.transform.position = hit.point;
            predicter.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
            predicter.GetComponent<NetPredicter>().Init();
        }
    }
}
