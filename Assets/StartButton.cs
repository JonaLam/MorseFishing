using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnButtonPress() 
    {
        SceneManager.LoadScene("GameScene");
    }
}
