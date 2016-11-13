using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TestPlayerController : NetworkBehaviour {

    private float moveSpeed = 10F;


    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }


        Vector3 velocity = Vector3.zero;
        velocity = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * moveSpeed;

        rigidBody.velocity = velocity;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))        //only for testing purposes
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        var bullet = (GameObject)Instantiate(
    bulletPrefab,
    bulletSpawn.position,
    bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0F);
    }
}
