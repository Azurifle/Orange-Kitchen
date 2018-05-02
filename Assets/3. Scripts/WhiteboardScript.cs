using UnityEngine;

public class WhiteboardScript : MonoBehaviour {

    public GameObject[] notes;
    
    public void TakeOrder(int i, bool boss)
    {
        if (boss)
        {
            notes[i].GetComponent<FoodCheckerScript>().isDeliver = false;
            notes[i].GetComponent<FoodCheckerScript>().seatNo = i + 1;
            notes[i].GetComponent<FoodCheckerScript>().noodleReq = Random.Range(4, 7);
            notes[i].GetComponent<FoodCheckerScript>().porkReq = Random.Range(3, 6);
            notes[i].GetComponent<FoodCheckerScript>().soupReq = Random.Range(0, 2);
        }
        else
        {
            notes[i].GetComponent<FoodCheckerScript>().isDeliver = false;
            notes[i].GetComponent<FoodCheckerScript>().seatNo = i + 1;
            notes[i].GetComponent<FoodCheckerScript>().noodleReq = Random.Range(0, 4);
            notes[i].GetComponent<FoodCheckerScript>().porkReq = Random.Range(0, 3);
            notes[i].GetComponent<FoodCheckerScript>().soupReq = Random.Range(0, 2);
        }
    }

    public void FinishOrder (int i)
    {
        notes[i].GetComponent<FoodCheckerScript>().isDeliver = true;
        SpawnScript.free[i] = true;
    }
}
