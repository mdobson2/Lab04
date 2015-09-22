using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;


public class ModDataScript : MonoBehaviour {

    FileInfo originalFile;
    TextAsset textFile;
    TextReader reader;
    ScriptEngine engine;

    public List<string> questions = new List<string>();
    public List<string> answers = new List<string>();

	// Use this for initialization
	void Start () {

        engine = Camera.main.GetComponent<ScriptEngine>();
        
        originalFile = new FileInfo(Application.dataPath + "/questions.txt");

        if(originalFile != null && originalFile.Exists)
        {
            reader = originalFile.OpenText(); 
        }
        else
        {
            textFile = (TextAsset)Resources.Load("embedded", typeof(TextAsset));
            reader = new StringReader(textFile.text);
        }

        string lineOfText;
        int lineNumber = 0;

        while((lineOfText = reader.ReadLine()) != null)
        {
            if(lineNumber % 2 == 0)
            {
                questions.Add(lineOfText);
            }
            else
            {
                answers.Add(lineOfText);
            }
            lineNumber++;
        }

        engine.RunGame(questions, answers);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
