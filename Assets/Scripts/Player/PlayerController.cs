using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public Transform spellSpawner;           //assigned by Inspector
    public float standardMoveSpeed;          //assigned by Inspector
    public MovementManager movementManager;
    public float curMoveSpeed;               //asigned by movementManager

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

    private List<SpellBookItem> spellBook = new List<SpellBookItem>();
    private bool isEventListenerAdded = false;
    public OnSpellHitEvent onSpellHitEvent;

	public ScriptableProjectileSpell test_Fireball;

    // "constructor" when script is initialized
    void Awake()
    {
        onSpellHitEvent = new OnSpellHitEvent();
        playerSync = GetComponent<PlayerSync>();
        //gameField = GameField.getGameField().gameObject;

        movementManager = new MovementManager(this);

        if (!isEventListenerAdded)
        {
            onSpellHitEvent.AddListener(OnSpellHit);
            isEventListenerAdded = true;
        }
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

        //only for testing purposes - later spells will be added to spellbook from merchant
        spellBook.Add(new SpellBookItem<Fireball>( (GameObject) Resources.Load("Prefabs/Fireball"), 1 ));
        spellBook.Add(new SpellBookItem<SpeedBoost>(null, 1));

		DebugConsole.Log ("testmessage");
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
            CmdCast();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))        //only for testing purposes
        {
            spellBook[1].generateSpell(this);       //speedboost
        }

		if (Input.GetKeyDown (KeyCode.H)) {
			CmdTestFireball (netId);
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			DebugConsole.isVisible = !DebugConsole.isVisible;
		}

        mouseLook.UpdateCursorLock();
    }

	[Command]
	void CmdTestFireball(NetworkInstanceId casterNetworkId) {
		test_Fireball.Initialize (casterNetworkId);
		GameObject fireball = test_Fireball.TriggerSpell ();
		NetworkServer.Spawn (fireball);
	}

    [Command]
    void CmdCast()
    {
        GameObject fireball = spellBook[0].generateSpell(this);
        NetworkServer.Spawn(fireball);
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

    public void OnSpellHit(ProjectileSpell source, Vector3 pushDir)
    {
        Debug.Log("OnSpellHit called for player" + this.netId);
        this.Knockback(-pushDir, source.getKnockbackForce());
    }

	public void OnHit(ProjectileSpellScript source) {
		if (!isLocalPlayer) {
			return;
		}
		Debug.Log("OnHit called for player" + this.netId);
		this.Knockback(-source.transform.forward, source.knockBackForce);
	}

    public Transform getSpellSpawner()
    {
        return spellSpawner;
    }

    public OnSpellHitEvent getOnSpellHitEvent()
    {
        return onSpellHitEvent;
    }
}
