using UnityEngine;
using DG.Tweening;

public class ArkaCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BodyPart"))
        {
            ContactPoint contact = collision.GetContact(0);
            Vector3 dir = collision.transform.position - contact.normal;
            collision.transform.DOMove(dir, .01f).SetEase(Ease.InQuart);
        }
    }


}
