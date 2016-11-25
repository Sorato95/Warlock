using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MsgCollisionDetected : MessageBase {
    public const int MSGID = 100;

    public NetworkInstanceId playerHitNetId;
    public int damageDealt;
    public float knockbackForce;
    public Vector3 pushDirection;
}