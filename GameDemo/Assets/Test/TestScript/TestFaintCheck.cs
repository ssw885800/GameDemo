using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class TestFaintCheck : MonoBehaviour
{
    bool FaintCheck;    //是否昏厥
    bool SlowDownCheck; //是否緩速
    bool m_IsInsideBeam = false; //是否在光線內
    Collider m_Collider = null;  
    public static string SaveNowScene;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
       FaintCheck = false;
       SlowDownCheck = false;
       m_Collider = GetComponent<Collider>();   //獲取自身碰撞體
       Debug.Assert(m_Collider);
    }

    // Update is called once per frame
    void Update()
    {  
        Debug.Log("是否緩速" + SlowDownCheck);
    }
    void Faint()        //昏厥
    {
        if (m_IsInsideBeam)
        {
            FaintCheck = true;
            Time.timeScale = 0;
            SaveNowScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("DeathScene", LoadSceneMode.Additive);
            Debug.Log("被強光照到了");
        }
        else Debug.Log("有遮擋物");
    }
    void OnSlowDown()         //進入緩速狀態
    {
        SlowDownCheck = true;
    }
    void ExitSlowDown()     //離開緩速狀態
    {
        SlowDownCheck = false;
    }


    void OnTriggerStay(Collider trigger)
    {
        var dynamicOcclusion = trigger.GetComponent<VLB.DynamicOcclusionRaycasting>();
        Debug.Log("在光照區域中");
        if (dynamicOcclusion)  //判定光線範圍內有無遮擋物，如果有則判定沒被光線照射到，如果無則會被判定被光線照射到
        {
            // This GameObject is inside the beam's TriggerZone.
            // Make sure it's not hidden by an occluder
            m_IsInsideBeam = !dynamicOcclusion.IsColliderHiddenByDynamicOccluder(m_Collider);
        } 
        else
        {
            m_IsInsideBeam = true;
        }
        if (trigger.gameObject.CompareTag("StrongLight"))//被強光檢測到時，觸發昏厥方法
        {
            Debug.Log("碰到標籤為StrongLight的物件");
            Faint();
        }
        if (trigger.gameObject.CompareTag("LowLight"))
        {
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
