using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{

    [SyncVar]
    private Vector3 syncPos;

    [SyncVar]
    public Vector3 syncSpellPos;

    [SyncVar]
    private Quaternion syncPlayerRotation;

    public Transform playerTransform;
    public Transform spellSpawnTransform;

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
            spellSpawnTransform.position = Vector3.Lerp(spellSpawnTransform.position, syncSpellPos, Time.deltaTime * lerpRate);
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
    void CmdProvideTransformToServer(Vector3 pos, Quaternion playerRot, Vector3 spellPos)
    {
        syncPos = pos;
        syncSpellPos = spellPos;
        syncPlayerRotation = playerRot;
    }



    [ClientCallback]
    void TransmitTransform()
    {
        if (isLocalPlayer)
        {
            CmdProvideTransformToServer(playerTransform.position, playerTransform.rotation, spellSpawnTransform.position);
        }
    }
}
