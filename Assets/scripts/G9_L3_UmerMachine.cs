using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G9_L3_UmerMachine : MonoBehaviour
{

    public InputField input;
    public Button btn;
    public Text message;
    public Text state;
    public AudioSource spacesound;
    public Button btnReset;
    public Text steps;
    public Button back;
    public Material green;
    float cubepos = 0;
    int cubeIndex;
    private string str = null;
    char[] word;
    Vector3 Position;
    int counter = 0;
   
   


    private Umerturing Umerturingmachine = new Umerturing();

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        back.onClick.AddListener(backs);
        btnReset.onClick.AddListener(restart);
        input.onValueChange.AddListener(delegate { keylistener(str); ; });
        Position = this.transform.position;
    }

    private void restart()
    {
        SceneManager.LoadScene("G9_L3_umer");
    }
    public void backs()
    {

        SceneManager.LoadScene("G9_Main_Menu");
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
        if (Input.GetKeyDown(KeyCode.Space))
        { //particle = GameObject.Find(element).gameObject;
          // particle.GetComponent<AudioListener>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        { input.text = ""; }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           
        }
    }

    private void machineTM()
    {
        if (Umerturingmachine.current_state != States.halt)
        {
            if (Umerturingmachine.current_state != States.q7 || Umerturingmachine.moveCurrent == UmerMovement.H)
            {
                counter = counter + 1;
               
                Umerturingmachine.run();
                Umerturingmachine.str[Umerturingmachine.positionCurrent] = Umerturingmachine.replaceChar;
                if (Umerturingmachine.position>=word.Length-1)
                {                    
                    generateCube();
                    cubeIndex = cubeIndex + 1;
                }
                changeTapeCharacter();
                this.transform.position = new Vector3(Position.x + (Umerturingmachine.position-1) * 2.0f, Position.y, Position.z);
                Vector3 camerMove = this.transform.position;
            }
        }
        state.text = "current running q State: " + Umerturingmachine.current_state;
        steps.text = "Total Step count: " + counter.ToString();
        if (Umerturingmachine.current_state == States.q7 && Umerturingmachine.moveCurrent == UmerMovement.H)
        {
            Umerturingmachine.current_state = States.halt;
            message.color = Color.magenta;
            message.text = "Congratulation String You Entered is Accepted";
        }    
        else if (Umerturingmachine.current_state != States.q7 && Umerturingmachine.moveCurrent == UmerMovement.H)
        {
            Umerturingmachine.current_state = States.halt;
            message.color = Color.cyan;
            message.text = "String You Have Entered is Rejected";
        }
    }

    private void diplayInput()
    {        
        for (cubeIndex = 0; cubeIndex < word.Length; cubeIndex++)
        {
            generateCube();
            
        }
    }

    private void generateCube() 
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(Position.x - 2 + cubepos, 0, 0);
        cube.name = "cube" + cubeIndex.ToString();
        cube.AddComponent<Renderer>();
        cube.GetComponent<Renderer>().material = green;
        GameObject childObject = new GameObject("text");
        childObject.transform.parent = cube.transform;
        childObject.AddComponent<TextMesh>();
        childObject.GetComponent<TextMesh>().fontSize = 300;
        childObject.GetComponent<TextMesh>().characterSize = 0.1f;
        childObject.GetComponent<TextMesh>().color = Color.red;
        childObject.GetComponent<TextMesh>().transform.localScale = new Vector3(0.38694f, 0.38694f, 0.38694f);
        childObject.GetComponent<TextMesh>().transform.localPosition = new Vector3(-0.354f, 0.62f, 0.62f);
        childObject.GetComponent<TextMesh>().transform.Rotate(90, 0, 0);
        if (cubeIndex<word.Length)
        {
            childObject.GetComponent<TextMesh>().text = word[cubeIndex].ToString();   
        }
        else
            childObject.GetComponent<TextMesh>().text = 'Δ'.ToString();
        cubepos = cubepos + 2.0f;
    }

    private void changeTapeCharacter()
    {
            GameObject find = GameObject.Find("cube" + Umerturingmachine.positionCurrent.ToString());
            find.GetComponentInChildren<TextMesh>().text = Umerturingmachine.replaceChar.ToString();       
    }


    public void saveString()
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
        Umerturingmachine.str = blank;
        input.gameObject.SetActive(false);
        btn.gameObject.SetActive(false);
        state.text = "Current State: q0";
        steps.text = "Counter: 0";
    }

   
}
public enum UmerMovement
{ 
    L,R,H,S
}
public class Umerturing
{
    public States current_state = States.q0;
    public char[] str;
    public int position = 2;
    public int positionCurrent;
    public char symbol = 'B';
    public char replaceChar;
    public UmerMovement moveCurrent;
    public Umerturing()
    {

    }

