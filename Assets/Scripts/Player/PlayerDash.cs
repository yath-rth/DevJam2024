using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerDash : MonoBehaviour
{
    PlayerStats playerStats;
    Animator am;
    Rigidbody rb;
    public float MinDashForce = 5, MaxDashForce = 15, Damage = 10, WaitTime = .5f, MinDamage = 5, MaxDamage = 20, TimeBtwDash = .5f;
    public float anti { get; private set; }
    [HideInInspector]
    public float dashForce = 0, timer = 1;
    [SerializeField] Vector3 size;
    public LayerMask EnemiesLayer;
    CinemachineVirtualCamera vrCam;
    PlayerMovement movementScipt;
    public bool CanDash = false;
    List<Transform> enemies;
    public AnimationCurve DashSpeed;
    Vector3 dashpos;
    PlayerInputActions inputActions;
    float canDashTime;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        playerStats = GetComponent<PlayerStats>();
        enemies = new List<Transform>(100);
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        movementScipt = GetComponent<PlayerMovement>();
        vrCam = GameObject.FindGameObjectWithTag("Virtual Cam").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        anti = 0;
        timer = 1;
        dashForce = MinDashForce;
        Damage = MinDamage;

        inputActions.Gameplay.QuickDash.performed += _ => QuickDash();
        inputActions.Gameplay.Dash.canceled += _ => StartCoroutine(dash());
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Debug.Log(inputActions.Gameplay.Dash.ReadValue<float>());

        if (!playerStats.IsDead)
        {
            {
                vrCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = anti;

                am.SetFloat("Anticipation", anti);

                if (inputActions.Gameplay.Dash.ReadValue<float>() > 0)
                {
                    ChargeUp();
                }
            }
        }
    }

    IEnumerator dash()
    {
        if (CanDash)
        {
            anti = 0;
            dashForce = MinDashForce;
            Damage = MinDamage;
            movementScipt.CanThouMove = true;
            timer = 0;
            am.SetTrigger("Attack");
            dashpos = transform.position + (transform.forward * dashForce);

            while (timer < 1)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, dashpos, DashSpeed.Evaluate(timer) * Time.deltaTime);
                yield return null;
            }
            CanDash = false;
            if (enemies.Count > 0) enemies.Clear();
        }

    }

    private void Attack()
    {
        Collider[] enemie = Physics.OverlapBox(transform.position + new Vector3(0, 1.5f, 0), size / 2, Quaternion.identity, EnemiesLayer);
        foreach (Collider enemy in enemie)
        {
            if (enemies.Contains(enemy.transform) == false)
            {
                Debug.Log(enemy.name);
                IStats enemyStats = enemy.GetComponent<IStats>();
                if (enemyStats != null)
                {
                    enemies.Add(enemy.transform);
                    enemyStats.TakeDamage(Damage);
                }
            }
        }
    }

    void ChargeUp()
    {
        if(inputActions.Gameplay.Dash.ReadValue<float>() > 0)
        {
            anti += 0.01f;
            Damage = Mathf.Clamp(Damage + .05f, MinDamage, MaxDamage);
            anti = Mathf.Clamp(anti, 0, 1);
            dashForce = Mathf.Lerp(MinDashForce, MaxDashForce, anti);
            if (anti >= 0.2f && anti <= 0.21f)
            {
                am.SetTrigger("Start Attack");
                CanDash = true;
            }
            movementScipt.CanThouMove = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 1.5f, 0), size);
    }

    void QuickDash(){
        CanDash = true;
        StartCoroutine(dash());
    }
}
