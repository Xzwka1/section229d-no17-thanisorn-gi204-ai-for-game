using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject[] spawnPoints;

    void Start()
    {
        
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        
        Respawn();
    }

    public void Respawn()
    {
        
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            transform.position = spawnPoints[randomIndex].transform.position;
        }
        else
        {
            
            Debug.LogWarning("หา SpawnPoint ไม่เจอครับ! อย่าลืมใส่ Tag ให้จุดเกิดด้วยนะครับ");
        }
    }
}