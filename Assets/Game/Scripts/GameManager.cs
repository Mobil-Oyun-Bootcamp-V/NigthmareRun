using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private GameManager(){}

   private static GameManager _manager;

   public static GameManager Instance => _manager;

   private DataContainer _container;

   private void Awake()
   {
      if(_manager == null)
      {
         _manager = this;
         DontDestroyOnLoad(this);
         _container = new DataContainer();
      }
      else if (_manager != this)
      {
         Destroy(gameObject);
      }
   }
   
   public enum GameSceneState
   {
      PLAY,END
   }

   public GameSceneState currentSceneState = GameSceneState.PLAY;




}
