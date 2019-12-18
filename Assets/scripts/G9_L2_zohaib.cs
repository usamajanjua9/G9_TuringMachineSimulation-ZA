using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G9_L2_zohaib : MonoBehaviour
{

    public InputField zinput;
    public Button zbtn;
    public Text zmessage;
    public Button back;
    public Text zstate;
    public AudioSource spacesound;
    public Button btnReset;
    public Text zsteps;
    public Material red;
    float zcubepos = -3;
    int zcubeIndex;
    private string zstr = null;
    char[] zword;
    Vector3 zPosition;
    int zcounter = 0;
  

    Regex zrgx = new Regex(@"[0-9+]$");


    private zturing zturingmachine = new zturing();

    // Start is called before the first frame update
    void Start()
    {
        back.onClick.AddListener(backs);
        btnReset.onClick.AddListener(restart);
        zPosition = this.transform.position;

       
    }
    public void backs()
    {

        SceneManager.LoadScene("G9_Main_Menu");
    }
    private void restart()
    {
        SceneManager.LoadScene("G9_L2_zohaib");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            zmachineTM();
            spacesound.Play();
        
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           
        }
    }



    private void zmachineTM()
    {
        if (zturingmachine.current_state != ZStates.ha)
        {
            if (zturingmachine.current_state != ZStates.q9 || zturingmachine.moveCurrent == zMovement.H)
            {
                zcounter = zcounter + 1;
               
                zturingmachine.run();
                zturingmachine.str[zturingmachine.positionCurrent] = zturingmachine.replaceChar;
                if (zturingmachine.position>=zword.Length-1)
                {                    
                    generateCube();
                    zcubeIndex = zcubeIndex + 1;
                }
                changeTapeCharacter();
                this.transform.position = new Vector3(zPosition.x + (zturingmachine.position-1) * 2.0f, zPosition.y, zPosition.z);
                Vector3 camerMove = this.transform.position;
            }
        }
        zstate.text = "current State: " + zturingmachine.current_state;
        zsteps.text = "Step: " + zcounter.ToString();
        if (zturingmachine.current_state == ZStates.q9 && zturingmachine.moveCurrent == zMovement.H)
        {
            zturingmachine.current_state = ZStates.ha;
            zmessage.color = Color.blue;
            zmessage.text = "string is accepted";

         
        }
        else if (zturingmachine.current_state != ZStates.q9 && zturingmachine.moveCurrent == zMovement.H)
        {
            zturingmachine.current_state = ZStates.ha;
            zmessage.color = Color.red;
            zmessage.text = "string is rejected";

          
        }



    }


    private void diplayInput()
    {        
        for (zcubeIndex = 0; zcubeIndex < zword.Length; zcubeIndex++)
        {
            generateCube();
            
        }
    }

    private void generateCube() 
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(zPosition.x - 3 + zcubepos, -9.5f, -35.09f);
        
        cube.name = "cube" + zcubeIndex.ToString();
        cube.AddComponent<Renderer>();
        cube.GetComponent<Renderer>().material = red;
        GameObject childObject = new GameObject("text");
        childObject.transform.parent = cube.transform;
        childObject.AddComponent<TextMesh>();
        childObject.GetComponent<TextMesh>().fontSize = 300;
        childObject.GetComponent<TextMesh>().characterSize = 0.1f;
        childObject.GetComponent<TextMesh>().color = Color.blue;
        childObject.GetComponent<TextMesh>().transform.localScale = new Vector3(0.38694f, 0.38694f, 0.38694f);
        childObject.GetComponent<TextMesh>().transform.localPosition = new Vector3(-0.40f, 0.65f, -0.57f);
        childObject.GetComponent<TextMesh>().transform.Rotate(0, 0, 0);
        if (zcubeIndex<zword.Length)
        {
            childObject.GetComponent<TextMesh>().text = zword[zcubeIndex].ToString();   
        }
        else
            childObject.GetComponent<TextMesh>().text = 'Δ'.ToString();
        zcubepos = zcubepos + 3.0f;
    }

    private void changeTapeCharacter()
    {
            GameObject find = GameObject.Find("cube" + zturingmachine.positionCurrent.ToString());
            find.GetComponentInChildren<TextMesh>().text = zturingmachine.replaceChar.ToString();       
    }


    public void saveString()
    {
        char[] blank = new char[1000];
        for (int i = 0; i < 1000; i++)
        {
            blank[i] = 'Δ';
        }
        zstr = "ΔΔ" + zstr + "ΔΔ";
        zword = zstr.ToCharArray();
        int i2=0;
        foreach (var item in zword)
        {
            blank[i2] = item;
            i2 = i2 + 1;
        }
        diplayInput();
        zturingmachine.str = blank;
        zinput.gameObject.SetActive(false);
        zbtn.gameObject.SetActive(false);
        zstate.text = "Current State: q0";
        zsteps.text = "Counter: 0";
    }

    public void checks()
    {

        if (zrgx.IsMatch(zinput.text))
        {
            zstr = zinput.text;
        }
        else
        {
            zinput.text = zstr;
        }
    }
}
public enum zMovement
{ 
    L,R,H,S
}
public class zturing
{
    public ZStates current_state = ZStates.q0;
    public char[] str;
    public int position = 2;
    public int positionCurrent;
    public char symbol = 'B';
    public char replaceChar;
    public zMovement moveCurrent;
    public zturing()
    {

    }

