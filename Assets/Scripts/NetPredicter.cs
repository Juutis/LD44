using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPredicter : MonoBehaviour
{
    float TIME_TO_LIVE = 0.1f;
    float spawnTime;

    private int ENEMY_LAYER;

    public void Init()
    {
        spawnTime = Time.time;
        ENEMY_LAYER = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime + TIME_TO_LIVE < Time.time)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ENEMY_LAYER)
        {
            other.gameObject.GetComponent<Bunny>().Flee(transform.position);
        }
    }
}
