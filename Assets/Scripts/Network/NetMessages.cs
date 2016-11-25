using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MsgCollisionDetected : MessageBase {
    public const int MSGID = 100;

    public NetworkInstanceId netId;
    public float knockbackForce;
    public Vector3 pushDirection;
}