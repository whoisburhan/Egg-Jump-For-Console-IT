using UnityEngine;

public class activatorScript : MonoBehaviour
{
    [Header("ACTIVATOR GAME OBJECT")]
    public GameObject actavatorGameObject;

    [Header("SPAWNING POINT")]
    public Transform spawnPoint;

    private float destroyTime = 100f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Destroy(Instantiate(actavatorGameObject, spawnPoint.position,spawnPoint.rotation),destroyTime);
        }
    }


}
