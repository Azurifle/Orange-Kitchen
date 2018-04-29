using UnityEngine;
using System.Collections;

public class CustomerTable : MonoBehaviour {

    public FoodCheckerScript foodChecker;
    public WhiteboardScript whiteboard;
    public Transform player;
    public ParticleSystem winParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BowlFinishing"))
            return;

        BowlFinishing bowl = other.GetComponent<BowlFinishing>();
        if (bowl.HasOrangeFishBall &&
            ((foodChecker.soupReq >= 1 && bowl.HasSoup) || (foodChecker.soupReq <= 0 && !bowl.HasSoup))
            && foodChecker.porkReq == bowl.RedPorkCount && foodChecker.noodleReq == bowl.NoodleCount)
        {
            winParticle.Play();
            whiteboard.FinishOrder(foodChecker.seatNo - 1);
            bowl.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.position - player.position) * 1);
            Destroy(bowl.transform.parent.gameObject, 1f);
            StartCoroutine(Wait());
        }   
        else
        {
            bowl.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.position - transform.position) * 1);
            Destroy(bowl.transform.parent.gameObject, 1f);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        winParticle.Stop();
    }
}
