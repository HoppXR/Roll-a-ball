using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Timer timer;
    
    private Rigidbody rb;
    
    private int count;
    
    public float speed = 0; 
    
    public TextMeshProUGUI countText;
    
    public GameObject winTextObject;
    
    private float movementX;
    private float movementY;
    
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        timer = FindObjectOfType<Timer>();
        
        count = 0; 
        SetCountText();
        
        timer.StartTimer();
        
        winTextObject.SetActive(false);
    }
    
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed); 
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            
            count = count + 1;
            SetCountText();
        }
    }
    
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }
    
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        
        if (count >= 18)
        {
            winTextObject.SetActive(true);
            
            timer.StopTimer();
        }
    }
}
