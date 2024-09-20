using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(1, 50)]
    [SerializeField] float Speed = 5;
    [HideInInspector]
    public Vector3 movementWSAD, move, temp;
    Rigidbody rb;
    Animator am;
    [HideInInspector]
    public bool CanThouMove = true;
    Camera mainCam;
    float point, angle = 0;
    public PlayerInputActions inputActions;
    public bool shouldRotate;
    Vector2 mousePos;
    PlayerStats playerStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        am = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        mainCam = Camera.main;
        inputActions = new PlayerInputActions();
    }

    #region InputActions Enabling
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    #endregion

    private void FixedUpdate()
    {
        if (!playerStats.IsDead)
        {
            move = new Vector3(movementWSAD.normalized.x, 0, movementWSAD.normalized.z);
            if (move.magnitude != null)
            {
                temp = transform.position + move;
            }
            transform.LookAt(temp);

            move = transform.forward * Speed * Time.deltaTime * movementWSAD.normalized.magnitude;

            rb.MovePosition(move + transform.position);

            if (am != null)
            {
                //am.SetFloat("horizontal", movementWSAD.x);
                am.SetFloat("vertical", movementWSAD.normalized.magnitude);

                if (inputActions.Gameplay.Dash.ReadValue<float>() > 0 && movementWSAD.magnitude > 0)
                {
                    am.SetLayerWeight(2, 1);
                }
                else
                {
                    am.SetLayerWeight(2, 0);
                }
            }
        }
    }

    private void Update()
    {
        if (!playerStats.IsDead)
        {
            angle = transform.rotation.y;
            if (shouldRotate) { Rotate(); }

            Vector2 MovementInput = inputActions.Gameplay.Movement.ReadValue<Vector2>();
            mousePos = inputActions.Gameplay.Rotation.ReadValue<Vector2>();

            movementWSAD = new Vector3(MovementInput.x, 0, MovementInput.y).normalized;
            Vector3 mousePosInWorld = mainCam.ScreenToWorldPoint(mousePos) - new Vector3(.1f, 0, 10f);

            if (mousePosInWorld == transform.position)
            {
                transform.LookAt(new Vector3(0, 0, 0));
            }
        }
    }

    void Rotate()
    {
        Ray ray = mainCam.ScreenPointToRay(mousePos);

        Plane groundPlane = new Plane(Vector3.up, Vector3.up);

        if (groundPlane.Raycast(ray, out point))
        {
            Vector3 Point = ray.GetPoint(point);
            Vector3 CorrectedLookAT = new Vector3(Point.x, transform.position.y, Point.z);
            transform.LookAt(CorrectedLookAT);
        }
    }
}