using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameIntro : MonoBehaviour
{
   
   void Awake() {
    this.GetComponent<VideoPlayer>().Play();
    this.GetComponent<VideoPlayer>().loopPointReached += CheckOver;
   }

   void CheckOver(VideoPlayer vp) {
    SceneManager.LoadScene(2);
   }

   void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
         SceneManager.LoadScene(2);
      }
   }

}
