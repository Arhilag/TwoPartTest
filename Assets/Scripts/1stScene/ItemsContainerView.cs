using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System;

public class ItemsContainerView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ItemsListConfig itemsList;
    [SerializeField] private Item itemPrefab;
    [SerializeField] private ProfileBase profile;

    public static event Action<ItemsContainerView> OnSelect;
    public static event Action OnUnselect;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnSelect?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnUnselect?.Invoke();
    }

    private void Start()
    {
        DrawItems(profile.GetItems());
    }

    private void DrawItems(List<int> itemsId)
    {
        foreach(var id in itemsId)
        {
            var itemData = itemsList.Items.First(x => x.Id == id);
            var item = Instantiate(itemPrefab, transform, false);
            item.Initialize(itemData, this);
        }
    }

    public void RemoveItem(int item)
    {
        profile.RemoveItem(item);
    }

    public void AddItem(int newItem)
    {
        profile.AddItem(newItem);
    }

    public void SellItem(int value)
    {
        profile.AddGold(value);
    }

    public bool TryBuyItem(int value)
    {
        return profile.TryBuy(value);
    }
}
