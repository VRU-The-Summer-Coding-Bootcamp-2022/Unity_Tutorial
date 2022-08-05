using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Singelton : MonoBehaviour
//{
//    public static Singelton instance;

//    private int score = 0;

//    private void Awake()
//    {
//        if (instance != null && instance != this)
//            Destroy(this);
//        else
//            instance = this;
//    }

//    public void AddScore(int amount)
//    {
//        score += amount;
//        Debug.Log($"score is : {score}");
//    }
//}






public class Singelton : MonoBehaviour
{
    private static Singelton _instance;
    public static Singelton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Singelton();
            }
            return _instance;
        }
    }

    private int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"score is : {score}");
    }
}
