using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : ProfileBase
{
    protected override int BaseGold => 150;
    protected override List<int> BaseItems => new List<int>()
    {
        1,2,3,4,5,6
    };
}