using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockCoin : MonoBehaviour
{
    private Audio audioScript;
    public Text scoresText;
    private void Start() {
        GameManager.Instance.AddCoin();
        audioScript = GameObject.Find("AudioMarioBros").GetComponent<Audio>();
        StartCoroutine(Animate());
    }
     private IEnumerator Animate(){
        audioScript.CoinAudio();

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;
        
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        Destroy(gameObject);
    }
    private IEnumerator Move(Vector3 from, Vector3 to){
        float elapsed = 0f;
        float duration = 0.25f;
        
        while(elapsed < duration){
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed = elapsed + Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }
}
