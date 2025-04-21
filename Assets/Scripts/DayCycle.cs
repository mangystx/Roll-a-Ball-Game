using UnityEngine;
using UnityEngine.AI;

public class DayCycle : MonoBehaviour
{
    private float _dayProgress;
    
    [Tooltip("In seconds")]
    public float dayLength = 30;
    public Light directionalLight;
    
    public NavMeshAgent[] enemies;
    public float enemyDaySpeed = 2.5f;
    public float enemyNightSpeed = 6f;
    
    private void Update()
    {
        _dayProgress += Time.deltaTime / dayLength;

        if (_dayProgress >= 1) _dayProgress = 0;
        
        UpdateLight();
        UpdateEnemySpeed();
    }
    
    private void UpdateLight()
    {
        const float totalRotation = 360f;
        
        var lightRotation = _dayProgress * totalRotation - 90f; 
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(lightRotation, -30f, 0f));
        
        if (lightRotation is >= 180f or < 0f)
        {
            var nightProgressNormalized = lightRotation >= 180f
                ? (lightRotation - 180f) / 180f 
                : (lightRotation + 180f) / 180f;
            
            directionalLight.intensity = Mathf.Lerp(1f, 0.2f, nightProgressNormalized);
        }
        else
        {
            var dayProgressNormalized = lightRotation / 180f;
            directionalLight.intensity = Mathf.Lerp(0.2f, 1f, dayProgressNormalized);
        }
    }
    
    private void UpdateEnemySpeed()
    {
        var lightRotation = _dayProgress * 360f - 90f;
        var isNight = lightRotation >= 180f || lightRotation < 0f;
        var speed = isNight ? enemyNightSpeed : enemyDaySpeed;

        foreach (var enemy in enemies)
        {
            if (enemy) enemy.speed = speed;
        }
    }
}