    public void run()
    {

        if (current_state == ZStates.q0)
        {
            if (str[position] == '0')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '0';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '1')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '1';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '2')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '2';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '3')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '3';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '4')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '4';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '5')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '5';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '6')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '6';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '7')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '7';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '8')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '8';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '9')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '9';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '+')
            {
                current_state = ZStates.q0;
                moveCurrent = zMovement.R;
                replaceChar = '+';
                positionCurrent = position;
                position = position + 1;
            }

            else if (str[position] == 'Δ')
            {
                current_state = ZStates.q1;
                moveCurrent = zMovement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }

        }
        else if (current_state == ZStates.q1)
        {
            if (str[position] == '0')
            {
                current_state = ZStates.q1;
                moveCurrent = zMovement.L;
                replaceChar = '0';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'x')
            {
                current_state = ZStates.q1;
                moveCurrent = zMovement.L;
                replaceChar = 'x';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == '+')
            {
                current_state = ZStates.q1;
                moveCurrent = zMovement.L;
                replaceChar = 'x';
                positionCurrent = position;
                position = position - 1;
            }

            else if (str[position] == '1')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '0';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '2')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '1';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '3')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '2';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '4')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '3';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '5')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '4';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '6')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '5';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '7')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '6';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '8')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '7';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '9')
            {
                current_state = ZStates.q2;
                moveCurrent = zMovement.R;
                replaceChar = '8';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = ZStates.q5;
                moveCurrent = zMovement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }
        }
        else if (current_state == ZStates.q2)
        {
            if (str[position] == 'x')
            {
                current_state = ZStates.q3;
                moveCurrent = zMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = ZStates.q3;
                moveCurrent = zMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }

            else if (str[position] == '0')
            {
                current_state = ZStates.q1;
                moveCurrent = zMovement.R;
                replaceChar = '9';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }
        }
        else if (current_state == ZStates.q3)
        {
            if (str[position] == 'x')
            {
                current_state = ZStates.q3;
                moveCurrent = zMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = ZStates.q3;
                moveCurrent = zMovement.R;
                replaceChar = 'y';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '0')
            {
                current_state = ZStates.q3;
                moveCurrent = zMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;

            }
            else if (str[position] == 'Δ')
            {
                current_state = ZStates.q4;
                moveCurrent = zMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;

            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }
        }
        else if (current_state == ZStates.q4)
        {
            if (str[position] == 'y')
            {
                current_state = ZStates.q4;
                moveCurrent = zMovement.L;
                replaceChar = 'y';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'x')
            {
                current_state = ZStates.q1;
                moveCurrent = zMovement.L;
                replaceChar = 'x';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }

        }
        else if (current_state == ZStates.q5)
        {
            if (str[position] == '0')
            {
                current_state = ZStates.q5;
                moveCurrent = zMovement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'x')
            {
                current_state = ZStates.q5;
                moveCurrent = zMovement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'y')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.S;
                replaceChar = 'y';
                positionCurrent = position;
            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }

        }
        else if (current_state == ZStates.q6)
        {
            if (str[position] == '0')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '0';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '1')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '1';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '2')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '2';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '3')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '3';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '4')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '4';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '5')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '5';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '6')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '6';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '7')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '7';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '8')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '8';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '9')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '9';
                positionCurrent = position;
                position = position + 1;
            }

            else if (str[position] == 'Δ')
            {
                current_state = ZStates.q8;
                moveCurrent = zMovement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'y')
            {
                current_state = ZStates.q7;
                moveCurrent = zMovement.L;
                replaceChar = 'x';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'x')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = 'x';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }
        }
        else if (current_state == ZStates.q7)
        {
            if (str[position] == 'x')
            {
                current_state = ZStates.q7;
                moveCurrent = zMovement.L;
                replaceChar = 'x';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == '9')
            {
                current_state = ZStates.q7;
                moveCurrent = zMovement.L;
                replaceChar = '0';
                positionCurrent = position;
                position = position - 1;
            }

            else if (str[position] == '0')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '1';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '1')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '2';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '2')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '3';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '3')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '4';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '4')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '5';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '5')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '6';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '6')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '7';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '7')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '8';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '8')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '9';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = ZStates.q6;
                moveCurrent = zMovement.R;
                replaceChar = '1';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = ZStates.ha;
                moveCurrent = zMovement.H;
            }
        }
        if (current_state == ZStates.q8)
        {
            if (str[position] == 'x')
            {
                current_state = ZStates.q8;
                moveCurrent = zMovement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == '0')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '0';
                positionCurrent = position;
            }
            else if (str[position] == '1')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '1';
                positionCurrent = position;
            }
            else if (str[position] == '2')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '2';
                positionCurrent = position;
            }
            else if (str[position] == '3')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '3';
                positionCurrent = position;
            }
            else if (str[position] == '4')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '4';
                positionCurrent = position;
            }
            else if (str[position] == '5')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '5';
                positionCurrent = position;
            }
            else if (str[position] == '6')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '6';
                positionCurrent = position;
            }
            else if (str[position] == '7')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '7';
                positionCurrent = position;
            }
            else if (str[position] == '8')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '8';
                positionCurrent = position;
            }
            else if (str[position] == '9')
            {
                current_state = ZStates.q9;
                moveCurrent = zMovement.H;
                replaceChar = '9';
                positionCurrent = position;
            }

        }
        else if (current_state == ZStates.q9)
        {
            current_state = ZStates.q9;
            moveCurrent = zMovement.H;
        }

    }

}
public enum ZStates
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
    ha,
    q69,
}