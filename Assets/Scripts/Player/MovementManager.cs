using UnityEngine;
using System.Collections;

public class MovementManager {

    PlayerController playerToManage;

	public MovementManager(PlayerController playerToManage)
    {
        this.playerToManage = playerToManage;
        playerToManage.curMoveSpeed = playerToManage.standardMoveSpeed;
    }
}
