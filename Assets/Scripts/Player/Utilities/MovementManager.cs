using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementManager {
	
	private PlayerController playerToManage;
	
	private Dictionary<int, MovementInfluence> influencesByIntKey;
	private int latestKey = 0;
	
	public MovementManager (PlayerController playerToManage){
		this.playerToManage = playerToManage;
		influencesByIntKey = new Dictionary<int, MovementInfluence>();
        playerToManage.CurMoveSpeed = playerToManage.standardMoveSpeed;
	}
	
	public void setStandardMoveSpeed (float standardMoveSpeed){
		playerToManage.standardMoveSpeed = standardMoveSpeed;
	}
	
	public int addInfluence (float val, string AddOrMultiplyOrDivide){
        latestKey = findKey();
		influencesByIntKey.Add(findKey(), new MovementInfluence(val, AddOrMultiplyOrDivide));
		applyAllInfluences();
        return latestKey;
	}
	
	public void removeInfluence (int intKey) {
		influencesByIntKey.Remove(intKey);
		applyAllInfluences();
	}
	
	private void applyAllInfluences(){
		float newSpeed = playerToManage.standardMoveSpeed;
		
		foreach (int intKey in influencesByIntKey.Keys) {
			newSpeed = influencesByIntKey[intKey].apply(newSpeed);;
		}
		
		playerToManage.CurMoveSpeed = newSpeed;
	}
	
	private int findKey (){
		if (latestKey > 1500){
			latestKey = 0;
		}
		else{
			latestKey++;
		}
		return latestKey;
	}
}