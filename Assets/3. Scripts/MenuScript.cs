using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Text StartText;

	// Use this for initialization
	void Start () {
        StartCoroutine(Blink());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Blink ()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            StartText.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            StartText.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        Application.LoadLevel(1);
    }
}
