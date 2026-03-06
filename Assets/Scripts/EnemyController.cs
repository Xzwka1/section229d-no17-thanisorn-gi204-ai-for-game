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
        
        if (playerTransform != null)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

            
            transform.LookAt(targetPosition);

            
            transform.eulerAngles = new Vector3(-90f, transform.eulerAngles.y, 0f);

            
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