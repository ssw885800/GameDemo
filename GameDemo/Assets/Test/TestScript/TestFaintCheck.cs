using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestFaintCheck : MonoBehaviour
{
    bool FaintCheck;    //是否昏厥
    bool SlowDownCheck; //是否緩速

    public static string SaveNowScene;

    // Start is called before the first frame update
    void Start()
    {
       FaintCheck = false;
       SlowDownCheck = false;
    }

    // Update is called once per frame
    void Update()
    {  
        Debug.Log("是否緩速" + SlowDownCheck);
    }
    void Faint()        //昏厥
    { 
        FaintCheck = true;
        Time.timeScale = 0;
        SaveNowScene =  SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("DeathScene",LoadSceneMode.Additive);
        Debug.Log("被強光照到了");
    }
    void OnSlowDown()         //進入緩速狀態
    {
        SlowDownCheck = true;
    }
    void ExitSlowDown()     //離開緩速狀態
    {
        SlowDownCheck = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StrongLight"))//被強光檢測到時，觸發昏厥方法
        {
            Debug.Log("碰到標籤為StrongLight的物件");
            Faint();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LowLight"))//當進到弱光照射區時，觸發進入緩速方法
        {
            Debug.Log("碰到標籤為LowLight的物件");
            OnSlowDown();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LowLight"))//當離開弱光照射區時，觸發離開緩速方法
        {
            ExitSlowDown();
        }
    }
}
