using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class ScriptEngine : MonoBehaviour {

    public Text questionText;
    public Text statusText;

    int numCorrect = 0;

    List<string> questions;
    List<string> answers;
    int numDisplay = 0;
    string answer;

	public void RunGame(List<string> pQuestions, List<string> pAnswers)
    {
        questions = pQuestions;
        answers = pAnswers;

        NextQuestion();
    }

    public void NextQuestion()
    {
        questionText.text = questions[numDisplay];
        statusText.text = "Number Right: " + numCorrect;
    }

    public void TrueAnswer()
    {
        answer = "true";
        CheckAnswer();
    }

    public void FalseAnswer()
    {
        answer = "false";
        CheckAnswer();
    }

    public void CheckAnswer()
    {
        if(answer == answers[numDisplay].ToLower())
        {
            numCorrect++;
        }
        numDisplay++;
        NextQuestion();
    }
}
