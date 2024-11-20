using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProfileBase : MonoBehaviour
{
    protected TraderData traderData;
    protected virtual string SaveString => "save";
    protected virtual int BaseGold => 0;
    protected virtual List<int> BaseItems => new List<int>();

    public event Action<int> OnChangeGold;

    public List<int> GetItems() 
    {
        return traderData.items;
    }
    public int GetGold()
    {
        return traderData.Gold;
    }
    public void RemoveItem(int item)
    {
        traderData.items.Remove(item);
    }
    public void AddItem(int newItem)
    {
        traderData.items.Add(newItem);
    }

    public bool TryBuy(int price)
    {
        if(price <= traderData.Gold)
        {
            traderData.Gold -= price;
            OnChangeGold?.Invoke(traderData.Gold);
            return true;
        }
        return false;
    }

    public void AddGold(int value)
    {
        traderData.Gold += value;
        OnChangeGold?.Invoke(traderData.Gold);
    }

    public void Awake()
    {
        traderData = Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetString(SaveString, JsonUtility.ToJson(traderData));
    }

    private TraderData Load()
    {
        if (PlayerPrefs.HasKey(SaveString))
        {
            return JsonUtility.FromJson<TraderData>(PlayerPrefs.GetString(SaveString));
        }

        var data = new TraderData();
        data.Gold = BaseGold;
        data.items = BaseItems;

        return data;
    }
}

[Serializable]
public struct TraderData
{
    public int Gold;
    public List<int> items;
}