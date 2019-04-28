using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldrat : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject interactPrompt;

    [SerializeField]
    Dialog dialog;

    Camera camera;

    int layerMask = 1 << 16;

    bool prompt;

    bool introtext = true, bluesBrought = false, redsBought = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (prompt && !ShopManager.INSTANCE.dialogActive)
        {
            interactPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (introtext)
                {
                    List<string> texts = new List<string>();
                    texts.Add("Hello there traveler!");
                    texts.Add("I believe this is what you are looking for. The ancient artifact of Mount Weasel!");
                    texts.Add("Listen, I can help you unravel the mysteries it holds... For a price.");
                    texts.Add("I have a problem, you see. This mountain is not a suitable place for us weasels. Not at all. " +
                        "But my brethren... they're not that smart and refuse to listen to me. So I need you to catch them for me... ALIVE.");
                    texts.Add("I see you already have suitable equipment for that. You can use your net to trap my fellow weasels. And if they're " +
                        "too slippery for you, you can also try to slow them down with that mighty blunderbuss. A wounded weasel is a slow weasel. Don't " +
                        "overdo it, though. We don't want to slaughter the poor creatures.");
                    texts.Add("You get weasel tokens when you catch them. The healthier they are, the more they are worth. Different coloured weasels grant " +
                        "different tokens. If you bring me enough weasel tokens, I might be able to give you something to aid you on your journey.");
                    texts.Add("Now, let me show you my stuff.");
                    dialog.playTexts(texts, true);
                    introtext = false;
                }
                else
                {
                    List<string> texts = new List<string>();
                    texts.Add("Hello there!");
                    if (ShopManager.INSTANCE.redsUnhurt >= 3)
                    {
                        if (!redsBought)
                        {
                            texts.Add("Marvellous! Three unscathed red weasels! Here is your reward.");
                            texts.Add("- The old weasel gives you a red gem -");
                            ShopManager.INSTANCE.redGem = true;
                            redsBought = true;
                        }
                    }
                    else if (!redsBought)
                    {
                        texts.Add("If you bring me three red weasels completely unharmed I will reward you with a special prize.");
                    }

                    if (ShopManager.INSTANCE.blacksUnhurt >= 3)
                    {
                        if (!bluesBrought)
                        {
                            texts.Add("Marvellous! Three blue weasels without a scratch! Here is your reward.");
                            texts.Add("- The old weasel gives you a blue gem -");
                            ShopManager.INSTANCE.blueGem = true;
                            bluesBrought = true;
                        }
                    }
                    else if (!bluesBrought)
                    {
                        texts.Add("If you bring me three blue weasels completely unharmed I will reward you with a special prize.");
                    }

                    dialog.playTexts(texts, true);
                }
            }
        }
        else
        {
            interactPrompt.SetActive(false);
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
