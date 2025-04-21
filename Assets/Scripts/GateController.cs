using System;
using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public Transform gateLeafPivot;
    public float openAngleForward = 90;
    public float openAngleBackward = -90;
    public float closeAngle;
    public float speed = 8;
    
    private Coroutine _crtCoroutine;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var targetAngle = DetermineOpenAngle(other.transform);
            
            if (_crtCoroutine != null) StopCoroutine(_crtCoroutine);
            _crtCoroutine = StartCoroutine(RotateGate(targetAngle));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_crtCoroutine != null) StopCoroutine(_crtCoroutine);
            _crtCoroutine = StartCoroutine(RotateGate(closeAngle));
        }
    }
    
    private IEnumerator RotateGate(float targetAngle)
    {
        while (Math.Abs(NormalizeAngle(gateLeafPivot.localRotation.eulerAngles.x) - targetAngle) > 0.01f)
        {
            var crtAngle = Mathf.MoveTowards(NormalizeAngle(gateLeafPivot.localRotation.eulerAngles.x), targetAngle, speed * Time.deltaTime);
            gateLeafPivot.localRotation = Quaternion.Euler(crtAngle, 0, 0);
            
            yield return null;
        }
    }
    
    private float DetermineOpenAngle(Transform playerTransform)
    {
        var directionToPlayer = playerTransform.position - transform.position;
        
        var dot = Vector3.Dot(transform.forward, directionToPlayer.normalized);
        
        return dot > 0 ? openAngleForward : openAngleBackward;
    }
    
    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        
        return angle;
    }
}