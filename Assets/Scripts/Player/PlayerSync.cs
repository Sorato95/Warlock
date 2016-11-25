using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{

    [SyncVar]
    private Vector3 syncPos;

    [SyncVar]
    private Quaternion syncPlayerRotation;
    

    public Transform playerTransform;

    public float lerpRate = 15;

    public void FixedUpdate()
    {
        TransmitTransform();

        LerpPosition();
        LerpRotation();
    }

    public void SyncPlayer()
    {
        playerTransform.position = syncPos;
        playerTransform.rotation = syncPlayerRotation;
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
    void CmdProvideTransformToServer(Vector3 pos, Quaternion playerRot)
    {
        syncPos = pos;
        syncPlayerRotation = playerRot;
    }


    [ClientCallback]
    void TransmitTransform()
    {
        if (isLocalPlayer)
        {
            CmdProvideTransformToServer(playerTransform.position, playerTransform.rotation);
        }
    }
}