    public void run()
    {

        if (current_state == States.q0)
        {
            if (str[position] == 'a')
            {
                current_state = States.q1; //jiski trf jana hy 
                moveCurrent = UmerMovement.R;
                replaceChar = 'o';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'z')
            {
                current_state = States.q0;
                moveCurrent = UmerMovement.R;
                replaceChar = 'z';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'v')
            {
                current_state = States.q7;
                moveCurrent = UmerMovement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'o')
            {
                current_state = States.q0;
                moveCurrent = UmerMovement.R;
                replaceChar = 'o';
                positionCurrent = position;
                position = position + 1;
            }

            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }

        }
        else if (current_state == States.q1)
        {
            if (str[position] == 'b')
            {
                current_state = States.q2;
                moveCurrent = UmerMovement.L;
                replaceChar = 'i';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'a')
            {
                current_state = States.q1;
                moveCurrent = UmerMovement.R;
                replaceChar = 'a';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'i')
            {
                current_state = States.q1;
                moveCurrent = UmerMovement.R;
                replaceChar = 'i';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'z')
            {
                current_state = States.q2;
                moveCurrent = UmerMovement.L;
                replaceChar = 'i';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = States.q4;
                moveCurrent = UmerMovement.L;
                replaceChar = 'v';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'v')
            {
                current_state = States.q6;
                moveCurrent = UmerMovement.L;
                replaceChar = 'v';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }


        }
        else if (current_state == States.q2)
        {
            if (str[position] == 'i')
            {
                current_state = States.q2;
                moveCurrent = UmerMovement.L;
                replaceChar = 'i';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'o')
            {
                current_state = States.q8;
                moveCurrent = UmerMovement.L;
                replaceChar = 'o';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'a')
            {
                current_state = States.q3;
                moveCurrent = UmerMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }

        }
        else if (current_state == States.q3)
        {
            if (str[position] == 'a')
            {
                current_state = States.q3;
                moveCurrent = UmerMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'o')
            {
                current_state = States.q0;
                moveCurrent = UmerMovement.R;
                replaceChar = 'o';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }
        }
        else if (current_state == States.q4)
        {
            {
                if (str[position] == 'a')
                {
                    current_state = States.q4;
                    moveCurrent = UmerMovement.H;
                    replaceChar = 'a';
                    positionCurrent = position;
                    position = position - 1;
                }
                else if (str[position] == 'i')
                {
                    current_state = States.q13;
                    moveCurrent = UmerMovement.L;
                    replaceChar = 'i';
                    positionCurrent = position;
                    position = position - 1;
                }
                else
                {
                    current_state = States.halt;
                    moveCurrent = UmerMovement.H;
                }
            }
        }
        else if (current_state == States.q5)
        {
            {
                if (str[position] == 'o')
                {
                    current_state = States.q5;
                    moveCurrent = UmerMovement.L;
                    replaceChar = 'a';
                    positionCurrent = position;
                    position = position - 1;
                }
                else if (str[position] == 'a')
                {
                    current_state = States.q5;
                    moveCurrent = UmerMovement.L;
                    replaceChar = 'a';
                    positionCurrent = position;
                    position = position - 1;
                }
                else if (str[position] == 'Δ')
                {
                    current_state = States.q0;
                    moveCurrent = UmerMovement.R;
                    replaceChar = 'Δ';
                    positionCurrent = position;
                    position = position + 1;
                }
                else
                {
                    current_state = States.halt;
                    moveCurrent = UmerMovement.H;
                }
            }
        }
        else if (current_state == States.q6)
        {
            if (str[position] == 'o')
            {
                current_state = States.q6;
                moveCurrent = UmerMovement.L;
                replaceChar = 'o';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'a')
            {
                current_state = States.q6;
                moveCurrent = UmerMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'i')
            {
                current_state = States.q6;
                moveCurrent = UmerMovement.L;
                replaceChar = 'z';
                positionCurrent = position;
                position = position - 1;
            }
           else if (str[position] == 'Δ')
            {
                current_state = States.q0;
                moveCurrent = UmerMovement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }

        }
        else if (current_state == States.q7)
        {

            current_state = States.halt;
            moveCurrent = UmerMovement.H;

        }
        else if (current_state == States.q8)
        {
            if (str[position] == 'Δ')
            {
                current_state = States.q9;
                moveCurrent = UmerMovement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }
        }
        else if (current_state == States.q9)
        {
            if (str[position] == 'o')
            {
                current_state = States.q10;
                moveCurrent = UmerMovement.R;
                replaceChar = 'o';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }
        }
        else if (current_state == States.q10)
        {
            if (str[position] == 'i')
            {
                current_state = States.q11;
                moveCurrent = UmerMovement.R;
                replaceChar = 'i';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }
        }
        else if (current_state == States.q11)
        {
            if (str[position] == 'Δ')
            {
                current_state = States.q7;
                moveCurrent = UmerMovement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }
        }
       
         else if (current_state == States.q13)
        {
            if (str[position] == 'i')
            {
                current_state = States.q13;
                moveCurrent = UmerMovement.L;
                replaceChar = 'i';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'a')
            {
                current_state = States.q5;
                moveCurrent = UmerMovement.L;
                replaceChar = 'a';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = States.halt;
                moveCurrent = UmerMovement.H;
            }
        }

    }
}
public enum States
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
    halt
}