using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Dash : MonoBehaviour
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
    public bool CanDash = false, IsDashing = false;
    public List<Transform> enemies;
    Vector3 dashpos;
    PlayerInputActions inputActions;

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

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

        inputActions.Gameplay.QuickDash.performed += _ => StartCoroutine(dash());
        inputActions.Gameplay.Dash.canceled += _ => StartCoroutine(dash());
    }

    void Update()
    {
        if (!playerStats.IsDead)
        {
            vrCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = anti;

            am.SetFloat("Anticipation", anti);

            ChargeUp();

            if (IsDashing)
            {
                Attack();
            }
        }
    }

    IEnumerator dash()
    {
        anti = 0;
        dashForce = MinDashForce;
        Damage = MinDamage;
        movementScipt.CanThouMove = true;
        am.SetTrigger("Attack");

        if (CanDash)
        {
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            CanDash = false;
            IsDashing = true;
        }

        yield return new WaitForSeconds(1);
        CanDash = true;
        IsDashing = false;
        if (enemies.Count > 0) enemies.Clear();
    }

    void ChargeUp()
    {
        int a = 0;

        if (inputActions.Gameplay.Dash.ReadValue<float>() > 0)
        {
            if (a == 0)
            {
                am.SetTrigger("Start Attack");
                a++;
            }

            anti += 0.01f;
            anti = Mathf.Clamp(anti, 0, 1);

            if (anti >= 0.2f && anti <= 0.21f)
            {
                CanDash = true;
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 1.5f, 0), size);
    }
}
