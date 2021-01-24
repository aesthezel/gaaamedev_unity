using UnityEngine;

public class Billboarding : MonoBehaviour
{

    [SerializeField] private Camera gameCamera;
    [SerializeField] private bool billboardActive = false, autoStart = false;
    
    #region Class Logic
    #endregion

    #region MonoBehaviour API
    private void Awake()
    {
        if (autoStart == true)
        {
            gameCamera = Camera.main;
            billboardActive = true;
        }
    }

    private void LateUpdate()
    {
        if (billboardActive == true)
        {
            transform.LookAt(transform.position - gameCamera.transform.rotation * Vector3.back, gameCamera.transform.rotation * Vector3.up);
        }
    }
    #endregion
}
