using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectEffect;
    
    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
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
