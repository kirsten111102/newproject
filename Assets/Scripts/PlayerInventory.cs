using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private CoinCounterUI coinCounterUI;
    private int coinCount;
    public int CoinCount
    {
        get { return coinCount; }
    }
    private List<Item> items = new List<Item>();

    private void Start()
    {
        if (coinCounterUI != null)
            coinCounterUI.SetCoinCount(coinCount);
    }

    public void AddCoin(int amount)
    {
        if (amount <= 0) return;

        coinCount += amount;
        if (coinCounterUI != null)
            coinCounterUI.SetCoinCount(coinCount);
    }

    public Item FindItem(int id)
    {
        foreach (Item item in items)
        {
            if (item.Id == id)
                return item;
        }
        return null;
    }

    public void AddItem(Item newItem)
    {
        Item existingItem = FindItem(newItem.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += newItem.Quantity;
            return;
        }

        items.Add(newItem);
    }

    public bool RemoveItem(int id, int amount)
    {
        if (amount <= 0) return false;

        Item item = FindItem(id);
        if (item == null) return false;
        if (item.Quantity < amount) return false;

        item.Quantity -= amount;
        if (item.Quantity == 0) items.Remove(item);

        return true;
    }
}
