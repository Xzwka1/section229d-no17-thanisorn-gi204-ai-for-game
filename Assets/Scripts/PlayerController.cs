using UnityEngine;
using UnityEngine.UI; // จำเป็นต้องมีบรรทัดนี้เพื่อจัดการ UI เป้าเล็ง

public class PlayerController : MonoBehaviour
{
    [Header("การเคลื่อนที่ & หันหน้า")]
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    private float rotationX = 0f;
    public Camera playerCamera; // ลาก Main Camera มาใส่ช่องนี้

    [Header("ระบบเล็งและยิง")]
    public Image crosshair; // ลาก UI Crosshair มาใส่ช่องนี้
    public float attackRange = 10f; // ระยะหมัด/ระยะยิง
    public int score = 0; // ตัวแปรเก็บคะแนน

    void Start()
    {
        // ซ่อนเมาส์และล็อคเป้าไว้กลางหน้าจอเวลาเริ่มเกม
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- 1. ระบบหันหน้า (Mouse Look) ---
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // ล็อคไม่ให้ก้ม/เงยคอหักเกิน 90 องศา

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);

        // --- 2. ระบบเดินอิสระ (WASD) ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;

        // --- 3. ระบบเล็งเป้าและยิง ---
        AimAndShoot();
    }

    void AimAndShoot()
    {
        // ยิงเลเซอร์ล่องหนจากกึ่งกลางหน้าจอกล้องไปข้างหน้า
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // เช็คว่าเลเซอร์ชนอะไรในระยะที่กำหนดหรือไม่
        if (Physics.Raycast(ray, out hit, attackRange))
        {
            // ถ้าชนวัตถุที่มี Tag ชื่อว่า "Enemy" (ศัตรู)
            if (hit.collider.CompareTag("Enemy"))
            {
                crosshair.color = Color.red; // เปลี่ยนเป้าเป็นสีแดง

                // ถ้าคลิกเมาส์ซ้าย (0) ตอนที่เป้าเป็นสีแดง
                if (Input.GetMouseButtonDown(0))
                {
                    score += 1; // เพิ่มคะแนน
                    Debug.Log("โดนกล้วยแล้ว! คะแนนตอนนี้คือ: " + score);

                    // (เดี๋ยวเราจะเรียกคำสั่งให้กล้วยเจ็บ/วาร์ปหนี ตรงจุดนี้ในภายหลังครับ)
                }
            }
            else
            {
                crosshair.color = Color.green; // ถ้าชนกำแพงหรืออย่างอื่น ให้เป้ากลับเป็นสีเขียว
            }
        }
        else
        {
            crosshair.color = Color.green; // ถ้าไม่ชนอะไรเลย เป้าก็เป็นสีเขียว
        }
    }
}