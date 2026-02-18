using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;


public class RaycastShooter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera rayCam;
    public RaycastHit rayHit;
    private ParticleSystem particle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.value;
        Ray screenRay = rayCam.ScreenPointToRay(mousePos);
       


        if (Physics.Raycast(screenRay, out RaycastHit hitInfo))
        {
            rayHit = hitInfo;
            Debug.DrawLine(screenRay.origin, hitInfo.point, Color.yellow);
            if (Mouse.current.leftButton.wasPressedThisFrame && hitInfo.collider.gameObject.CompareTag("Brick"))
            {
                Destroy(hitInfo.collider.gameObject);
                
            }
        }
    }

   
}
