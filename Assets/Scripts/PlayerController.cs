using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Timer _timer;

    private HighScoreBoard _highScoreBoard;
    
    private Rigidbody _rb;
    
    private int _count;
    private int _maxCount;
    public int score;
    
    public float speed = 0;
    [SerializeField] private float jumpForce;
    
    public TextMeshProUGUI countText;
    
    public GameObject winTextObject;
    
    private float _movementX;
    private float _movementY;

    [SerializeField] private Canvas canvas;
    
    public HighScoreBoard highScoreBoard;
    public TMP_Text playerScoreText;
    public TMP_InputField playerNameInput;
    public Button saveScoreButton;
    
    private bool _hasSaved = false;
    
    void Start()
    {
        // Components required for the start of the game
        
        _rb = GetComponent <Rigidbody>();
        _timer = FindObjectOfType<Timer>();
        
        _count = 0; 
        _maxCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
        SetCountText();
        
        _timer.StartTimer();
        
        winTextObject.SetActive(false);

        canvas.enabled = false;
            
        saveScoreButton.onClick.AddListener(OnSaveClicked);
        saveScoreButton.interactable = false;
        
        playerNameInput.onValueChanged.AddListener(OnNameChanged);
    }

    private void Update()
    {
        // Checks if game is over
        if (HasGameEnded())
        {
            GameOver();
        }

        // Player will jump if space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        playerScoreText.text = "Score: " + score;
    }

    private void FixedUpdate() 
    {
        // Prevents input when time is frozen
        if (Time.timeScale == 0)
        {
            return;
        }
        
        Vector3 movement = new Vector3 (_movementX, 0.0f, _movementY);
        
        _rb.AddForce(movement * speed); 
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            
            _count = _count + 1;
            SetCountText();
        }
    }
    
    // Basic player movement
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        _movementX = movementVector.x; 
        _movementY = movementVector.y; 
    }

    // Applies upwards force using jumpForce
    void Jump()
    {
        _rb.AddForce(0, jumpForce, 0);
    }
    
    // Updates the Count Text UI and will check if player has collected all the collectables
    void SetCountText() 
    {
        countText.text =  "Count: " + _count.ToString() + "/" + _maxCount;
        
        if (_count >= _maxCount)
        {
            winTextObject.SetActive(true);
            
            _timer.StopTimer();
        }
    }

    // Checks if game has ended
    public bool HasGameEnded()
    {
        return HasPlayerWon() || HasPlayerLost();
    }

    // Checks if the player has collected all the collectables
    bool HasPlayerWon()
    {
        return _count >= _maxCount;
    }

    // Checks if the player fell off the map
    bool HasPlayerLost()
    {
        return transform.position.y < -15f;
    }

    // Freezes game when over and calculates score, then enables the game end screen
    void GameOver()
    {
        Time.timeScale = 0;

        score = _count * 100;

        canvas.enabled = true;
    }
    
    // Saves the player data
    void OnSaveClicked()
    {
        _hasSaved = true;
        saveScoreButton.interactable = false;
        highScoreBoard.AddHighScore(playerNameInput.text, score, _timer.time);
    }

    // Save score can only be pressed when player has not saved or enters a character
    void OnNameChanged(string name)
    {
        saveScoreButton.interactable = !_hasSaved && name.Length > 0;
    }
}
