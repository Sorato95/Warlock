public class MovementInfluence {
	
	private float val;
	private string operand;
	
	public MovementInfluence (float val, string AddOrMultiplyOrDivide){
		this.val = val;
		this.operand = AddOrMultiplyOrDivide;
	}
	
	public apply (float speedToInfluence){
		switch(operand){
			case "Add": 		return speedToInfluence + val;
			case "Multiply":	return speedToInfluence * val;
			case "Divide":		return speedToInfluence / val;
			default:			throw new InvalidOperandException();
		}
	}
	
}