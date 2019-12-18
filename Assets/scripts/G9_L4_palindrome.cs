using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G9_L4_palindrome : MonoBehaviour
{
    public GameObject cube;
    public AudioSource spacesound;
    public InputField input;
    public Button Startbtn;
    public Button back;
    public Text Accept;
    public Text State;
    public Text Steps;
    float cubeposition = 0;
    float cubepositionleft = 0;
    int cubeIndex;    
    private string str = null;
    char[] word;
    Vector3 Position;
    int counter = 0;
    public Button reset;
   


    private turing turingmachine = new turing();

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        back.onClick.AddListener(backs);
        input.onValueChange.AddListener(delegate { keylistener(str); ; });
        Position = this.transform.position;
        reset.onClick.AddListener(Restart);
    }

    private void Restart()
    {

        SceneManager.LoadScene("G9_L4_palindrome");
    }
    public void backs()
    {

        SceneManager.LoadScene("G9_Main_Menu");
    }
    public void keylistener(string g)
    {
        if (Input.GetKey("0") || Input.GetKey("1"))
        {
            input.text = input.text;
            str = input.text;
        }
        
        else if (Input.GetKeyDown(KeyCode.Backspace))
        { str = "";
            input.text = "";
        }
        else
        {
            input.text = str + "";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            machineTM();
            spacesound.Play();
        }
        if (Input.GetKey("up"))
            transform.Translate(0.0f, 0.0f, 0.20f);

        if (Input.GetKey("down"))
            transform.Translate(0.0f, 0.0f, -0.20f);
       // if (Input.GetKeyDown(KeyCode.Space))
       // { //particle = GameObject.Find(element).gameObject;
          // particle.GetComponent<AudioListener>().enabled = true;
       // }
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           
        }
    }

    private void machineTM()
    {
        if (turingmachine.current_state != uStates.halt)

        {
            if (turingmachine.current_state != uStates.q5 || turingmachine.moveCurrent == Movement.H)

            {
                counter = counter + 1;

                turingmachine.run();
                turingmachine.str[turingmachine.positionCurrent] = turingmachine.replaceChar;
                if (turingmachine.position >= word.Length - 1)
                {
                    CubeGenerator();
                    cubeIndex = cubeIndex + 1;
                }               
                changeTapeCharacter();
                this.transform.position = new Vector3(Position.x + (turingmachine.position - 1) * 2.0f, Position.y, Position.z);
                Vector3 camerMove = this.transform.position;

            }
        }
        State.text = "Current State: " + turingmachine.current_state;
        Steps.text = "Steps = " + counter.ToString();
        if (turingmachine.current_state == uStates.q5 && turingmachine.moveCurrent == Movement.H)
        {
            turingmachine.current_state = uStates.halt;
            Accept.color = Color.green;
            Accept.text = "String Accepted";
        }
        else if (turingmachine.current_state != uStates.q5 && turingmachine.moveCurrent == Movement.H)
        {
            turingmachine.current_state = uStates.halt;
            Accept.color = Color.red;
            Accept.text = "String Rejected";
           
        }
    }

    private void diplayInput()
    {        
        for (cubeIndex = 0; cubeIndex < word.Length; cubeIndex++)
        {
            CubeGenerator();            
        }
    }
    void CreateCube()
    {
        cube = Instantiate(GameObject.FindGameObjectWithTag("cube")) as GameObject;
        cube.transform.position = new Vector3(Position.x - 2 + cubeposition, 0, 0);
        
        cube.name = "Cube" + cubeIndex.ToString();
    }
    private void CubeGenerator() 
    {
        CreateCube();
        
       
        

        GameObject childObject = new GameObject("text");
        childObject.transform.parent = cube.transform;
        childObject.AddComponent<TextMesh>();
        childObject.GetComponent<TextMesh>().fontSize = 300;
        childObject.GetComponent<TextMesh>().characterSize = 0.1f;
        childObject.GetComponent<TextMesh>().color = Color.red;
        childObject.GetComponent<TextMesh>().transform.localScale = new Vector3(0.38694f, 0.38694f, 0.38694f);
        childObject.GetComponent<TextMesh>().transform.localPosition = new Vector3(-0.354f, 0.62f, 0.62f);
        childObject.GetComponent<TextMesh>().transform.Rotate(90, 0, 0);
      
        if (cubeIndex < word.Length-1)
        {
            childObject.GetComponent<TextMesh>().text = word[cubeIndex].ToString();
            cubeposition = cubeposition + 2.0f;
        }
        else if (turingmachine.position <= 1)
        {
            childObject.GetComponent<TextMesh>().text = 'Δ'.ToString();          
            cubepositionleft = cubepositionleft - 2.0f;
        }
        else
        {
            childObject.GetComponent<TextMesh>().text = 'Δ'.ToString();
            cubeposition = cubeposition + 2.0f;
        }
        //cubeposition = cubeposition + 2.0f;
        
        
    }

    private void changeTapeCharacter()
    {
            GameObject find = GameObject.Find("Cube" + turingmachine.positionCurrent.ToString());
            find.GetComponentInChildren<TextMesh>().text = turingmachine.replaceChar.ToString();       
    }


    public void SaveString()
    {
        char[] blank = new char[1000];
        for (int i = 0; i < 1000; i++)
        {
            blank[i] = 'Δ';
        }
        str = "ΔΔ" + str + "ΔΔ";
        word = str.ToCharArray();
        int i2=0;
        foreach (var item in word)
        {
            blank[i2] = item;
            i2 = i2 + 1;
        }
        diplayInput();
        turingmachine.str = blank;
        input.gameObject.SetActive(false);
        Startbtn.gameObject.SetActive(false);
        State.text = "Current State = q0";
        Steps.text = "Steps = 0";
    }

   
}
public enum Movement
{ 
    L,R,H,S
}