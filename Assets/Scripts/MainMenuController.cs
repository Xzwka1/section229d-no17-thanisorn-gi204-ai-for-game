using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    
    public void PlayGame()
    {
        
        SceneManager.LoadScene("GamePlay");
    }

    
    public void QuitGame()
    {
        Debug.Log("ออกจากเกมแล้ว!");
        Application.Quit(); 
    }
}