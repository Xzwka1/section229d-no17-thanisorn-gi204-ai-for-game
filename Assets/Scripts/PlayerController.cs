using UnityEngine;
using UnityEngine.UI;
using TMPro; // เพิ่มบรรทัดนี้เข้ามา เพื่อให้รู้จัก TextMeshPro

public class PlayerController : MonoBehaviour
{
    [Header("การเคลื่อนที่ & หันหน้า")]
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    private float rotationX = 0f;
    public Camera playerCamera;

    [Header("ระบบเล็งและยิง")]
    public Image crosshair;
    public float attackRange = 10f;
    public int score = 0;

    [Header("ระบบ UI และเวลา")]
    public TextMeshProUGUI scoreText;       // เปลี่ยนเป็น TextMeshPro
    public TextMeshProUGUI timeText;        // เปลี่ยนเป็น TextMeshPro
    public float timeLimit = 60f;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;  // เปลี่ยนเป็น TextMeshPro

    private bool isGameOver = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver) return;

        // ระบบเวลา
        timeLimit -= Time.deltaTime;
        if (timeText != null)
        {
            timeText.text = "Time: " + Mathf.Ceil(timeLimit).ToString() + "s";
        }

        if (timeLimit <= 0)
        {
            GameOver();
        }

        // ระบบหันหน้า
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);

        // ระบบเดิน
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;

        AimAndShoot();
    }

    void AimAndShoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                crosshair.color = Color.red;

                if (Input.GetMouseButtonDown(0))
                {
                    score += 1;
                    UpdateScoreUI();

                    EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                    if (enemy != null)
                    {
                        enemy.Respawn();
                    }
                }
            }
            else
            {
                crosshair.color = Color.green;
            }
        }
        else
        {
            crosshair.color = Color.green;
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        timeLimit = 0;

        if (timeText != null) timeText.text = "Time: 0s";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null)
            {
                finalScoreText.text = "Your Score: " + score.ToString() + " Hits!";
            }
        }
    }
}