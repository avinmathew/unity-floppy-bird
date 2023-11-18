using UnityEngine;

public class MiddlePipeTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GameManagerScript.instance.AddScore(1);
        }
    }
}
