using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    private Audio audioScript;
    public Text scoresText;
    public Text coinText;
    public Text life;
    private void Start() {
        audioScript = GameObject.Find("AudioMarioBros").GetComponent<Audio>();
    }
    public enum Type{
        Coin, ExtraLife, MagicMushroom, StarPower,
    }

    public Type type;  
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Collet(other.gameObject);
        }
    }

    private void Collet(GameObject player){
        switch(type){
            case Type.Coin:
                GameManager.Instance.AddCoin();
                GameManager.Instance.AddScoresCoin();
                if(GameManager.diem < 1000000){
                    scoresText.text = GameManager.diem.ToString("D6");
                }else{
                    scoresText.text = "Choi ma` hack ?";
                }
                if(GameManager.dongCoin < 100){
                    coinText.text = GameManager.dongCoin.ToString("D2");
                }else{
                    GameManager.mang++;
                    life.text = GameManager.mang.ToString("D2");
                }
                audioScript.CoinAudio();
                break;

            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;

            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                audioScript.UpPowerAudio();
                GameManager.Instance.AddScoresPowerUp();
                break;

            case Type.StarPower:
                player.GetComponent<Player>().Starpower();
                audioScript.PowerStartAudio();
                GameManager.Instance.AddScoresPowerUp();
                break;
        }
        Destroy(gameObject);
    }
}
