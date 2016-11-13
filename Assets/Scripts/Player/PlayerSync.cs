using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{

    [SyncVar]
    private Vector3 syncPos;

    [SyncVar]
    public Vector3 syncVelocity;

    [SyncVar]
    private Quaternion syncPlayerRotation;

    public Transform playerTransform;
    public Rigidbody playerRigidbody;

    public float lerpRate = 15;

    public void FixedUpdate()
    {
        TransmitTransform();

        LerpPosition();
        LerpRotation();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    void LerpRotation()
    {
        if (!isLocalPlayer)
        {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
        }

    }

    [Command]
    void CmdProvideTransformToServer(Vector3 pos, Quaternion playerRot, Vector3 velocity)
    {
        syncPos = pos;
        syncPlayerRotation = playerRot;
        syncVelocity = velocity;
    }



    [ClientCallback]
    void TransmitTransform()
    {
        if (isLocalPlayer)
        {
            CmdProvideTransformToServer(playerTransform.position, playerTransform.rotation, playerRigidbody.velocity);
        }
    }
}
