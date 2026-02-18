using UnityEngine;
using UnityEngine.InputSystem;

public class particleSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private UnityEngine.ParticleSystem m_ParticleSystem;
    [SerializeField]
    private RaycastShooter RaycastShooter;
  
    
   

    public void spawnParticle()
    {
        var hit = RaycastShooter.rayHit;
        if (hit.collider != null && hit.collider.CompareTag("Brick") && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(m_ParticleSystem, hit.point, Quaternion.identity);
        }

    }

    void Update()
    {
        spawnParticle();
    }
}
