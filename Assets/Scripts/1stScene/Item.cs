using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TMP_Text textPrice;
    [SerializeField] private Image image;

    private ItemConfig config;
    private Transform parentObject;
    private int siblingIndex;
    private bool moveToMouse;
    private float sale;
    private ItemsContainerView _container;
    public ItemsContainerView Container => _container;
    public int Coast => config.Cost - (int)(config.Cost * sale);

    public static Action<Item> OnSelect;
    public static Action OnDrop;

    public void Initialize(ItemConfig itemConfig, ItemsContainerView container)
    {
        config = itemConfig;
        textPrice.text = $"{config.Cost - (int)(config.Cost * sale)}";
        image.sprite = config.Image;
        _container = container;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        parentObject = transform.parent;
        siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(transform.parent.parent);
        moveToMouse = true;
        image.raycastTarget = false;

        OnSelect?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.raycastTarget = true;
        OnDrop?.Invoke();
    }

    public void BackHome()
    {
        moveToMouse = false;
        transform.SetParent(parentObject);
        transform.SetSiblingIndex(siblingIndex);
    }

    public void SetNewHome(ItemsContainerView container)
    {
        _container.RemoveItem(config.Id);
        moveToMouse = false;
        _container = container;
        transform.SetParent(container.transform);
        _container.AddItem(config.Id);
    }

    public void SetSale(float newSale)
    {
        sale = newSale;
        textPrice.text = $"{config.Cost - (int)(config.Cost * sale)}";
    }

    private void Update()
    {
        if (moveToMouse)
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouse.x, mouse.y);
        }
    }
}
