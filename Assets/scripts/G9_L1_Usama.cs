using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G9_L1_Usama : MonoBehaviour
{
    public AudioSource spacesound;
    public AudioSource win;
    public AudioSource loss;
    public InputField input;
    public Button Startbtn;
    public Button btnReset;
    public Button back;
    public Text Accept;
    public Text State;
    public Text Steps;
    float cubeposition = -3;
    float cubepositionleft = 0;
    int cubeIndex;    
    private string str = null;
    char[] word;
    Vector3 Position;
    int counter = 0;
    public Material black;
    private GameObject particle;
    public GameObject cube;
 //   Rigidbody m_Rigidbody;
   // Vector3 m_EulerAngleVelocity;

    private Simpleturing sturingmachine = new Simpleturing();
   


    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {

        back.onClick.AddListener(backs);

        btnReset.onClick.AddListener(OnRestartButtonClick);
        input.onValueChange.AddListener(delegate { keylistener(str); ; });
        Position = this.transform.position;

       // m_EulerAngleVelocity = new Vector3(0, 100, 0);
    }
    void FixedUpdate()
    {
    //    Quaternion ΔRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.ΔTime);
      //  m_Rigidbody.MoveRotation(m_Rigidbody.rotation * ΔRotation);
    }
    public void OnRestartButtonClick()
    {

        SceneManager.LoadScene("G9_L1_usama - simple"); 
     }
    public void backs()
    {

        SceneManager.LoadScene("G9_Main_Menu");
    }
    void SuccessMachine()
    {
        //The head is green
        particle = GameObject.Find("PointPlayer").gameObject;
        particle.GetComponent<Light>().color = new Color(0.0f, 1.0f, 0.0f, 1f);

    }
    void ErrorMachine()
    {
        //The head is red
        particle = GameObject.Find("PointPlayer").gameObject;
        particle.GetComponent<Light>().color = new Color(0.82f, 0.0f, 0.0f, 1f);

    }

    public void keylistener(string g)
    {
        if (Input.GetKey("a") || Input.GetKey("b"))
        {
            input.text = input.text;
            str = input.text;
        }
        
        
        else
        {
            input.text = str + "";
        }
         if (Input.GetKeyDown(KeyCode.Backspace))
        {
            str = "";
            input.text = "";
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
       
       
    }

    private void machineTM()
    {
        if (sturingmachine.current_state != SimpleStates.haltt)

        {
            if (sturingmachine.current_state != SimpleStates.q8 || sturingmachine.moveCurrent == SMovement.H)

            {
                counter = counter + 1;

                sturingmachine.run();
                sturingmachine.str[sturingmachine.positionCurrent] = sturingmachine.replaceChar;
                if (sturingmachine.position >= word.Length - 1)
                {
                    CubeGenerator();
                    cubeIndex = cubeIndex + 1;
                }               
                changeTapeCharacter();
                this.transform.position = new Vector3(Position.x + (sturingmachine.position - 1) * 2.0f, Position.y, Position.z);
                Vector3 camerMove = this.transform.position;

            }
        }
        State.text = "Current State = " + sturingmachine.current_state;
        Steps.text = "Steps = " + counter.ToString();
        if (sturingmachine.current_state == SimpleStates.q8 && sturingmachine.moveCurrent == SMovement.H)
        {
            sturingmachine.current_state = SimpleStates.haltt;
            Accept.color = Color.green;
            Accept.text = "String Accepted";
            SuccessMachine();
            win.Play();
        }     
        else if (sturingmachine.current_state != SimpleStates.q8 && sturingmachine.moveCurrent == SMovement.H)
        {
            sturingmachine.current_state = SimpleStates.haltt;
            Accept.color = Color.red;
            Accept.text = "String Rejected";
            ErrorMachine();
            loss.Play();
           
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
        cube.transform.position = new Vector3(Position.x-3+cubeposition , -9.5f, -35.09f);
        cube.name = "Cube" + cubeIndex.ToString();
        //cube.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
    private void CubeGenerator() 
    {
        CreateCube();
       // cube.AddComponent<Rigidbody>();
       //  m_Rigidbody = cube.GetComponent<Rigidbody>();
       // m_Rigidbody.useGravity = false;       
     //   cube.tag = "cube";       
        GameObject childObject = new GameObject("text");
        childObject.transform.parent = cube.transform;
        childObject.AddComponent<TextMesh>();
        childObject.GetComponent<TextMesh>().fontSize = 300;
        childObject.GetComponent<TextMesh>().characterSize = 0.1f;
        childObject.GetComponent<TextMesh>().color = Color.red;
        childObject.GetComponent<TextMesh>().transform.localScale = new Vector3(0.38694f, 0.38694f, 0.38694f);
        childObject.GetComponent<TextMesh>().transform.localPosition = new Vector3(-0.40f, 0.65f, -0.57f);
        childObject.GetComponent<TextMesh>().transform.Rotate(0, 0, 0);
      
        if (cubeIndex < word.Length-1)
        {
            childObject.GetComponent<TextMesh>().text = word[cubeIndex].ToString();
            cubeposition = cubeposition + 3f;
        }
       
        
        else
        {
            childObject.GetComponent<TextMesh>().text = 'Δ'.ToString();
            cubeposition = cubeposition + 3f;
        }
        //cubeposition = cubeposition + 2.0f;
        
        
    }

    private void changeTapeCharacter()
    {
            GameObject find = GameObject.Find("Cube" + sturingmachine.positionCurrent.ToString());
            find.GetComponentInChildren<TextMesh>().text = sturingmachine.replaceChar.ToString();       
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
        sturingmachine.str = blank;
        input.interactable = false;
        Startbtn.interactable=false;
        State.text = "Current State = q0";
        Steps.text = "Steps = 0";
    }

   
}
public enum SMovement
{ 
    L,R,H,S
}
public class Simpleturing
{
    public SimpleStates current_state = SimpleStates.q0;
    public char[] str;
    public int position = 2;
    public int positionCurrent;
    public char symbol = 'B';
    public char replaceChar;
    public SMovement moveCurrent;
    public Simpleturing()
    {

    }

    public void run()
    {

        if (current_state == SimpleStates.q0)
        {
            if (str[position] == 'a')
            {
                current_state = SimpleStates.q1;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q3;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q1)
        {
            if (str[position] == 'a')
            {
                current_state = SimpleStates.q1;
                moveCurrent = SMovement.R;
                replaceChar = 'a';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q1;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'b')
            {
                current_state = SimpleStates.q4;
                moveCurrent = SMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'c')
            {
                current_state = SimpleStates.q10;
                moveCurrent = SMovement.L;
                replaceChar = '-';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = SimpleStates.q5;
                moveCurrent = SMovement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q2)
        {
            if (str[position] == 'x')
            {
                current_state = SimpleStates.q2;
                moveCurrent = SMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = SimpleStates.q0;
                moveCurrent = SMovement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q2;
                moveCurrent = SMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'c')
            {
                current_state = SimpleStates.q2;
                moveCurrent = SMovement.L;
                replaceChar = 'c';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'b')
            {
                current_state = SimpleStates.q2;
                moveCurrent = SMovement.L;
                replaceChar = 'b';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q3)
        {
            if (str[position] == 'c')
            {
                current_state = SimpleStates.q3;
                moveCurrent = SMovement.R;
                replaceChar = 'c';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'b')
            {
                current_state = SimpleStates.q3;
                moveCurrent = SMovement.R;
                replaceChar = 'b';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '-')
            {
                current_state = SimpleStates.q15;
                moveCurrent = SMovement.L;
                replaceChar = '-';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q3;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = SimpleStates.q2;
                moveCurrent = SMovement.L;
                replaceChar = 'c';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == '-')
            {
                current_state = SimpleStates.q9;
                moveCurrent = SMovement.R;
                replaceChar = '-';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }
        }
        else if (current_state == SimpleStates.q4)
        {
            if (str[position] == 'y')
            {
                current_state = SimpleStates.q4;
                moveCurrent = SMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'a')
            {
                current_state = SimpleStates.q4;
                moveCurrent = SMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'x')
            {
                current_state = SimpleStates.q0;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q5)
        {

            if (str[position] == 'y')
            {
                current_state = SimpleStates.q6;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }
        }
        else if (current_state == SimpleStates.q6)
        {
            if (str[position] == 'Δ')
            {
                current_state = SimpleStates.q8;
                moveCurrent = SMovement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }

            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q7)
        {
            if (str[position] == 'a')
            {
                current_state = SimpleStates.q12;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }

            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q8)
        {


            current_state = SimpleStates.haltt;
            moveCurrent = SMovement.H;


        }
        else if (current_state == SimpleStates.q9)
        {

            if (str[position] == 'x')
            {
                current_state = SimpleStates.q18;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }

            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q10)
        {
            if (str[position] == 'x')
            {
                current_state = SimpleStates.q0;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q10;
                moveCurrent = SMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == '-')
            {
                current_state = SimpleStates.q10;
                moveCurrent = SMovement.L;
                replaceChar = '-';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'a')
            {
                current_state = SimpleStates.q11;
                moveCurrent = SMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }
        }
        else if (current_state == SimpleStates.q11)
        {
            if (str[position] == 'a')
            {
                current_state = SimpleStates.q11;
                moveCurrent = SMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'x')
            {
                current_state = SimpleStates.q7;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }

            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q12)
        {

            if (str[position] == '-')
            {
                current_state = SimpleStates.q12;
                moveCurrent = SMovement.R;
                replaceChar = '-';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q12;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'a')
            {
                current_state = SimpleStates.q12;
                moveCurrent = SMovement.R;
                replaceChar = 'a';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'c')
            {
                current_state = SimpleStates.q13;
                moveCurrent = SMovement.L;
                replaceChar = '-';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = SimpleStates.q8;
                moveCurrent = SMovement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }
        }

        else if (current_state == SimpleStates.q13)
        {

            if (str[position] == '-')
            {
                current_state = SimpleStates.q13;
                moveCurrent = SMovement.L;
                replaceChar = '-';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q13;
                moveCurrent = SMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'x')
            {
                current_state = SimpleStates.q14;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'a')
            {
                current_state = SimpleStates.q13;
                moveCurrent = SMovement.R;
                replaceChar = 'a';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q14)
        {

            if (str[position] == '-')
            {
                current_state = SimpleStates.q14;
                moveCurrent = SMovement.R;
                replaceChar = '-';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q14;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'c')
            {
                current_state = SimpleStates.q8;
                moveCurrent = SMovement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q15)
        {

            if (str[position] == '-')
            {
                current_state = SimpleStates.q15;
                moveCurrent = SMovement.L;
                replaceChar = '-';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q16;
                moveCurrent = SMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }
        }
        else if (current_state == SimpleStates.q16)
        {

            if (str[position] == 'x')
            {
                current_state = SimpleStates.q16;
                moveCurrent = SMovement.L;
                replaceChar = 'x';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q16;
                moveCurrent = SMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = SimpleStates.q9;
                moveCurrent = SMovement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'a')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = 'a';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }
        }
        else if (current_state == SimpleStates.q17)
        {

            if (str[position] == 'a')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = 'a';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'c')
            {
                current_state = SimpleStates.q8;
                moveCurrent = SMovement.H;
                replaceChar = 'c';
                positionCurrent = position;
                position = position + 1;
            }
           else if (str[position] == 'Δ')
            {
                current_state = SimpleStates.q8;
                moveCurrent = SMovement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'x')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '-')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = '-';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q18)
        {

            if (str[position] == 'a')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = 'a';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'x')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = SimpleStates.q19;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }
        else if (current_state == SimpleStates.q19)
        {


            if (str[position] == 'y')
            {
                current_state = SimpleStates.q17;
                moveCurrent = SMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = SimpleStates.haltt;
                moveCurrent = SMovement.H;
            }

        }

    }
}
    

public enum SimpleStates
{
    q0,
    q1,
    q2,
    q3,
    q4,
    q5,
    q6,
    q7,
    q8,
    q9,
    q10,
    q11,
    q12,
    q13,
    q14,
    q15,
    q16,
    q17,
    q18,
    q19,
    haltt
}