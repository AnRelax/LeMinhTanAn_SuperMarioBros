using UnityEngine;

public class GameMenu : MonoBehaviour
{
     public GameObject Notification;
    public GameObject MenuGame;
    public GameObject OptionGame;
    private AudioSource audioSource;
    public AudioClip audioClip;
    private void Start() {
        MenuGame.SetActive(true);
        Notification.SetActive(false);
        OptionGame.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();
        audioSource.volume = 0.5f;
        PlayerPrefs.SetFloat("checkVolume",audioSource.volume);
    }
    bool check = false;
    public void ContinueGame(){
        if(check == true){
            PlayerPrefs.SetString("CheckButton","Continue");
            GameManager.Instance.LoadLevel(1, 1);
        }else{
            MenuGame.SetActive(false);
            Notification.SetActive(true);
        }
    }
    public void StartGame(){
        PlayerPrefs.SetString("CheckButton","Start");
        GameManager.Instance.LoadLevel(1, 1);
    }
    public void OptionGames(){
        MenuGame.SetActive(false);
        OptionGame.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }
    public void ClickOkNotification(){
        Notification.SetActive(false);
        MenuGame.SetActive(true);
    }
    public void ClickOkOption(){
        OptionGame.SetActive(false);
        MenuGame.SetActive(true);
    }
}

