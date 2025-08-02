using UnityEngine;

public class Bat : MonoBehaviour
{
    [Header("VFX")] 
    [SerializeField] private GameObject deathVFX;
    public void Die()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
