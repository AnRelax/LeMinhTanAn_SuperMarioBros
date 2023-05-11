using System.Collections;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip nenAudio;
    public AudioClip jumpSmallMarioAudio;
    public AudioClip jumpBigMarioAudio;
    public AudioClip coinAudio;
    public AudioClip goombaAudio;
    public AudioClip upAudio;
    public AudioClip flagPoleAudio;
    public AudioClip bumpAudio;
    public AudioClip marioCloseAudio;
    public AudioClip powerStartAudio;
    public AudioClip timeSpeedAudio;
    public AudioClip upPowerAudio;
    public AudioClip downTheTubeAudio;
    public AudioClip itemblockAudio;
    public AudioClip levelClearAudio;
    public AudioClip gameOverAudio;

    
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        playNen();
    }
    public void playNen(){
        audioSource.clip = nenAudio;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void JumpSmallMarioAudio(){
        audioSource.PlayOneShot(jumpSmallMarioAudio);
    }
    public void JumpBigMarioAudio(){
        audioSource.PlayOneShot(jumpBigMarioAudio);
    }
    public void CoinAudio(){
        audioSource.PlayOneShot(coinAudio);
    }
    public void GoombaAudio(){
        audioSource.PlayOneShot(goombaAudio);
    }
    public void UpAudio(){
        audioSource.PlayOneShot(upAudio);
    }
    public void FlagPoleAudio(){
        audioSource.PlayOneShot(flagPoleAudio);
    }
    public void BumpAudio(){
        audioSource.PlayOneShot(bumpAudio);
    }
    public void MarioCloseAudio(){
        //can xu li
        audioSource.PlayOneShot(marioCloseAudio);
    }
    public void PowerStartAudio(){
        audioSource.PlayOneShot(powerStartAudio);
    }
    public void TimeSpeedAudio(){
        audioSource.PlayOneShot(timeSpeedAudio);
    }
    public void UpPowerAudio(){
        audioSource.PlayOneShot(upPowerAudio);
    }
    public void DownTheTubeAudio(){
        audioSource.PlayOneShot(downTheTubeAudio);
    }
    public void ItemblockAudio(){
        audioSource.PlayOneShot(itemblockAudio);
    }
    public void LevelClearAudio(){
        audioSource.PlayOneShot(levelClearAudio);
    }
    public void GameOverAudio(){
        audioSource.PlayOneShot(gameOverAudio);
    }
}
