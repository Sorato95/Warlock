using UnityEngine;
using System.Collections;

public class MovementManager {

    PlayerController playerToManage;

	public MovementManager(PlayerController playerToManage)
    {
        this.playerToManage = playerToManage;
        playerToManage.curMoveSpeed = playerToManage.standardMoveSpeed;
    }

    public float impact(float value, string MultiplyOrAddOrDivide)
    {
        float previousSpeed = playerToManage.curMoveSpeed;

        switch (MultiplyOrAddOrDivide)
        {
            case "Multiply":    playerToManage.curMoveSpeed *= value;
                                break;
            case "Add":         playerToManage.curMoveSpeed += value;
                                break;
            case "Divide":      playerToManage.curMoveSpeed /= value;
                                break;

        }

        return previousSpeed - playerToManage.curMoveSpeed;
    }
}
