using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    private int gems;
    public TextMeshProUGUI gemText; //Text for how many gems the player has

    [Header("Items being sold")]
    public ShopItem[] shopItems;

    public GameObject shopItemPrefab;
    public Transform shopLayout;

    private GameObject temp;

    private void Start()
    {
        gems = PlayerPrefs.GetInt("Money");
        gemText.text = gems.ToString();
        //PlayerPrefs.DeleteAll();
        PopulateShop();
    }

    private void PopulateShop()
    {
        for (int i = 0; i < shopItems.Length; i++) //Loop through all our shop items
        {
            ShopItem currentItem = shopItems[i];
            currentItem.ID = i;

            GameObject itemContainer = Instantiate(shopItemPrefab, shopLayout);

            if (PlayerPrefs.GetInt("CurrentItem") == i) //If player is currently using this item
            {
                temp = itemContainer;
                temp.GetComponent<Image>().color = Color.blue;
            }

            if (PlayerPrefs.GetInt(currentItem.trailName) == 1 || currentItem.ID == 0) //If the item has already been sold to the player or is our default trail at element 0
            {
                itemContainer.GetComponent<Button>().onClick.AddListener(() => SelectTrail(currentItem, itemContainer)); //Add an onclick event & pass our current item to the method
                itemContainer.transform.GetChild(0).GetComponent<Image>().sprite = currentItem.icon; //Change the container sprite to be that of our trail icon
                itemContainer.transform.GetChild(1).gameObject.SetActive(false); //Disable text
            }
            else if(currentItem.trailName == "Coming Soon") //If the trail has not been implemented yet
            {
                itemContainer.transform.GetChild(0).GetComponent<Image>().sprite = currentItem.icon; //Change the container sprite to be that of our trail icon
                itemContainer.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Locked";
            }
            else
            {
                itemContainer.GetComponent<Button>().onClick.AddListener(() => BuyTrail(currentItem, itemContainer)); //Add an onclick event & pass our current item to the method
                itemContainer.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentItem.cost.ToString(); //Change the containter text to be the cost of our trail 
            }
        }
    }

    private void BuyTrail(ShopItem item, GameObject container)
    {
        if (temp != null)
        {
            temp.GetComponent<Image>().color = Color.white;
        }

        Debug.Log(item.trailName);
        if (item.cost <= gems)
        {
            gems -= item.cost; //Buy the new item with our gems
            gemText.text = gems.ToString(); //Update our counter

            PlayerPrefs.SetInt("Money", gems);

            container.transform.GetChild(1).gameObject.SetActive(false); //Disable text
            container.transform.GetChild(0).GetComponent<Image>().sprite = item.icon; //Change the container sprite to be that of our trail icon

            temp = container;
            temp.GetComponent<Image>().color = Color.blue;

            container.GetComponent<Button>().onClick.RemoveAllListeners(); //Remove old listener 
            container.GetComponent<Button>().onClick.AddListener(() => SelectTrail(item, container)); //Add new listener now that the item has already been bought

            PlayerPrefs.SetInt("CurrentItem", item.ID); //Set the current item ID
            PlayerPrefs.SetInt(item.trailName, 1); //Set the trail equal to 1 (sold);

        }
    }

    private void SelectTrail(ShopItem item, GameObject container)
    {
        if (temp != null)
        {
            temp.GetComponent<Image>().color = Color.white;
        }

        temp = container;
        temp.GetComponent<Image>().color = Color.blue;

        PlayerPrefs.SetInt("CurrentItem", item.ID); //Set the current item ID
    }

}
