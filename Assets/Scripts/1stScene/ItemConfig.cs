using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig", order = 0)]
public class ItemConfig : ScriptableObject
{
    [SerializeField]
    private int id;
    public int Id => id;
    [SerializeField]
    private Sprite image;
    public Sprite Image => image;
    [SerializeField]
    private int cost;
    public int Cost => cost;
}
