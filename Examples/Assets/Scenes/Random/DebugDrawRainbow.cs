using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DebugDrawRainbow : MonoBehaviour
{
    [SerializeField] Racer racers;
    [SerializeField] private float offset = 3f;
    [SerializeField] private Transform[] racersPositions = new Transform[3];

    private Vector2 racerMovement = Vector2.zero;

    private const string    AXIS_HORIZONTAL = "Horizontal", 
                            AXIS_VERTICAL = "Vertical";

    private Camera cam;

    private void Awake() => cam = this.GetComponent<Camera>();

    private void DrawLines()
    {
        Vector3 cameraPositionOnScreen = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, cam.nearClipPlane));
        Vector3 finalLine = new Vector3(cameraPositionOnScreen.x - offset, cameraPositionOnScreen.y, cameraPositionOnScreen.z);
        
        //Debug.Log($"¡La cámara está a {cameraPositionOnScreen.x} de ancho y de alto {cameraPositionOnScreen.y}!");
        
        foreach (var racer in racersPositions)
        {
            Debug.DrawLine(racer.position, new Vector3(racer.position.x - 5f, racer.position.y, 0), racers.ChangeColor(new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), 1f)), 0.1f);
        }
        
    }

    private void LateUpdate() 
    {
        racerMovement.Set(Input.GetAxisRaw(AXIS_HORIZONTAL), Input.GetAxisRaw(AXIS_VERTICAL));

        if(racerMovement != Vector2.zero)
        {
            for (int r = 0; r < racersPositions.Length; r++)
            {
                racersPositions[r].Translate(racerMovement.normalized * racers.RacerSpeed * Time.deltaTime);
            }
            
            DrawLines();
        }
    }
}
