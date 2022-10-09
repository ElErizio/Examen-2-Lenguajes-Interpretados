using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level1-1");
    }
}
