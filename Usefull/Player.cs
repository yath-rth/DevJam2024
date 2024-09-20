using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    public static Player instance;

    public NavMeshAgent agent { get; private set; }
    FieldOfView fov;
    Rigidbody rb;

    [SerializeField] inputManager inputMan;

    [Header("playercontrols")]
    [SerializeField, Range(0, 50f)] float speed;
    [SerializeField] GameObject walkParticles;
    Vector3 moveDirection, moveInput;
    Vector2 moveInputTemp;

    [Header("Rotation")]
    [SerializeField] Transform crossHair;
    [SerializeField, Range(0, 10f)] float crossHairOffset;
    Vector2 look;
    Vector3 mouse, Point, correctLookAt;
    float point;

    [Header("Stats")]
    [SerializeField] HealthSystem health;
    [SerializeField] SkinnedMeshRenderer mesh;

    [Header("Random")]
    [Range(0, 10f)] public float playerCollisionRad;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        rb = GetComponent<Rigidbody>();
        inputMan.newInput();
        health.newEntity();

        health.death += playerDeath;
    }

    private void OnEnable()
    {
        inputMan.inputEnable();
    }

    private void OnDisable()
    {
        inputMan.inputDisable();
        health.death -= playerDeath;
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
        health.death -= playerDeath;
    }

    private void Update()
    {
        if (inputMan.input.asset.enabled == true)
        {
            if (agent.isActiveAndEnabled == true) move();
            rotate();
        }

        if ((health.health - health.maxHealth) / health.maxHealth >= 1)
        {
            inputMan.input.asset.Disable();
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    void move()
    {
        agent.speed = speed;

        moveInputTemp = inputMan.input.playercontrols.Move.ReadValue<Vector2>();
        moveInput = new Vector3(moveInputTemp.x, 0, moveInputTemp.y).normalized;

        moveDirection = moveInput * speed * Time.deltaTime;

        //To move in direction player is looking
        //moveDirection = transform.TransformDirection(moveDirection);

        agent.Move(moveDirection);
        agent.SetDestination(transform.position + moveDirection);

        if(moveInput.magnitude > 0) walkParticles.SetActive(true);
        else walkParticles.SetActive(false);

        #region Walk Particles
        /*if (moveInput.x > 0 && moveInput.z == 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (moveInput.x < 0 && moveInput.z == 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (moveInput.z > 0 && moveInput.x == 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveInput.x < 0 && moveInput.x == 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput.x > 0 && moveInput.z > 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (moveInput.x < 0 && moveInput.z > 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, -45, 0);
        }
        else if (moveInput.z < 0 && moveInput.x > 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else if (moveInput.x < 0 && moveInput.x < 0)
        {
            walkParticles.transform.rotation = Quaternion.Euler(0, -135, 0);
        }

        walkParticles.Play();*/
        #endregion
    }

    void rotateMouse()
    {
        look = inputMan.input.playercontrols.Look.ReadValue<Vector2>();

        mouse = new Vector3(look.x, look.y, 0);

        Ray ray = Camera.main.ScreenPointToRay(mouse);

        Plane groundPlane = new Plane(Vector3.up, Vector3.up);

        if (groundPlane.Raycast(ray, out point))
        {
            Point = ray.GetPoint(point);
            correctLookAt = new Vector3(Point.x, transform.position.y, Point.z);
            transform.LookAt(correctLookAt);

            crossHair.position = correctLookAt;

            if (Vector3.Distance(correctLookAt, transform.position) < 0.5f)
            {
                //To avoid bug of not moving when pointer is exactly on player
                //transform.rotation = correctLookAt + 1;
            }
        }
    }

    void rotate()
    {
        if (fov.closestTarget == null && moveInput.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveInput);
            crossHair.transform.position = transform.forward * crossHairOffset;
            crossHair.gameObject.SetActive(false);
        }
        else if (fov.closestTarget != null)
        {
            crossHair.gameObject.SetActive(true);
            crossHair.transform.position = fov.closestTarget.transform.position + new Vector3(0, .2f, 0);
            transform.LookAt(crossHair.transform);
        }
    }

    void playerDeath()
    {
        print("Player has died");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerCollisionRad);
    }
}
