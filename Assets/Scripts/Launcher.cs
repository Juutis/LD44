using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    GameObject launchableTemplate;

    [SerializeField]
    int objectsToLaunch = 1;

    [SerializeField]
    float speed;

    [SerializeField]
    float spread = 0;

    [SerializeField]
    bool useLeft = false;

    [SerializeField]
    GameObject fpsModel;

    Animator fpsAnimator;

    Camera camera;

    float delay = 0.5f;
    float lastShot = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        fpsAnimator = fpsModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShopManager.INSTANCE.dialogActive)
        {
            if(useLeft && Input.GetKeyDown(KeyCode.Mouse0) || !useLeft && Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (Time.time > lastShot + delay)
                {
                    for (int i = 0; i < objectsToLaunch; i++)
                    {
                        GameObject launchable = Instantiate(launchableTemplate);
                        launchable.transform.position = transform.position;
                        Vector3 direction = camera.transform.forward.normalized * 10;
                        direction += new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
                        launchable.GetComponent<Launchable>().Launch(direction, speed);
                        fpsAnimator.SetTrigger("throw");
                        lastShot = Time.time;
                    }
                }
            }

        }
    }
}
