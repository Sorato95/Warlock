public class MovementManager(){
	
	private PlayerController playerToManage;
	
	private Dictionary<int, MovementInfluence> influencesByIntKey;
	private int latestKey = 0;
	
	public MovementManager (PlayerController playerToManage){
		this.playerToManage = playerToManage;
		influencesByIntKey = new Dictionary<int, MovementInfluence>();
	}
	
	public setStandardMoveSpeed (float standardMoveSpeed){
		playerToManage.standardMoveSpeed = standardMoveSpeed;
	}
	
	public int addInfluence (float val, string AddOrMultiplyOrDivide){
		influencesByIntKey.Add(findKey(), new MovementInfluence(val, AddOrMultiplyOrDivide);
		applyAllInfluences();
	}
	
	public void removeInfluence (int intKey) {
		influencesByIntKey.Remove(intKey);
		applyAllInfluences();
	}
	
	private void applyAllInfluences(){
		float newSpeed = playerToManage.standardMoveSpeed;
		
		foreach (int intKey : influencesByIntKey.Keys){
			newSpeed = influencesByIntKey.Get(intKey).apply(newSpeed);
		}
		
		playerToManage.curMoveSpeed = newSpeed;
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