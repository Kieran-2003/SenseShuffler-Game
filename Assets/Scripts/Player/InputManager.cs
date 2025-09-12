using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [Header("Input Settings")]
    private InputActionMap playerMap;
    private InputActionMap uiMap;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction pauseAction;

    private Vector2 moveInput;

    [Header("Input References")]
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    private bool isPaused = false;


    private void Awake()
    {
        //  Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //  Assigning action maps
        playerMap = InputSystem.actions.FindActionMap("PLayer");
        uiMap = InputSystem.actions.FindActionMap("UI");

        //  Assigning actions to be acted upon
        moveAction = playerMap.FindAction("Move");
        interactAction = playerMap.FindAction("Interact");
        pauseAction = playerMap.FindAction("Pause");
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveInput * moveSpeed, ForceMode2D.Force);
    }
}
