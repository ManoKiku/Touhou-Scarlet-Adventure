using UnityEngine;

public class InfinityRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 30;

    private void Update () {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}