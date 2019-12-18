using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class G9_L1_Loadusamascene : MonoBehaviour
{
   
  
    // Start is called before the first frame update



    void Start()

    {
        StartCoroutine(c());

    }
    IEnumerator c()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(sceneName: "G9_L1_usama - simple");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
