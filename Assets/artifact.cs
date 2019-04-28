using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artifact : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject interactPrompt;

    [SerializeField]
    Dialog dialog;

    Camera camera;

    int layerMask = 1 << 19;

    bool prompt;

    [SerializeField]
    GameObject blue, red, yellow, green, endScreen;

    int gems = 0;

    bool finished = false;
    float finish = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (prompt && !ShopManager.INSTANCE.dialogActive
            && (ShopManager.INSTANCE.blueGem || ShopManager.INSTANCE.redGem || ShopManager.INSTANCE.yellowGem || ShopManager.INSTANCE.greenGem))
        {
            interactPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ShopManager.INSTANCE.blueGem)
                {
                    ShopManager.INSTANCE.blueGem = false;
                    blue.SetActive(true);
                    gems++;
                }
                else if (ShopManager.INSTANCE.redGem)
                {
                    ShopManager.INSTANCE.redGem = false;
                    red.SetActive(true);
                    gems++;
                }
                else if (ShopManager.INSTANCE.yellowGem)
                {
                    ShopManager.INSTANCE.yellowGem = false;
                    yellow.SetActive(true);
                    gems++;
                }
                else if (ShopManager.INSTANCE.greenGem)
                {
                    ShopManager.INSTANCE.greenGem = false;
                    green.SetActive(true);
                    gems++;
                }

                if (gems == 4)
                {
                    finish = Time.time + 2.0f ;
                    finished = true;
                }

                List<string> texts = new List<string>();
                texts.Add("test 123");
                texts.Add("test 123 sdfsdf dfs sdfas dfasdf sadfsad fasdfas dfsa dfas dfsa dfas fdsa fsadf asfas");
                texts.Add("- rpess srenay key to kcontineua -");
                dialog.playTexts(texts, true);
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }

        if (finished && finish < Time.time)
        {
            endScreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, 3, layerMask))
        {
            prompt = true;
        }
        else
        {
            prompt = false;
        }
    }
}
