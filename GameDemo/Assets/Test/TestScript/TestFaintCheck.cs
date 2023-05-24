using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class TestFaintCheck : MonoBehaviour
{
    bool FaintCheck;    //�O�_����
    bool SlowDownCheck; //�O�_�w�t
    bool m_IsInsideBeam = false; //�O�_�b���u��
    Collider m_Collider = null;  
    public static string SaveNowScene;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
       FaintCheck = false;
       SlowDownCheck = false;
       m_Collider = GetComponent<Collider>();   //����ۨ��I����
       Debug.Assert(m_Collider);
    }

    // Update is called once per frame
    void Update()
    {  
        Debug.Log("�O�_�w�t" + SlowDownCheck);
    }
    void Faint()        //����
    {
        if (m_IsInsideBeam)
        {
            FaintCheck = true;
            Time.timeScale = 0;
            SaveNowScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("DeathScene", LoadSceneMode.Additive);
            Debug.Log("�Q�j���Ө�F");
        }
        else Debug.Log("���B�ת�");
    }
    void OnSlowDown()         //�i�J�w�t���A
    {
        SlowDownCheck = true;
    }
    void ExitSlowDown()     //���}�w�t���A
    {
        SlowDownCheck = false;
    }


    void OnTriggerStay(Collider trigger)
    {
        var dynamicOcclusion = trigger.GetComponent<VLB.DynamicOcclusionRaycasting>();
        Debug.Log("�b���Ӱϰ줤");
        if (dynamicOcclusion)  //�P�w���u�d�򤺦��L�B�ת��A�p�G���h�P�w�S�Q���u�Ӯg��A�p�G�L�h�|�Q�P�w�Q���u�Ӯg��
        {
            // This GameObject is inside the beam's TriggerZone.
            // Make sure it's not hidden by an occluder
            m_IsInsideBeam = !dynamicOcclusion.IsColliderHiddenByDynamicOccluder(m_Collider);
        } 
        else
        {
            m_IsInsideBeam = true;
        }
        if (trigger.gameObject.CompareTag("StrongLight"))//�Q�j���˴���ɡAĲ�o���֤�k
        {
            Debug.Log("�I����Ҭ�StrongLight������");
            Faint();
        }
        if (trigger.gameObject.CompareTag("LowLight"))
        {
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
