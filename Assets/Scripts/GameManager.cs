using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Ball ball;
    [SerializeField] private ProjecttilePath projecttilePath;
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private Transform basketParent;
    [SerializeField] private GameOverDetector gameOverDetector;
    [SerializeField] private GameObject tutorialObject;
    [SerializeField] private GameObject dragTextObject;

    private Camera mainCamera;
    private Animator anim;

    private bool isDragging = false;
    private bool isDragArea = false;

    private bool isGrounded;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }

        set
        {
            isGrounded = value;
        }
    }

    Vector2 startPos;
    Vector2 endPos;
    Vector2 direction;
    Vector2 ballForce;
    float disatce;
    float forceOffset;

    Vector2 camPos;
    Vector2 gameOverLinePos;
    public float cameraOffset;

    private bool isGameOver = false;

    public List<GameObject> basketList = new List<GameObject>();
    int index = 0;
    float basketOffsetX;
    public float basketOffsetY;


    // Properties for Reset Game.

    private Vector2 ballStartPos;
    private Vector3 cameraStatPos;
    private Vector2 gameOverLineStartPos;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        index = 0;
        isGameOver = false;
        cameraOffset = (camPos - ball.GetBallPos()).y;
        forceOffset = 5f;
        mainCamera = Camera.main;
        ball.ActivateRb();

        UpdateBasket();
        SetDefaultGamePos();
        anim = tutorialObject.GetComponent<Animator>();
    }


    private void Update()
    {
        if (isGameOver)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            isGrounded = false;
            ball.DeactivateRb();
            OnDragStart();
        }
        if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if(isDragging)
        {
            OnDrag();
        }

        if(isGrounded)
        {
            ball.DeactivateRb();
        }
    }

    // Starting the mouse drag.
    private void OnDragStart()
    {
        isDragArea = false;
        ball.DeactivateRb();
        startPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Start Pos :: "+startPos);
        Debug.Log("ball.GetBallPos().y :: " + ball.GetBallPos().y);
        if (startPos.y < (ball.GetBallPos().y))
        {
            isDragArea = true;
            isDragging = true;
            projecttilePath.Show();
        }
        tutorialObject.SetActive(false);
        dragTextObject.SetActive(false);
    }


    // Continue the drag
    private void OnDrag()
    {
        endPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = (startPos - endPos).normalized;
        disatce = Vector2.Distance(startPos,endPos);

        //Calculating force applyimg to the ball.
        ballForce = disatce * direction * forceOffset;

        projecttilePath.UpdatePoints(ball.GetBallPos(), ballForce);
    }


    //End of the mousedarg.
    // appling force once mouse drag end.
    private void OnDragEnd()
    {
        if (isDragArea)
        {
            ball.ActivateRb();
            ball.AddForce(ballForce);
            projecttilePath.Hide();
        }
    }

    private void LateUpdate()
    {
        if (isGameOver)
            return;
        camPos.y = ball.GetBallPos().y + cameraOffset;
        Vector3 smoothPos = Vector3.Lerp(mainCamera.transform.position, camPos, 0.05f);
        smoothPos.z = -10f;
        mainCamera.transform.position = smoothPos;

        
    }

    public void GameOver()
    {
        isGameOver = true;
        ResetBasket();
        ScoreHandler.instance.OnGameOver();
    }

    public void UpdateBasket()
    {
        Debug.Log("Indexxxx :: "+index);
        Vector2 basketPos = new Vector2(basketList[index].transform.position.x, (ball.GetBallPos().y + basketOffsetY));
        basketList[index].GetComponentInChildren<Basket>().UpdateBasket(basketPos);

        if (index == 0)
        {
            index = 1;
        }
        else
        {
            index = 0;
        }
    }


    private void ResetBasket()
    {
        for (int i = 0; i < basketList.Count; i++)
        {
            basketList[i].GetComponentInChildren<Basket>().DisableBasket();
        }
    }

    public void OnReplay()
    {
        ResetBasket();
        index = 0;
        isGameOver = false;
        mainCamera.transform.position = cameraStatPos;
        ball.SetBallPos(ballStartPos);
        gameOverDetector.SetPosition(gameOverLineStartPos);
        UpdateBasket();
    }

    public void UpdateGameOverLine()
    {
        gameOverLinePos = new Vector2(gameOverDetector.GetPosition().x, (ball.GetBallPos().y - basketOffsetY*2));
        gameOverDetector.SetPosition(gameOverLinePos);
    }

    
    private void SetDefaultGamePos()
    {
        ballStartPos = ball.GetBallPos();
        cameraStatPos = mainCamera.transform.position;
        cameraStatPos.z = -10f;
        gameOverLineStartPos = gameOverDetector.GetPosition();

    }
}
