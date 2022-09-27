using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
        RandomizePosition();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        _transform.position = new Vector2(Random.Range(-10, 11), Random.Range(-7, 8));
    }
}
