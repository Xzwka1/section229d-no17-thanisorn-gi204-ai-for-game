using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject[] spawnPoints;

    [Header("ระบบ AI เดินหาผู้เล่น")]
    public float moveSpeed = 3f; 
    private Transform playerTransform;

    void Start()
    {
        
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

       
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("หากล้วยไม่เจอผู้เล่นครับ! อย่าลืมตั้ง Tag ของ Player เป็น 'Player' นะครับ");
        }

        
        Respawn();
    }

    void Update()
    {
        // ถ้าเจอตัวผู้เล่น ให้กล้วยทำหน้าที่เดินเข้าหา
        if (playerTransform != null)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

            // 1. สั่งให้กล้วยหันหน้าไปหาผู้เล่น (มันจะก้มหน้าลง)
            transform.LookAt(targetPosition);

            // 2. ดัดหลังกล้วยให้ยืนตรง! (บังคับหมุนแกน X ขึ้นมา)
            // *** หมายเหตุ: ถ้าใส่ -90f แล้วกล้วยหงายหลัง ให้เปลี่ยนเป็น 90f หรือ 270f แทนนะครับ ***
            transform.eulerAngles = new Vector3(-90f, transform.eulerAngles.y, 0f);

            // 3. สั่งให้กล้วยเดินหน้าเข้าหาเป้าหมาย
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        if (playerTransform != null)
        {
            
            Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

            
            transform.LookAt(targetPosition);

            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void Respawn()
    {
        
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            transform.position = spawnPoints[randomIndex].transform.position;
        }
    }
}