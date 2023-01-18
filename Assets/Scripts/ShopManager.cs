using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public int coins = 300;
    public Buyable[] buyables;

    public Text coinText;
    public GameObject shopUI;
    public Transform shopContent;
    public GameObject itemPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        foreach(Buyable buyable in buyables)
        {
            GameObject item = Instantiate(itemPrefab, shopContent);

            buyable.itemRef = item;
            foreach(Transform child in item.transform)
            {
                if (child.gameObject.name == "Quantity")
                {
                    child.gameObject.GetComponent<Text>().text = buyable.quantity.ToString();
                }
                else if (child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<Text>().text = "$" + buyable.cost.ToString();
                }
                else if (child.gameObject.name == "Name")
                {
                    child.gameObject.GetComponent<Text>().text = buyable.name.ToString();
                }
                else if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = buyable.image;
                }
            }

            item.GetComponent<Button>().onClick.AddListener(() => {
                BuyItem(buyable);
            });
        }   
    }

    public void BuyItem(Buyable buyable)
    {
        if (coins >= buyable.cost)
        {
            coins -= buyable.cost;
            buyable.quantity++;
            buyable.itemRef.transform.GetChild(0).GetComponent<Text>().text = buyable.quantity.ToString();
            // ApllyBuyable(buyable);
        }
    }
    public void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }

    private void OnGUI() {
        coinText.text = "Coins: " + coins.ToString();
    }
}


[System.Serializable]
public class Buyable
{
    public string name;
    public int cost;
    public Sprite image;
    public int quantity;
    public GameObject itemRef;

}