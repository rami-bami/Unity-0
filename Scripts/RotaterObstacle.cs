using UnityEngine;

public class RotatorObstacle : MonoBehaviour
{
void Update()
    {
        transform.Rotate (new Vector3 (0, 0, 30) * Time.deltaTime);
    }
    
}