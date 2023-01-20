using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public int coins = 300;
    public Item[] buyables;

    public Text coinText;
    public GameObject shopUI;
    public Transform shopContent;
    public GameObject itemPrefab;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach(Item buyable in buyables)
        {
            GameObject item = Instantiate(itemPrefab, shopContent);

            foreach(Transform child in item.transform)
            {
                if (child.gameObject.name == "Quantity")
                {
                    Debug.Log("chegou aqui");
                    child.gameObject.GetComponent<Text>().text = buyable.quantity.ToString();
                }
                else if (child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<Text>().text = "$" + buyable.value.ToString();
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

    public void BuyItem(Item buyable)
    {
        if (coins >= buyable.value && buyable.quantity >= 1)
        {
            buyable.quantity--;
            coins -= buyable.value;
            buyable.quantity.ToString();
            // ApllyBuyable(buyable);
        }

    }
    public void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }

    private void OnGUI() {
        coinText.text = coins.ToString();
    }
}
