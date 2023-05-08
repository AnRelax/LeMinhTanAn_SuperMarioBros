using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
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
                break;

            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;

            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;

            case Type.StarPower:
                player.GetComponent<Player>().Starpower();
                break;
        }
        Destroy(gameObject);
    }
}
