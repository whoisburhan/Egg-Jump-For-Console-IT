using UnityEngine;

public class birdScripts : MonoBehaviour
{
    private float speed = .5f;

    private void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);
    }

}
