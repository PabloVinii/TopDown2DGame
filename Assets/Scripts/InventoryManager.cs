using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>(); 

    public Transform itemContent;
    public GameObject inventoryItem;

    public Toggle enableRemove;
    public InventoryItemController[] inventoryItems;

    void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {   

        //Clear Content before open
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("Name").GetComponent<Text>();
            var itemImage = obj.transform.Find("Image").GetComponent<Image>();
            var removeButton = obj.transform.Find("Remove").GetComponent<Button>();

            itemName.text = item.itemName;
            itemImage.sprite = item.image;

            if (enableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if(enableRemove.isOn)
        {
            foreach (Transform item in itemContent)
            {
                item.Find("Remove").gameObject.SetActive(true);
            }
        } 
        else
        {
            foreach (Transform item in itemContent)
            {
                item.Find("Remove").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }
}