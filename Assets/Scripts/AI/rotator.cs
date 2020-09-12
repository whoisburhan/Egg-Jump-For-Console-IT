using UnityEngine;

public class rotator : MonoBehaviour
{
    public float rotateSpeed = 90f;

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

}
