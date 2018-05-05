using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {

    public static bool IS_KITCHEN_CLOSED = false;
    public Rigidbody[] lifes;
    public GameObject ceilingLight;
    public Text restartText;
    public ChefHand left;
    public ChefHand right;
    public GameObject gameOverText;

    private int _lifeLeft = 5;
    private bool _canRestart = false;    

    internal void Decrease(int lifeLose)
    {
        if (IS_KITCHEN_CLOSED)
            return;

        _lifeLeft -= lifeLose;
        if (_lifeLeft < 0)
            _lifeLeft = 0;

        for (int i = _lifeLeft; i < lifes.Length; ++i)
        {
            lifes[i].isKinematic = false;
        }

        if (_lifeLeft <= 0)
        {
            //stop spawning at WhiteboardScript
            //get all customers out at CustomerScript
            IS_KITCHEN_CLOSED = true;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5.0f);
        ceilingLight.SetActive(false);

        yield return new WaitForSeconds(5.0f);
        _canRestart = true;
        gameOverText.SetActive(true);

        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            restartText.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            restartText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (_canRestart && (left.IsPointing() || right.IsPointing()))
        {
            SceneManager.LoadScene("1. Start");
        }
    }
}
