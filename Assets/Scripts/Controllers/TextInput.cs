using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
	InputField input;
	InputField.SubmitEvent se;
	InputField.OnChangeEvent ce;
	public Text output;

	// Use this for initialization
	void Start () {
        output.text = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;

        input = this.GetComponent<InputField>();
		se = new InputField.SubmitEvent();
		se.AddListener(SubmitInput);
		/*
		ce = new InputField.OnChangeEvent();
		ce.AddListener(ChangeInput);
		*/
		input.onEndEdit = se;
		//input.onValueChanged = ce;
	
	}
	
	// Update is called once per frame
	/*
	 * void Update () {
	
	}
	*/

	private void SubmitInput(string arg0)
	{
		string currentText = output.text;

        //  DO THIS LATER 
         CommandProcessor aCmd = new CommandProcessor();
         output.text = aCmd.Parse(arg0);

        //output.text = arg0;

		input.text = "";
		input.ActivateInputField();



	}

	private void ChangeInput( string arg0)
	{
		Debug.Log(arg0);
	}
}
