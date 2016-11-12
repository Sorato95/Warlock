using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10.0F;

    public MouseLook mouseLook;

    private Camera mainCamera;
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
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        mouseLook.Init(transform, mainCamera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        mouseLook.LookRotation(transform, mainCamera.transform);

        Vector3 velocity = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), -1, Input.GetAxis("Vertical"))) * moveSpeed;

        curVelocity = Vector3.Lerp(curVelocity, velocity, friction * Time.deltaTime);



        controller.Move(curVelocity * Time.deltaTime);

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
            curFov = Camera.main.fieldOfView;
            curFov += (Input.GetAxis("Mouse ScrollWheel") * -1) * sensitivity;
            curFov = Mathf.Clamp(curFov, minFov, maxFov);
        }

        Camera.main.fieldOfView = curFov;

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




    public void Knockback(Vector3 direction, float force)
    {
        friction = 0.5f;

        direction.Normalize();
        direction.y = 0;

        impact += direction.normalized * force / 3.0F;
    }

    public void OnSpellHit(Spell source)
    {
        source.affectPlayer(gameObject);
    }

}
