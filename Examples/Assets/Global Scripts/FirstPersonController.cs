using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour, IPowered
{
    private const float REGULATED_VELOCITY = -2f;
    private const string    INPUT_MOVE_X = "Horizontal", 
                            INPUT_MOVE_Z = "Vertical",
                            INPUT_JUMP = "Jump";

    [Header("Habilidades para las físicas")]
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "En este script actualmente hay que definir las habilidades 'Movement', 'Gravity' y 'Jump'.";
#endif
    [SerializeField] private Ability[] abilities;
    
    [Header("Detección del suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundSeparation = 0.3f;

    private CharacterController controller;
    private Vector3 velocity = Vector3.zero;

    #region Class Logic
    private IPowerUp GetPowerUp(string abilityType)
    {
        IPowerUp ability = Array.Find(abilities, abillitiesArray => abillitiesArray.Type == abilityType);
        return ability;
    }

    private float GetAbilityValue(string abilityType)
    {
        Ability ability = Array.Find(abilities, abillitiesArray => abillitiesArray.Type == abilityType);
        if(ability == null) return 0;

        return ability.Value;
    }

    private bool IsOnGround() => Physics.CheckSphere(groundCheck.position, groundSeparation, groundMask);
    private void GetBackToFloor()
    {
        if (IsOnGround() && velocity.y < Vector3.zero.y)
        {
            velocity.y = REGULATED_VELOCITY;
        }
    }

    private Vector3 InputMovement(float movementX, float movementZ) => transform.right * movementX + transform.forward * movementZ;

    private Vector3 GetGravity()
    {
        velocity.y += GetAbilityValue("Gravity") * Time.deltaTime;
        return velocity * Time.deltaTime;
    }

    private void CheckMovement()
    {
        controller.Move(InputMovement(Input.GetAxis(INPUT_MOVE_X), Input.GetAxis(INPUT_MOVE_Z)) * GetAbilityValue("Movement") * Time.deltaTime);
        controller.Move(GetGravity());
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown(INPUT_JUMP) && IsOnGround())
        {
            velocity.y = Mathf.Sqrt(GetAbilityValue("Jump") * REGULATED_VELOCITY * GetAbilityValue("Gravity"));
        }
    }
    #endregion

    #region MonoBehaviour API
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetBackToFloor();
        CheckMovement();
        CheckJump();
    }

    public IPowerUp ChangePowerState(string powerUpName)  => GetPowerUp(powerUpName); 
    #endregion
}
