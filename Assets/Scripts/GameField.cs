using UnityEngine;
using System.Collections;

public class GameField : MonoBehaviour {

    private static GameField gameField;

	// Use this for initialization
	void Awake () {
        gameField = this;
	}

    public static GameField getGameField()
    {
        return gameField;
    }
}
