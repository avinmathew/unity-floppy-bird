using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public int moveSpeed = 3;

    private float _leftEdge;

    private void Start()
    {
        _leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1;
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < _leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
