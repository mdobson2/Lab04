using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// References.cs gatheres references from various objects in the screen.
/// It then stores the references for use in the game at later points. 
/// @author Tiffany Fisher, 9-10-2015
/// </summary>

public class References : MonoBehaviour {

    //Variables to store all information, hidden from designer
    [HideInInspector]
    public Text[] letters;
    [HideInInspector]
    public Image[] correct;
    [HideInInspector]
    public Image[] unfilled;
    [HideInInspector]
    public Text hint;
    [HideInInspector]
    public Text score;
    [HideInInspector]
    public Image gameOverImage, gameWonImage;
    [HideInInspector]
    public Text gameOverText, gameWonText;
    [HideInInspector]
    public GameObject gameOverButton, gameWonButton;
    [HideInInspector]
    public Button[] buttons;

    //Altered
    void Start () {

        //Initialize the arrays to store 52 items, which is the number of letters on the wheel of 
        //fortune board
        letters = new Text[52];
        correct = new Image[52];
        unfilled = new Image[52];

        //Gather all of the references from the top row
        for(int i = 0; i < 12; i++)
        {
            GameObject obj = GameObject.Find("Top Row (" + i + ")");
            letters[i] = obj.GetComponentInChildren<Text>();
            correct[i] = obj.GetComponentsInChildren<Image>()[0];
            unfilled[i] = obj.GetComponentsInChildren<Image>()[1];
        }

        //Gather all of the references from the top middle row
        for(int i = 0; i < 14; i++)
        {
            GameObject obj = GameObject.Find("Top Middle Row (" + i + ")");
            letters[i+12] = obj.GetComponentInChildren<Text>();
            correct[i+12] = obj.GetComponentsInChildren<Image>()[0];
            unfilled[i+12] = obj.GetComponentsInChildren<Image>()[1];
        }

        //Gather all of the references from the bottom middle row
        for (int i = 0; i < 14; i++)
        {
            GameObject obj = GameObject.Find("Bottom Middle Row (" + i + ")");
            letters[i+26] = obj.GetComponentInChildren<Text>();
            correct[i+26] = obj.GetComponentsInChildren<Image>()[0];
            unfilled[i+26] = obj.GetComponentsInChildren<Image>()[1];
        }

        //Gather all of the references from the bottom row
        for (int i = 0; i < 12; i++)
        {
            GameObject obj = GameObject.Find("Bottom Row (" + i + ")");
            letters[i + 40] = obj.GetComponentInChildren<Text>();
            correct[i + 40] = obj.GetComponentsInChildren<Image>()[0];
            unfilled[i + 40] = obj.GetComponentsInChildren<Image>()[1];
        }

        //Gather references to both hint and score location
        hint = GameObject.Find("Hint").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();

        //for (int i = 0; i < letters.Length; i++)
        //{
        //    Debug.Log(i);
        //    letters[i].text = i.ToString();
        //}

        //Gather the images, text, and buttons for game over and game won
        gameOverImage = GameObject.Find("GameOver").GetComponent<Image>();
        gameOverText = gameOverImage.gameObject.GetComponentInChildren<Text>();
        gameOverButton = GameObject.Find("GameOverButton");
        gameWonImage = GameObject.Find("GameWon").GetComponent<Image>();
        gameWonText = gameWonImage.gameObject.GetComponentInChildren<Text>();
        gameWonButton = GameObject.Find("GameWonButton");

        //Disable the graphics for game over and game won
        //(You cannot use GameObject.Find to locate a disabled object, so they start
        //enabled and are manually disabled)
        gameOverImage.enabled = false;
        gameOverText.enabled = false;
        gameWonImage.enabled = false;
        gameWonText.enabled = false;
        gameOverButton.SetActive(false);
        gameWonButton.SetActive(false);

        //Gather references to all of the buttons that corrispond with letters
        buttons = GameObject.Find("Buttons").GetComponentsInChildren<Button>();

        //Inform the Engine to begin the game!
        SendMessage("Begin");
    }


}
