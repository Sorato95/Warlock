using UnityEngine;
using System.Collections;

public class Fireball : Spell {

    public float timeToLive;

    private int shotSpeed = 0;
    private Vector3 moveDirection;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(moveDirection * shotSpeed);
        Destroy(gameObject, timeToLive);                    //destroy bullet after <timeToLive> seconds
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter (Collision c)
    {
        if (c.gameObject.name == "Player")
        {
            Destroy(gameObject);

            c.gameObject.SendMessage("OnBulletHit", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void setShotSpeed(int shotSpeed)
    {
        this.shotSpeed = shotSpeed;
    }

    public void setMoveDirection(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }
}
