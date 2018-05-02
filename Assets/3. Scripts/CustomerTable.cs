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
            bowl.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.up * 10000);
            Destroy(bowl.transform.parent.gameObject, 1f);
            StartCoroutine(Wait());
        }   
        else
        {
            bowl.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(
                Vector3.Normalize(player.position - transform.position)+Vector3.up) * 4000);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        winParticle.Stop();
    }
}
