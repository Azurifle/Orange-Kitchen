using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerTable : MonoBehaviour {

    public FoodCheckerScript foodChecker;
    public WhiteboardScript whiteboard;
    public Transform player;
    public ParticleSystem winParticle;

    private TextMesh menuNote;
    private List<Rigidbody> waitToThrows;

    private void Start()
    {
        menuNote = foodChecker.text.GetComponent<TextMesh>();
        waitToThrows = new List<Rigidbody>();
    }

    private void Update()
    {
        if (menuNote.text != "")
        {
            foreach (Rigidbody item in waitToThrows)
                ThrowBack(item);
            waitToThrows.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BowlFinishing"))
        {
            if (other.gameObject.layer == HandController.LAYER_ITEM && !other.CompareTag("Bowl"))
            {
                waitToThrows.Add(other.GetComponent<Rigidbody>());
            }
            return;
        }

        BowlFinishing bowl = other.GetComponent<BowlFinishing>();
        if (menuNote.text != "" && bowl.HasOrangeFishBall &&
            ((foodChecker.soupReq >= 1 && bowl.HasSoup) || (foodChecker.soupReq <= 0 && !bowl.HasSoup))
            && foodChecker.porkReq == bowl.RedPorkCount && foodChecker.noodleReq == bowl.NoodleCount)
        {
            winParticle.Play();
            whiteboard.FinishOrder(foodChecker.seatNo - 1);
            bowl.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.up * 10000);
            Destroy(bowl.transform.parent.gameObject, 1f);
            StartCoroutine(Wait());
        }   
        else
        {
            waitToThrows.Add(bowl.transform.parent.GetComponent<Rigidbody>());
        }
    }

    private void ThrowBack(Rigidbody item)
    {
        if(item)
            item.AddForce(Vector3.Normalize(
               (player.position+Vector3.up*2) - transform.position) * 5000);//2500
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        winParticle.Stop();
    }
}
