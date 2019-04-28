using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Shop : MonoBehaviour
{
    [SerializeField]
    Text brownCoins, redCoins, blackCoins;

    [SerializeField]
    GameObject shop;
    
    [SerializeField]
    Button[] speeds;
    [SerializeField]
    GameObject[] speedTicks;

    [SerializeField]
    Button[] sizes;
    [SerializeField]
    GameObject[] sizeTicks;

    [SerializeField]
    Text[] brownCoinsSpeed, redCoinsSpeed, blackCoinsSpeed;
    [SerializeField]
    Text[] brownCoinsSize, redCoinsSize, blackCoinsSize;

    [SerializeField]
    Text brownCoinsBoots, redCoinsBoots, blackCoinsBoots;
    [SerializeField]
    Text brownCoinsTorch, redCoinsTorch, blackCoinsTorch;
    [SerializeField]
    Button boots, torch;
    [SerializeField]
    GameObject bootsTick, torchTick;

    [SerializeField]
    GameObject caves;

    [SerializeField]
    GameObject torchObj;

    [SerializeField]
    FirstPersonController playerController;

    private class requiredCoins
    {
        public static Vector3[] SPEED = {
            new Vector3(10, 0, 0),
            new Vector3(30, 0, 0),
            new Vector3(50, 10, 0),
            new Vector3(70, 20, 5)
        };

        public static Vector3[] SIZE = {
            new Vector3(10, 0, 0),
            new Vector3(30, 0, 0),
            new Vector3(50, 10, 0),
            new Vector3(70, 20, 5)
        };

        public static Vector3 BOOTS = new Vector3(50, 50, 10);
        public static Vector3 TORCH = new Vector3(50, 10, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in sizeTicks)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in speedTicks)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void activate()
    {
        shop.SetActive(true);
        ShopManager.INSTANCE.activate();
        updateUpgrades();
    }

    public void deactivate() {
        shop.SetActive(false);
        ShopManager.INSTANCE.deactivate();
    }

    public void exit()
    {
        deactivate();
    }

    public void buySpeed(int level)
    {
        if (requiredCoins.SPEED[level].x > ShopManager.INSTANCE.coins.x
            || requiredCoins.SPEED[level].y > ShopManager.INSTANCE.coins.y
            || requiredCoins.SPEED[level].z > ShopManager.INSTANCE.coins.z)
        {
            return;
        }
        if (ShopManager.INSTANCE.speedLevel == level)
        {
            ShopManager.INSTANCE.speedLevel = level + 1;
            ShopManager.INSTANCE.coins -= requiredCoins.SPEED[level];
        }
        updateUpgrades();
    }

    public void buySize(int level)
    {
        if (requiredCoins.SIZE[level].x > ShopManager.INSTANCE.coins.x
            || requiredCoins.SIZE[level].y > ShopManager.INSTANCE.coins.y
            || requiredCoins.SIZE[level].z > ShopManager.INSTANCE.coins.z)
        {
            return;
        }
        if (ShopManager.INSTANCE.sizeLevel == level)
        {
            ShopManager.INSTANCE.sizeLevel = level + 1;
            ShopManager.INSTANCE.coins -= requiredCoins.SIZE[level];
        }
        updateUpgrades();
    }

    public void buyTorch()
    {
        if (ShopManager.INSTANCE.torch)
        {
            return;
        }
        if (requiredCoins.TORCH.x > ShopManager.INSTANCE.coins.x
            || requiredCoins.TORCH.y > ShopManager.INSTANCE.coins.y
            || requiredCoins.TORCH.z > ShopManager.INSTANCE.coins.z)
        {
            return;
        }
        ShopManager.INSTANCE.torch = true;
        ShopManager.INSTANCE.coins -= requiredCoins.TORCH;
        caves.SetActive(false);
        torchObj.SetActive(true);
        updateUpgrades();
    }

    public void buyBoots()
    {
        if (ShopManager.INSTANCE.boots)
        {
            return;
        }
        if (requiredCoins.BOOTS.x > ShopManager.INSTANCE.coins.x
            || requiredCoins.BOOTS.y > ShopManager.INSTANCE.coins.y
            || requiredCoins.BOOTS.z > ShopManager.INSTANCE.coins.z)
        {
            return;
        }
        ShopManager.INSTANCE.boots = true;
        ShopManager.INSTANCE.coins -= requiredCoins.BOOTS;
        playerController.m_GravityMultiplier = 0.5f;
        updateUpgrades();
    }

    private void updateUpgrades()
    {
        int size = ShopManager.INSTANCE.sizeLevel - 1;
        int speed = ShopManager.INSTANCE.speedLevel - 1;
        for (int i = 0; i <= 3; i++)
        {
            sizes[i].interactable = false;
            if (size >= i)
            {
                sizeTicks[i].SetActive(true);
            }
        }
        for (int i = 0; i <= 3; i++)
        {
            speeds[i].interactable = false;
            if (speed >= i)
            {
                speedTicks[i].SetActive(true);
            }
        }

        if (size < 3)
        {
            sizes[size + 1].interactable = true;
        }
        if (speed < 3)
        {
            speeds[speed + 1].interactable = true;
        }

        if (ShopManager.INSTANCE.boots)
        {
            boots.interactable = false;
            bootsTick.SetActive(true);
        }
        else
        {
            boots.interactable = true;
            bootsTick.SetActive(false);
        }

        if (ShopManager.INSTANCE.torch)
        {
            torch.interactable = false;
            torchTick.SetActive(true);
        }
        else
        {
            torch.interactable = true;
            torchTick.SetActive(false);
        }

        for (int i = 0; i <= 3; i++)
        {
            brownCoinsSpeed[i].text = requiredCoins.SPEED[i].x.ToString();
            redCoinsSpeed[i].text = requiredCoins.SPEED[i].y.ToString();
            blackCoinsSpeed[i].text = requiredCoins.SPEED[i].z.ToString();

            brownCoinsSize[i].text = requiredCoins.SIZE[i].x.ToString();
            redCoinsSize[i].text = requiredCoins.SIZE[i].y.ToString();
            blackCoinsSize[i].text = requiredCoins.SIZE[i].z.ToString();
        }

        brownCoinsBoots.text = requiredCoins.BOOTS.x.ToString();
        redCoinsBoots.text = requiredCoins.BOOTS.y.ToString();
        blackCoinsBoots.text = requiredCoins.BOOTS.z.ToString();

        brownCoinsTorch.text = requiredCoins.TORCH.x.ToString();
        redCoinsTorch.text = requiredCoins.TORCH.y.ToString();
        blackCoinsTorch.text = requiredCoins.TORCH.z.ToString();

        brownCoins.text = ShopManager.INSTANCE.coins.x.ToString();
        redCoins.text = ShopManager.INSTANCE.coins.y.ToString();
        blackCoins.text = ShopManager.INSTANCE.coins.z.ToString();

        for (int i = 0; i <= 3; i++)
        {
            if (requiredCoins.SIZE[i].x > ShopManager.INSTANCE.coins.x
                || requiredCoins.SIZE[i].y > ShopManager.INSTANCE.coins.y
                || requiredCoins.SIZE[i].z > ShopManager.INSTANCE.coins.z)
            {
                ColorBlock cb = sizes[i].colors;
                cb.highlightedColor = alertColor;
                sizes[i].colors = cb;
            }
            else
            {
                ColorBlock cb = sizes[i].colors;
                cb.highlightedColor = highlightColor;
                sizes[i].colors = cb;
            }
        }
        for (int i = 0; i <= 3; i++)
        {
            if (requiredCoins.SPEED[i].x > ShopManager.INSTANCE.coins.x
                || requiredCoins.SPEED[i].y > ShopManager.INSTANCE.coins.y
                || requiredCoins.SPEED[i].z > ShopManager.INSTANCE.coins.z)
            {
                ColorBlock cb = speeds[i].colors;
                cb.highlightedColor = alertColor;
                speeds[i].colors = cb;
            }
            else
            {
                ColorBlock cb = speeds[i].colors;
                cb.highlightedColor = highlightColor;
                speeds[i].colors = cb;
            }
        }

        if (requiredCoins.BOOTS.x > ShopManager.INSTANCE.coins.x
            || requiredCoins.BOOTS.y > ShopManager.INSTANCE.coins.y
            || requiredCoins.BOOTS.z > ShopManager.INSTANCE.coins.z)
        {
            ColorBlock cb = boots.colors;
            cb.highlightedColor = alertColor;
            boots.colors = cb;
        }
        else
        {
            ColorBlock cb = boots.colors;
            cb.highlightedColor = highlightColor;
            boots.colors = cb;
        }

        if (requiredCoins.TORCH.x > ShopManager.INSTANCE.coins.x
            || requiredCoins.TORCH.y > ShopManager.INSTANCE.coins.y
            || requiredCoins.TORCH.z > ShopManager.INSTANCE.coins.z)
        {
            ColorBlock cb = torch.colors;
            cb.highlightedColor = alertColor;
            torch.colors = cb;
        }
        else
        {
            ColorBlock cb = torch.colors;
            cb.highlightedColor = highlightColor;
            torch.colors = cb;
        }

    }

    Color highlightColor = new Color32(224, 193, 149, 255);
    Color alertColor = new Color32(224, 153, 149, 255);
}
