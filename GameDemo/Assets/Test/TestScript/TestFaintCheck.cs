using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestFaintCheck : MonoBehaviour
{
    bool FaintCheck;    //�O�_����
    bool SlowDownCheck; //�O�_�w�t

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
        Debug.Log("�O�_�w�t" + SlowDownCheck);
    }
    void Faint()        //����
    { 
        FaintCheck = true;
        Time.timeScale = 0;
        SaveNowScene =  SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("DeathScene",LoadSceneMode.Additive);
        Debug.Log("�Q�j���Ө�F");
    }
    void OnSlowDown()         //�i�J�w�t���A
    {
        SlowDownCheck = true;
    }
    void ExitSlowDown()     //���}�w�t���A
    {
        SlowDownCheck = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StrongLight"))//�Q�j���˴���ɡAĲ�o���֤�k
        {
            Debug.Log("�I����Ҭ�StrongLight������");
            Faint();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LowLight"))//��i��z���Ӯg�ϮɡAĲ�o�i�J�w�t��k
        {
            Debug.Log("�I����Ҭ�LowLight������");
            OnSlowDown();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LowLight"))//�����}�z���Ӯg�ϮɡAĲ�o���}�w�t��k
        {
            ExitSlowDown();
        }
    }
}
