using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Text StartText;
    public ChefHand left;
    public ChefHand right;
    
    // Use this for initialization
    void Start () {
        StartCoroutine(Blink());
	}
	
	// Update is called once per frame
	void Update () {
        if (left.IsGrabbing() || right.IsGrabbing())
            StartGame();
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
        SceneManager.LoadScene("2. 1st_Playable");
    }
}
