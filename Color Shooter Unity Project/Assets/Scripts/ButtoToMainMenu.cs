
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtoToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
