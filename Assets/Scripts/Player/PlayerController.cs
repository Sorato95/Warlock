using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 10.0F;

    public RectTransform healthBar;
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public MouseLook mouseLook;

    public Camera playerCam;
    private CharacterController controller;

    private bool isOnLavaZone;
    private float friction = 5f;

    private float minFov = 8f;
    private float maxFov = 60f;
    private float sensitivity = 10f;
    private float curFov;

    private Vector3 curVelocity = Vector3.zero;

    public GameObject gameField;
    public GameObject bulletTest;
    private Rigidbody rigidBody;

    private Vector3 impact = Vector3.zero;


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        mouseLook.Init(transform, playerCam.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        mouseLook.LookRotation(transform, playerCam.transform);

        Vector3 velocity = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * moveSpeed;

        curVelocity = Vector3.Lerp(curVelocity, velocity, friction * Time.deltaTime);
        
        controller.Move(curVelocity * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        if (impact.magnitude > 0.2F)
        {
            controller.Move(impact * Time.deltaTime);
        }

        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, friction * Time.deltaTime);

        if (friction <= 5)
        {
            friction += 0.01f;
        }

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

        if (Input.GetKeyDown(KeyCode.K))
        {
            Knockback(Vector3.back, 250);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameField.transform.localScale -= new Vector3(0.1F, 0, 0.1F);
        }

        mouseLook.UpdateCursorLock();
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
        friction = 0.5f;

        direction.Normalize();
        direction.y = 0;

        impact += direction.normalized * force / 3.0F;
    }

    public void OnSpellHit(Spell source)
    {
        //source.affectPlayer(gameObject.active);
    }

}
