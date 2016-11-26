using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public Transform spellSpawner;           //assigned by Inspector
    public float standardMoveSpeed;          //assigned by Inspector

    private MovementManager movementManager;
    public MovementManager MovementManager
    {
        get
        {
            return movementManager;
        }
    }

    private float curMoveSpeed;              //controlled by movementManager
    public float CurMoveSpeed
    {
        get
        {
            return curMoveSpeed;
        }
        set
        {
            this.curMoveSpeed = value;
        }
    }

    public RectTransform healthBar;
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public MouseLook mouseLook;

    public Camera playerCam;

    private bool isOnLavaZone;
    public float friction = 5f;

    private float minFov = 8f;
    private float maxFov = 60f;
    private float sensitivity = 10f;
    private float curFov;

    private Vector3 curVelocity = Vector3.zero;

    private GameObject gameField;
    public GameObject bulletTest;
    private Rigidbody rigidBody;

    private Vector3 impact = Vector3.zero;

    public PlayerSync playerSync;

    public Text textPlayerId;

    private SpellBook spellBook;

    //for testing purposes
	public ScriptableSpell test_Fireball;
    public ScriptableSpell test_SpeedBoost;


    // "constructor" when script is initialized
    void Awake()
    {
        NetworkManager.singleton.client.RegisterHandler(MsgCollisionDetected.MSGID, OnHit);
        playerSync = GetComponent<PlayerSync>();

        spellBook = new SpellBook(this);
        movementManager = new MovementManager(this);
    }

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        if (isLocalPlayer)
        {
            textPlayerId.text = "PlayerId: " + netId;
            mouseLook.Init(transform, playerCam.transform);
            transform.position = new Vector3(transform.position.x, 1, transform.position.y);
        }

        
        //for testing purposes
        spellBook.AddAndInitialize(new SpellBookItem(test_Fireball, 1));
        spellBook.AddAndInitialize(new SpellBookItem(test_SpeedBoost, 1));
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector3 velocity = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * curMoveSpeed;

        curVelocity = Vector3.Lerp(curVelocity, velocity, friction * Time.deltaTime);

        rigidBody.velocity = curVelocity;

        if (impact.magnitude > 0.2F)
        {
            rigidBody.velocity = curVelocity - impact;
        }

        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, friction * Time.deltaTime);


        if (friction < 5)
        {
            friction += 0.35f * Time.deltaTime;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.gameObject.tag == "LavaZone")
            {
                isOnLavaZone = true;
            }
            else
            {
                isOnLavaZone = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        mouseLook.LookRotation(transform, playerCam.transform);

        if (curFov == 0)
        {
            curFov = 28f;
        }
        else
        {
            curFov = playerCam.fieldOfView;
            curFov += (Input.GetAxis("Mouse ScrollWheel") * -1) * sensitivity;
            curFov = Mathf.Clamp(curFov, minFov, maxFov);
        }

        playerCam.fieldOfView = curFov;

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameField.transform.localScale -= new Vector3(0.1F, 0, 0.1F);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))        //only for testing purposes
        {
            CmdTestFireball(this.netId);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))        //only for testing purposes
        {
            spellBook[1].generateSpell();            //speedboost
        }

		if (Input.GetKeyDown (KeyCode.L)) {
			DebugConsole.isVisible = !DebugConsole.isVisible;
		}

        if (Input.GetKeyDown(KeyCode.Plus))
        {
            playerSync.SyncPlayer();
        }

        mouseLook.UpdateCursorLock();
    }



	[Command]
	void CmdTestFireball(NetworkInstanceId casterNetworkId) {
        GameObject fireball = spellBook[0].generateSpell();
		NetworkServer.Spawn (fireball);
	}

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            // TODO: player dead -> do stuff
        }
    }


    public void Knockback(Vector3 direction, float force)
    {
        friction = 0.3f;

        direction.Normalize();
        direction.y = 0;

        impact += direction.normalized * force / 5.0F;
    }

    //called on client
    [ClientCallback]
    public void OnHit(NetworkMessage msg)
    {
        MsgCollisionDetected receivedMsg = msg.ReadMessage<MsgCollisionDetected>();

        PlayerController playerCtrl = ClientScene.FindLocalObject(receivedMsg.playerHitNetId).GetComponent<PlayerController>();

        DebugConsole.Log("OnHit called for player" + playerCtrl.netId);
        playerCtrl.TakeDamage(receivedMsg.damageDealt);
        playerCtrl.Knockback(receivedMsg.pushDirection, receivedMsg.knockbackForce);
    }
    
    // called on server
    [ServerCallback]
	public void ServerOnHit(ProjectileSpell source) {
		if (!isServer) {
			return;
		}

        var msg = new MsgCollisionDetected();
        msg.playerHitNetId = this.netId;
        msg.damageDealt = source.DamageDealt;
        msg.knockbackForce = source.KnockBackForce;
        msg.pushDirection = source.PushDirection;

        //source.affectPlayer(playerHit);       TO BE IMPLEMENTED !!

        base.connectionToClient.Send(MsgCollisionDetected.MSGID, msg);
	}

    public Transform getSpellSpawner()
    {
        return spellSpawner;
    }
}
