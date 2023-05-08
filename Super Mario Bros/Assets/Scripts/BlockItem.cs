using UnityEngine;
using System.Collections;

public class BlockItem : MonoBehaviour
{
    private void Start() {
        StartCoroutine(Animate());
    }
    private IEnumerator Animate(){
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
        physicCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while(elapsed < duration){
            float t = elapsed/duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed = elapsed + Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition;
        rigidbody.isKinematic = false;
        physicCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}
