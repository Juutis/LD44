using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Text text;
    [SerializeField]
    Shop shop;

    List<string> texts;
    bool active;
    int curText;
    bool writing;

    float waitUntil;

    bool openShop = false;



    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (writing)
            {
                if (Time.time >= waitUntil)
                {
                    waitUntil = Time.time + 0.005f;
                    printNextLetter(1);
                }

                if (skipKeyDown())
                {
                    printNextLetter(10000);
                }
            }
            else
            {
                if (Time.time >= waitUntil || skipKeyDown())
                {
                    nextText();
                }
            }
        }
    }

    public void activate()
    {
        active = true;
        panel.SetActive(true);
        curText = 0;
        writing = true;
        ShopManager.INSTANCE.activate();
        text.text = "";
    }

    public void deactivate()
    {
        active = false;
        panel.SetActive(false);
        ShopManager.INSTANCE.deactivate();
        text.text = "";
        if (openShop)
        {
            shop.activate();
        }
    }

    public void playTexts(List<string> texts, bool openShop = false)
    {
        activate();
        this.texts = texts;
        this.openShop = openShop;
    }

    private void nextText()
    {
        curText++;
        if (curText >= texts.Count)
        {
            deactivate();
        }
        else
        {
            text.text = "";
            writing = true;
        }
        waitUntil = Time.time + 0.1f;
    }

    private void printNextLetter(int amount)
    {
        int curLength = text.text.Length;
        string currentText = texts[curText];
        if (curLength == currentText.Length)
        {
            writing = false;
            waitUntil = Time.time + 30;
            return;
        }
        text.text = currentText.Substring(0, Mathf.Min(currentText.Length, curLength + amount));
    }

    private bool skipKeyDown()
    {
        return Input.GetKeyDown(KeyCode.E)
            || Input.GetKeyDown(KeyCode.Mouse0)
            || Input.GetKeyDown(KeyCode.Mouse1)
            || Input.GetKeyDown(KeyCode.Space);
    }
}
