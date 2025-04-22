using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RotationSpeed;
    public GameObject onCollectEffect;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Rotate(0, RotationSpeed, 0);
    
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }
    }
}
