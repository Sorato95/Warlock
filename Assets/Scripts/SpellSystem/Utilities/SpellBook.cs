using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellBook : List<SpellBookItem>{

    private PlayerController player;            //the player whom this SpellBook belongs to

	public SpellBook(PlayerController player) : base()
    {
        this.player = player;
    }

    public void AddAndInitialize(SpellBookItem item)
    {
        base.Add(item);
        item.SpellAsset.Initialize(player.netId);
    }
}
