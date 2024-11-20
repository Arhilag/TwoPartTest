using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldWidget : MonoBehaviour
{
    [SerializeField] private ProfileBase profile;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        profile.OnChangeGold += ChangeGold;
    }

    private void OnDestroy()
    {
        profile.OnChangeGold -= ChangeGold;
    }

    private void Start()
    {
        ChangeGold(profile.GetGold());
    }

    private void ChangeGold(int value)
    {
        text.text = $"{value} gold";
    }
}
