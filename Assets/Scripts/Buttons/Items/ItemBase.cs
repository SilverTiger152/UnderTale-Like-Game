using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemBase
{
    string ItemName { get; }
    string Description { get; }
    int HealAmount { get; }
    //사용한다는 메소드?
}
