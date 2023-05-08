using UnityEngine;
using System.Collections;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHit = -1;
    private bool animating;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!animating && maxHit != 0 &&collision.gameObject.CompareTag("Player")){
            if(collision.transform.DotTest(transform, Vector2.up)){
                Hit();
                StartCoroutine(Animate());
            }
        }
    }
    private void Hit(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        maxHit--;
        if(maxHit == 0){
            spriteRenderer.sprite = emptyBlock;
        }
        if( item != null){
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
    private IEnumerator Animate(){
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;
        
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }
    private IEnumerator Move(Vector3 from, Vector3 to){
        float elapsed = 0f;
        float duration = 0.125f;
        
        while(elapsed < duration){
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed = elapsed + Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }
}
