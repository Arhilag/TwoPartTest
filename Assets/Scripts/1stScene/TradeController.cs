using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour
{
    [SerializeField] private float saleAfterBuy;
    [SerializeField] private ItemsContainerView playerContainer;

    private ItemsContainerView selectedContainer;
    private Item selectedItem;

    private void Awake()
    {
        ItemsContainerView.OnSelect += SetSelectedContainer;
        ItemsContainerView.OnUnselect += () => SetSelectedContainer(null);
        Item.OnSelect += (item) => selectedItem = item;
        Item.OnDrop += DropItem;
    }

    private void OnDestroy()
    {
        ItemsContainerView.OnSelect -= (container) => selectedContainer = container;
        ItemsContainerView.OnUnselect -= () => SetSelectedContainer(null);
        Item.OnSelect -= (item) => selectedItem = item;
        Item.OnDrop -= DropItem;
    }

    private void SetSelectedContainer(ItemsContainerView itemsContainer)
    {
        selectedContainer = itemsContainer;
    }

    private void DropItem()
    {
        if(selectedContainer && selectedItem.Container != selectedContainer)
        {
            if(playerContainer != selectedContainer)
            {
                playerContainer.SellItem(selectedItem.Coast);
                selectedItem.SetNewHome(selectedContainer);
            }
            else
            {
                if (playerContainer.TryBuyItem(selectedItem.Coast))
                {
                    selectedItem.SetNewHome(playerContainer);
                    selectedItem.SetSale(saleAfterBuy);
                }
                else
                {
                    selectedItem.BackHome();
                }
            }
        }
        else
        {
            selectedItem.BackHome();
        }
    }
}
