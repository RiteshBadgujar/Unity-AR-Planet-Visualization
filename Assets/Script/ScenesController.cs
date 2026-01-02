using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesController : MonoBehaviour
{
    public void SwitchScence(string scenceName) {

        SceneManager.LoadScene(scenceName);
    }
}
