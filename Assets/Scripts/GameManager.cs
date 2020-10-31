using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple manager that just quits the program when escape is pressed
/// </summary>
public class GameManager : MonoBehaviour
{
    private void Update()
    {
        bool exitGame = Input.GetButtonDown("Cancel");
        if(exitGame)
		{
            Application.Quit(0);
		}
    }
}
