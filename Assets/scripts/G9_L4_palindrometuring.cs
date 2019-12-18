using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class turing
{
    public uStates current_state = uStates.q0;
    public char[] str;
    public int position = 2;
    public int positionCurrent;
    public char symbol = 'B';
    public char replaceChar;
    public Movement moveCurrent;
    public turing()
    {

    }

    public void run()
    {

        if (current_state == uStates.q0)
        {
            if (str[position] == '0')
            {
                current_state = uStates.q1;
                moveCurrent = Movement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '1')
            {
                current_state = uStates.q2;
                moveCurrent = Movement.R;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = uStates.halt;
                moveCurrent = Movement.H;
            }

        }
        else if (current_state == uStates.q1)
        {
            if (str[position] == '1')
            {
                current_state = uStates.q1;
                moveCurrent = Movement.R;
                replaceChar = '1';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '0')
            {
                current_state = uStates.q1;
                moveCurrent = Movement.R;
                replaceChar = '0';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = uStates.q3;
                moveCurrent = Movement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }
            else
            {
                current_state = uStates.halt;
                moveCurrent = Movement.H;
            }
        }
        else if (current_state == uStates.q2)
        {
            if (str[position] == '1')
            {
                current_state = uStates.q2;
                moveCurrent = Movement.R;
                replaceChar = '1';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == '0')
            {
                current_state = uStates.q2;
                moveCurrent = Movement.R;
                replaceChar = '0';
                positionCurrent = position;
                position = position + 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = uStates.q4;
                moveCurrent = Movement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }

        }
        else if (current_state == uStates.q3)
        {
            if (str[position] == '0')
            {
                current_state = uStates.q0;
                moveCurrent = Movement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }
           
            else if (str[position] == 'Δ')
            {
                current_state = uStates.q5;
                moveCurrent = Movement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = uStates.halt;
                moveCurrent = Movement.H;
            }
        }
        else if (current_state == uStates.q4)
        {
            if (str[position] == '1')
            {
                current_state = uStates.q0;
                moveCurrent = Movement.L;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position - 1;
            }
            else if (str[position] == 'Δ')
            {
                current_state = uStates.q5;
                moveCurrent = Movement.H;
                replaceChar = 'Δ';
                positionCurrent = position;
                position = position + 1;
            }
            else
            {
                current_state = uStates.halt;
                moveCurrent = Movement.H;
            }

        }

        else if (current_state == uStates.q5)
        {
            
            
                current_state = uStates.halt;
                moveCurrent = Movement.H;
            
        }

    } }
    public enum uStates
    {
        q0,
        q1,
        q2,
        q3,
        q4,
        q5,       
        halt
    }
