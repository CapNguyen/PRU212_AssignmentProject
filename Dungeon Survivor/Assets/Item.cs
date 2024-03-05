using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string name;
    public int armor;

    public void Equip(PlayerManager player)
    {
        player.armor += armor;
    }

    public void UnEquip(PlayerManager player)
    {
        player.armor -= armor;
    }
}
