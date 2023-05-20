using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestReStart : MonoBehaviour
{
    public void ReStart()
    {
        SceneManager.LoadScene(TestFaintCheck.SaveNowScene);
        Time.timeScale = 1;
    }
}
