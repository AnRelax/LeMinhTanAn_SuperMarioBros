using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHit = -1;
    public GameObject blockDestroy;
    private bool animating;

    //spamblock
    public GameObject blockPrefab;
    public float jumpForce = 5f;
    public float fallForce = 50f;
    public float spawnDelay = 0.0001f;
    public int numberOfBlocks = 6;
    public Transform spawnPoint;
    public Vector2[] jumpDirections = {new Vector2(0f, 1f), new Vector2(0.3f, 1f), new Vector2(0.6f, 1f), new Vector2(-0.3f, 1f), new Vector2(-0.6f, 1f)};
    private Audio audioScript;
    //------------------------------------//

    // animation
    private Animator animator;
    //------------------------------------//
    public Text scoresText;
    public Text coinText;
    public Text life;

    private void Start() {
        audioScript = GameObject.Find("AudioMarioBros").GetComponent<Audio>();
        animator = GetComponent<Animator>();
    }
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
        if( item != null){
            Instantiate(item, transform.position, Quaternion.identity);
            if(GameManager.dongCoin < 100){
                coinText.text = GameManager.dongCoin.ToString("D2");
            }else{
                GameManager.mang++;
                life.text = GameManager.mang.ToString("D2");
            }
        }
        Player check = FindObjectOfType<Player>();
        if(check.big && maxHit < 0){
            audioScript.BrickAudio();
            SpamBlock();
            Destroy(gameObject, 0.1f);
        }
        if(maxHit >= 0){
            GameManager.Instance.AddScoresCoin();
            if(GameManager.diem < 1000000){
                scoresText.text = GameManager.diem.ToString("D6");
            }else{
                scoresText.text = "Choi ma` hack ?";
            }
            if(maxHit == 0){
                spriteRenderer.sprite = emptyBlock;
                animator.SetBool("UnMysteryBlock",true);
            }
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

    //spamblock
    private void SpamBlock(){
        StartCoroutine(SpawnBlocks());
    }

    private IEnumerator SpawnBlocks()
    {
        for (int i = 0; i < numberOfBlocks; i++)
        {
            GameObject block = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D blockRigidbody = block.GetComponent<Rigidbody2D>();

            // Lựa chọn ngẫu nhiên hướng bay
            Vector2 randomJumpDirection = jumpDirections[Random.Range(0, jumpDirections.Length)];

            // Đẩy game object lên với hướng bay ngẫu nhiên
            blockRigidbody.AddForce(randomJumpDirection * jumpForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(spawnDelay);
            Destroy(block, 2.5f);
        }
    }
    //-------------//
}
