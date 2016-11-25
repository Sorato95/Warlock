using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

//QoS channels used:
//channel #0: Reliable Sequenced
//channel #1: Unreliable Sequenced
[NetworkSettings(channel=1,sendInterval=0.05f)]
public class NetworkMovement : NetworkBehaviour {

	public MouseLook mlook;

	public struct Inputs {
		public float vertical;
		public float horizontal;
		public float yaw;
		public float pitch;

		public float timeStamp;
	}

	public struct Results {
		public Quaternion rotation;
		public Vector3 position;

		public float timeStamp;
	}

	private Inputs inputs;

	[SyncVar(hook="RecieveResults")]
	private Results syncResults;

	private Results results;

	private List<Inputs> inputsList = new List<Inputs>();
	private List<Results> resultsList = new List<Results>();

	private float dataStep = 0;
	private float lastTimeStamp = 0;

	private float step = 0;

	void Update() {
		if (isLocalPlayer) {
			GetInputs (ref inputs);
		}
	}

	void FixedUpdate () {

	}

	public virtual void GetInputs(ref Inputs inputs) {
		inputs.horizontal = RoundToLargest(Input.GetAxis ("Horizontal"));
		inputs.vertical = RoundToLargest(Input.GetAxis ("Vertical"));

		inputs.yaw = -Input.GetAxis("Mouse Y") * 2f * Time.fixedDeltaTime/Time.deltaTime;
		inputs.pitch = Input.GetAxis("Mouse X") * 2f * Time.fixedDeltaTime/Time.deltaTime;
	}

	float RoundToLargest(float inp){
		if (inp > 0) {
			return 1;
		} else if (inp < 0) {
			return -1;
		}
		return 0;
	}
}