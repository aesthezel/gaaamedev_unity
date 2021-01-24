using UnityEngine;

public class PrefabPowerUp : MonoBehaviour
{
    [SerializeField] private string powerUpType;
    [SerializeField] private float changeValue;
    [SerializeField] private int timeEffectMilliseconds;
    private IPowered receiver;

    #region Class Logic
    #endregion

    #region MonoBehaviour API
    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.GetComponent<IPowered>() != null)
        {
            receiver = other.gameObject.GetComponent<IPowered>();
            Debug.Log("Entré aquí");

            IPowerUp powerUp = receiver.ChangePowerState(powerUpType);

            powerUp.ChangeValueDueSeconds(changeValue, timeEffectMilliseconds);
            
            Destroy(this.gameObject);
        }
    }
    #endregion
}
