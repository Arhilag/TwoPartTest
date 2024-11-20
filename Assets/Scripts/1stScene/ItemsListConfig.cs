using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsListConfig", menuName = "Configs/ItemsListConfig", order = 1)]
public class ItemsListConfig : ScriptableObject
{
    [SerializeField]
    private ItemConfig[] items;
    public ItemConfig[] Items => items;
}
