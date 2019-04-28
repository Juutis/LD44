using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    public Vector3 coins;

    public static ShopManager INSTANCE;

    public bool dialogActive = false;

    public int speedLevel = 2;
    public int sizeLevel = 1;

    public bool torch;
    public bool boots;

    [SerializeField]
    public bool redGem, greenGem, yellowGem, blueGem;

    [SerializeField]
    Text brown, red, black;

    public int redsUnhurt, blacksUnhurt;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void activate()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        dialogActive = true;
    }

    public void deactivate()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialogActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        brown.text = coins.x.ToString();
        red.text = coins.y.ToString();
        black.text = coins.z.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && !dialogActive)
        {
            Application.Quit();
        }
    }
    
}
