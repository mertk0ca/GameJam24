using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
   public void PlayVePause()
   {
        if(Time.timeScale==1)
        {
            Time.timeScale=0f;
           
        }
        else
        {
            Time.timeScale=1f;
            
        }
        
   }
   public void ExitGame()
   {
     Debug.Log("Oyun kapanıyor...");
        Application.Quit();
   }
   public void SesButton()
   {
        
   }
   public void MuzikButton()
   {

   }
   
}
