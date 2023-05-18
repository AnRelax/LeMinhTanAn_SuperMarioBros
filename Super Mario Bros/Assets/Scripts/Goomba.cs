using UnityEngine;
using UnityEngine.UI;

public class Goomba : MonoBehaviour
{
    private Audio audioScript;
    public Sprite flatSprite;
    public Text scoresText;

    private void Start() {
        audioScript = GameObject.Find("AudioMarioBros").GetComponent<Audio>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            } else {
                player.Hit();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

    private void Flatten()
    {
        audioScript.GoombaAudio();

        GameManager.Instance.AddScoresGoomba();
        //UIGame.Instance.AddScores();
        if(GameManager.diem < 1000000){
            scoresText.text = GameManager.diem.ToString("D6");
        }else{
            scoresText.text = "Choi ma` hack ?";
        }

        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {

        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
