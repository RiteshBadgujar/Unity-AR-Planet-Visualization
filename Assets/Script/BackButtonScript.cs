using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
