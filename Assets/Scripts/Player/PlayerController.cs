﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public Transform spellSpawner;          //assigned by Inspector
    public float moveSpeed = 10.0F;

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

    public GameObject gameField;
    public GameObject bulletTest;
    private Rigidbody rigidBody;

    private Vector3 impact = Vector3.zero;

    public GameObject fireballPrefab;

    private List<SpellBookItem> spellBook = new List<SpellBookItem>();
    private static bool isEventListenerAdded = false;
    public static OnSpellHitEvent onSpellHitEvent = new OnSpellHitEvent();

    void Awake()
    {
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
        mouseLook.Init(transform, playerCam.transform);

        transform.position = new Vector3(transform.position.x, 1, transform.position.y);

        //only for testing purposes - later spells will be added to spellbook from merchant
        spellBook.Add(new SpellBookItem<Fireball>((GameObject) Resources.Load("Prefabs/Fireball", typeof(GameObject)), 1));
        spellBook.Add(new SpellBookItem<SpeedBoost>(null, 1));
    }

    void FixedUpdate()
    {
        Vector3 velocity = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * moveSpeed;

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
            GameObject fireball = spellBook[1].generateSpell(this);
        }

        mouseLook.UpdateCursorLock();
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

    public static void OnSpellHit(PlayerController affectedPlayer, ProjectileSpell source, Vector3 pushDir)
    {
        affectedPlayer.Knockback(-pushDir, source.getKnockbackForce());
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
