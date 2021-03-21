using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;
    Vector2 _startPosition ;
    Rigidbody2D _rigidBody2D; 
    SpriteRenderer _spriterenderer;
    void Awake() 
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriterenderer = GetComponent<SpriteRenderer>();    
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidBody2D.position;
        _rigidBody2D.isKinematic = true; //the object is under control of animation or script and not the physics
    }
    
    void OnMouseDown()
    {
        _spriterenderer.color = Color.red;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidBody2D.position;
        Vector2 direction = _startPosition - currentPosition; // ie 0 - (-2) =2 so when drag to left the vector direction is positive(right)
        direction.Normalize();

        _rigidBody2D.isKinematic = false;
        _rigidBody2D.AddForce(direction * _launchForce); // sends the bird to the direction

        _spriterenderer.color = Color.white;
    }

    void OnMouseDrag() 
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //method to find mouse position
        
        Vector2 desiredPosition = mousePosition;
        
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + direction * _maxDragDistance;
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        _rigidBody2D.position = desiredPosition;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
       StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds (3);
        _rigidBody2D.position = _startPosition;
        _rigidBody2D.isKinematic = true;
        _rigidBody2D.velocity = Vector2.zero;
    }
}
