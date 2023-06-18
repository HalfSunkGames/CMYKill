using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputActions playerControls;
    private InputAction move;
    private InputAction fire;
    private InputAction rollDice;

    private Rigidbody rb;
    [SerializeField] private float aceleration;
    [SerializeField] private float deceleration;
    private Vector3 moveDirection;

    private Animator anim;
    [SerializeField] private ParticleSystem changeParticleC;
    [SerializeField] private ParticleSystem changeParticleM;
    [SerializeField] private ParticleSystem changeParticleY;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rb.drag = deceleration;

        DiceController.Instance.RollDice();
        int attack = DiceController.Instance.currentValue;
        if (attack == 1 || attack == 6)
        {
            anim.SetBool("C", true);
            anim.SetBool("M", false);
            anim.SetBool("Y", false);
        }
        if (attack == 2 || attack == 5)
        {
            anim.SetBool("C", false);
            anim.SetBool("M", true);
            anim.SetBool("Y", false);
        }
        if (attack == 3 || attack == 4)
        {
            anim.SetBool("C", false);
            anim.SetBool("M", false);
            anim.SetBool("Y", true);
        }
    }

    private void OnEnable()
    {
        // Link the actions for the player controls
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire; // Register the event with the custom function

        rollDice = playerControls.Player.RollDice;
        rollDice.Enable();
        rollDice.performed += ChangeAttack;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        rollDice.Disable();
    }

    private void Update()
    {
        moveDirection = move.ReadValue<Vector3>().normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * aceleration * Time.deltaTime, ForceMode.Impulse);
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);                        // Posición del jugador en el viewport
        Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()); // Posición del puntero en el viewport
        Vector3 direction = mouseOnScreen - positionOnScreen;                                                   // Linea entre ambos
        //Debug.Log("Mouse: " + mouseOnScreen + "\t" + "Player: " + positionOnScreen);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;                            // Calcula el ángulo de la dirección
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));                                       // Gira al jugador a esa diercción
        Debug.Log(angle);
    }

    // The player fires!
    private void Fire(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1)
        {
            int attack = DiceController.Instance.currentValue;
            if (attack < 4)
                anim.SetTrigger("melee");
            else
                anim.SetTrigger("proj");
        }
    }


    public void ProjAttack()
    {
        int attack = DiceController.Instance.currentValue;

        if (DiceController.Instance.UseAttack(attack)) // Can the attack be used?
        {
            Debug.Log($"Used: {attack}");
            GetComponent<ShotChanger>().AttackSelector(attack);

        }
        else
        {
            Debug.Log($"Can't use: {attack}");
            AudioVFXController.Instance.PlayCantRoll();
        }

    }

    public void MeleeAttack()
    {
        int attack = DiceController.Instance.currentValue;

        if (DiceController.Instance.UseAttack(attack)) // Can the attack be used?
        {
            Debug.Log($"Used: {attack}");
            GetComponent<ShotChanger>().AttackSelector(attack);

        }
        else
        {
            Debug.Log($"Can't use: {attack}");
            AudioVFXController.Instance.PlayCantRoll();
        }
    }

    private void ChangeAttack(InputAction.CallbackContext context)
    {
        DiceController.Instance.RollDice();
        int attack = DiceController.Instance.currentValue;

        if (attack == 1 || attack == 6)
        {
            anim.SetBool("C", true);
            anim.SetBool("M", false);
            anim.SetBool("Y", false);
            changeParticleC.Play();
        }
        if (attack == 2 || attack == 5)
        {
            anim.SetBool("C", false);
            anim.SetBool("M", true);
            anim.SetBool("Y", false);
            changeParticleM.Play();
        }
        if (attack == 3 || attack == 4)
        {
            anim.SetBool("C", false);
            anim.SetBool("M", false);
            anim.SetBool("Y", true);
            changeParticleY.Play();
        }
    }

    public void DamageAnim()
    {
        anim.SetTrigger("damage");
        anim.SetTrigger("inv");
    }
}
