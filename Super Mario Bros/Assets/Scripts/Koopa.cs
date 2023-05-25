using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;
    public Text scoresText;
    public GameObject damageTextPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                EnterShell();
            }  else {
                player.Hit();
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starpower) {
                    Hit();
                } else {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        shelled = true;

        GameManager.Instance.AddScoresKoopa();
        if(GameManager.diem < 1000000){
            scoresText.text = GameManager.diem.ToString("D6");
        }else{
            scoresText.text = "Choi ma` hack ?";
        }
        
        GetComponent<SpriteRenderer>().sprite = shellSprite;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = false;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed) {
            Destroy(gameObject);
        }
    }

    private void TakeDamage(int damage, bool isCritical)
    {
        GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        TextMeshPro damageTextMesh = damageTextObject.GetComponent<TextMeshPro>();
        if(damageTextMesh != null){
            damageTextMesh.text = damage.ToString("N0");
            if (isCritical){
                damageTextMesh.color = Color.red;
            }
            StartCoroutine(FlyUpFadeOutAndDestroy(damageTextObject, 2f, 1.25f));
        }else{
            Debug.Log("khong tim thay text mesh");
        }
    }
    private IEnumerator FlyUpFadeOutAndDestroy(GameObject obj, float duration, float flySpeed)
    {
        TextMeshPro textMesh = obj.GetComponent<TextMeshPro>();
        if (textMesh != null){
            float elapsedTime = 0f;
            float initialYPosition = obj.transform.position.y + 0.4f;
            Color startColor = textMesh.color;
            while (elapsedTime < duration){
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

                float newYPosition = initialYPosition + (flySpeed * elapsedTime);
                obj.transform.position = new Vector3(obj.transform.position.x, newYPosition, obj.transform.position.z);

                elapsedTime += Time.deltaTime;
                Destroy(obj, 2f);
                yield return null;
            }
            
        }
    }
}
