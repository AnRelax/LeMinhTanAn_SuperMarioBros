using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Audio audioScript;
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    private GameManager gameManager;

    public CapsuleCollider2D capsuleCollider { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }
    public Text scoresText;
    public Text coinText;
    public Text life;
    public Text world;
    public Text stage;
    public Text time;
    public float countdownTime = 900f;
    private float currentTime;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
    }

    private void Start() {
        currentTime = countdownTime;

        audioScript = GameObject.Find("AudioMarioBros").GetComponent<Audio>();
        scoresText.text = GameManager.diem.ToString("D6");
        coinText.text = GameManager.dongCoin.ToString("D2");
        life.text = GameManager.life.ToString("D2");
        world.text = GameManager.worldNow.ToString("D1");
        stage.text = GameManager.stageNow.ToString("D1");
    }
    private void Update() {
        currentTime -= Time.deltaTime;

        int seconds = Mathf.FloorToInt(currentTime);
        time.text = seconds.ToString("D3");

        if (currentTime <= 0)
        {
            // Xử lý khi đồng hồ đếm ngược đạt tới 0
            // Ví dụ: Dừng trò chơi, hiển thị thông báo, vv.
            // Điều chỉnh mã này dựa trên nhu cầu của bạn.
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameManager.Instance.LoadLevel(1, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "1UP"){
            life.text = GameManager.life.ToString("D2");
        }
    }
    public void Hit()
    {
        if (!dead && !starpower)
        {
            if (big) {
                Shrink();
            } else {
                Death();
            }
        }
    }

    public void Death()
    {
        audioScript.MarioCloseAudio();

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
        
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    public void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
        audioScript.playNen();
    }

}
