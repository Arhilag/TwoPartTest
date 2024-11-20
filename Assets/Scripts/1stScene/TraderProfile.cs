using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderProfile : ProfileBase
{
    protected override string SaveString => "trader";
    protected override List<int> BaseItems => new List<int>()
    {
        1,1,1,1
    };
}
