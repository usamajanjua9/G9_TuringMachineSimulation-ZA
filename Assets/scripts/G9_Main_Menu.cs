using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class G9_Main_Menu : MonoBehaviour
{
   
    public Button palindrome;
    public Button usama;
    public Button zohaib;
    public Button umer;
    // Start is called before the first frame update
    void Start()
    {
        umer.onClick.AddListener(um);
        palindrome.onClick.AddListener(p);
        zohaib.onClick.AddListener(z);
        usama.onClick.AddListener(u);

    }
   
    void u()
    {
        SceneManager.LoadScene(sceneName: "G9_L1_usamamenu");
    }
    void um()
    {
        SceneManager.LoadScene(sceneName: "G9_L3_umerload");
    }
    void z()
    {
        SceneManager.LoadScene(sceneName: "G9_L2_zohaibload");
    }
    void p()
    {
        SceneManager.LoadScene(sceneName: "G9_L4_loadpalindrome");
    }

    // Update is called once per frame
  
    void Update()
    {
     

    }
}
