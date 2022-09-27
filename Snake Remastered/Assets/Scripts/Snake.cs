using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private Vector2 _direction;
    private Transform _transform;
    private List<Transform> _segments;
    public int initialSize = 5;
    public Transform snakeSegment;
    
    private bool allowTurning;  
    // bug fix -> when the snake changes direction via input, further turning is forbidden
    // until the snake moves in that direction, and only after that can you change direction again.
    // this gets rid of the thing where while moving left you quickly press down then right
    // before moving down and just loose the game beacause your clicking was too quick
    // and you turned into yourself

    public int GetSnakeLength() { return _segments.Count; }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        ResetState();
    }

    void Update()
    {
        if (allowTurning)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down) 
                { _direction = Vector2.up; allowTurning = false; }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right) 
                { _direction = Vector2.left; allowTurning = false; }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left) 
                { _direction = Vector2.right; allowTurning = false; }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up) 
                { _direction = Vector2.down; allowTurning = false; }
        }
        
    }
    private void FixedUpdate()
    {
        SetHeadRotation();
        Move();
    }

    private void SetHeadRotation()
    {
        Vector3 headRotation;
        if (_direction == Vector2.up) { headRotation = new Vector3(0f, 0f, 0f); }
        else if (_direction == Vector2.left) { headRotation = new Vector3(0f, 0f, 90f); }
        else if (_direction == Vector2.down) { headRotation = new Vector3(0f, 0f, 180f); }
        else { headRotation = new Vector3(0f, 0f, 270f); }
        _transform.eulerAngles = headRotation;
    }
    private void Move()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        _segments[0].position = _transform.position;

        float newX = _transform.position.x + _direction.x;
        float newY = _transform.position.y + _direction.y;
        if(newX > 10) { newX = -10; }
        else if(newX < -10) { newX = 10; }
        if (newY > 7) { newY = -7; }
        else if (newY < -7) { newY = 7; }
        _transform.position = new Vector3(newX, newY, 0f);

        allowTurning = true;
    }
    private void ResetState()
    {
        _direction = Vector2.up;
        if(_segments != null) { 
            for(int i = _segments.Count - 1; i >= 0; i--)
            {
                Destroy(_segments[i].gameObject);
            }
            _segments.Clear(); 
        }
        else { 
            _segments = new List<Transform>(); 
        }
        allowTurning = true;
        for (int i = 0; i < initialSize; i++)
        {
            Grow();
        }
        _transform.position = new Vector2(0f, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Apple")
        {
            Grow();
        }
        else
        {
            ResetState();
        }
    }
    public void Grow()
    {
        float newX, newY;
        if (_segments.Count > 0)
        {
            newX = _segments[_segments.Count - 1].position.x;
            newY = _segments[_segments.Count - 1].position.y;
        }
        else
        {
            newX = newY = 0f;
        }
        snakeSegment.position = new Vector2(newX, newY);
        _segments.Add(Instantiate(snakeSegment));
    }
}
