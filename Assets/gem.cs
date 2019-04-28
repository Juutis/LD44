using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    [SerializeField]
    bool green = false;

    int PLAYER_LAYER;
    // Start is called before the first frame update
    void Start()
    {
        PLAYER_LAYER = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == PLAYER_LAYER)
        {
            if (green)
            {
                ShopManager.INSTANCE.greenGem = true;
                Destroy(gameObject);
            }
            else
            {
                ShopManager.INSTANCE.yellowGem = true;
                Destroy(gameObject);
            }
        }
    }
}
