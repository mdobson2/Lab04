using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Engine.cs is responsible for setting up the game, responding to player input,
/// and deciding if the game is won or lost.
/// @author Tiffany Fisher, 9-10-2015
/// </summary>

//Force unity to add the references script if it is not already on the object
[RequireComponent(typeof(References))]
public class Engine : MonoBehaviour {
    
    //Variables
    References refs;
    int points;
    string word = "  Luck Be       In The         Air        Tonight"; //Stores the phrase the player should guess
    string hint = "Phrases";                    //Stores the hint provided to the player 
    int charGoal = 0;                           //Stores how many letters the player needs to guess
    int charGot = 0;                            //Tracks how many letters the player has already guessed

    /// <summary>
    /// Called by References once it is done gathering all neccessary information
    /// Starts the game
    /// </summary>
    void Begin()
    {
        //Gets a link to the references script
        refs = gameObject.GetComponent<References>();
        //Starts the game
        StartGame();
    }

    /// <summary>
    /// Called by buttons in the scene to check the players input.
    /// </summary>
    /// <param name="letter">The letter to check against the phrase the player is guessing</param>
    public void CheckLetter(string letter)
    {
        //Find the button the player clicked and disable it so it cannot be clicked again
        GameObject.Find(letter).GetComponent<Button>().interactable = false;       

        //Start off pessimestic and assume the player didn't click a letter in the phrase
        bool found = false;

        //If the letter the player guessed in a space character, ignore it
        if (letter == " ")
            return;

        //Start looking at each and every character in the string holding the phrase the player is guessing
        for (int i = 0; i < word.Length; i++)
        {
            //If the current character is a space, ignore it
            if (word[i] == ' ')
            {
                continue;
            }

            //If the current character is the same letter the player guessed
            if (word[i] == letter[0])
            {
                //Show the letter on the screen for the player
                refs.letters[i].text = letter.ToString();
                //Change the background of the letter to something lighter
                refs.correct[i].enabled = true;
                //Hide the "unfilled" background so the lighter colour will show
                refs.unfilled[i].enabled = false;
                //Inform the algorithm a match has been found
                found = true;
                //Store that the player has found one more letter towards the goal
                charGot++;
            }
        }

        //Debug.Log("Got: " + charGot + " goal: " + charGoal);

        //If the player has found a letter, award them points
        //Otherwise, take their points
        if (found)
            points++;
        else
            points--;

        //If the player has no points left, end the game
        if (points <= 0)
            EndGame();

        //If the player has guessed all the letters, win the game
        if (charGot >= charGoal)
            WinGame();

        //Show the user their new score
        refs.score.text = points.ToString();

    }

    /// <summary>
    /// Shows a win image, a short text informing the player they won, and a restart button
    /// </summary>
    void WinGame()
    {
        refs.gameWonImage.enabled = true;
        refs.gameWonText.enabled = true;
        refs.gameWonButton.SetActive(true);
    }

    /// <summary>
    /// Shows the player the answer, an image, a short text saying they lost, and a restart button
    /// </summary>
    void EndGame()
    {
        //Go through each letter in the phrase the player is supposed to guess
        for (int i = 0; i < word.Length; i++)
        {
            //Skip spaces
            if (word[i] == ' ')
            {
                continue;
            }
            //Display the letter to the player
            refs.letters[i].text = word[i].ToString();
        }

        refs.gameOverImage.enabled = true;
        refs.gameOverText.enabled = true;
        refs.gameOverButton.SetActive(true);
    }

    /// <summary>
    /// Sets all player data to default values 
    /// </summary>
    public void StartGame()
    {
        //Sets the goal, and how many letters the player has guessed to zero
        charGoal = 0;
        charGot = 0;

        //If there are too many letters in the phrase, skip it 
        //Probably some more robust error checking and fail-safe should happen here
        if (word.Length > 51)
        {
            Debug.Log("PHRASE TO LONG!");
            return;
        }

        //Disable all game over/won images, texts, and buttons
        refs.gameOverImage.enabled = false;
        refs.gameOverText.enabled = false;
        refs.gameWonImage.enabled = false;
        refs.gameWonText.enabled = false;
        refs.gameOverButton.SetActive(false);
        refs.gameWonButton.SetActive(false);

        //Convert the phrase to uppercase for comparison with player input
        word = word.ToUpper();

        //Go through each letter display on screen and erase all text
        //Also, hide all indications that a letter should belong there
        for (int i = 0; i < 52; i++)
        {
            refs.letters[i].text = "";
            refs.unfilled[i].enabled = false;
            refs.correct[i].enabled = false;
        }

        //Set up to split up the phrase based on spaces
        char[] delimiterCharacters = { ' ' };

        //Split the phrase at every space
        string[] words = word.Split(delimiterCharacters);

        //Set the inital amount of points to (number of letters)/3
        points = (words.Length)/3;

        //Make sure the player starts with at LEAST three points
        if(points < 3)
            points = 3;

        //Show the player their starting points
        refs.score.text = points.ToString();

        //Display where letters will appear on the board
        for (int i = 0; i < word.Length; i++)
        {
            //Skip spaces
            if (word[i] == ' ')
            {
                continue;
            }

            refs.unfilled[i].enabled = true;
            //keep track of how many letters there are
            charGoal++;

        }

        //Show the player the hint
        refs.hint.text = hint;

        //Enable all of the buttons on the screen so the player can interact again
        foreach(Button bt in refs.buttons)
        {
            bt.interactable = true;
        }
    }

    
}
