using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Battle/Item")]
public class ItemInformation : ScriptableObject, ItemBase
{
    public string ItemName => throw new System.NotImplementedException();

    public string Description => throw new System.NotImplementedException();

    public int HealAmount => throw new System.NotImplementedException();
}
