using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject fadeBlackout;

    void Start()
    {
       //StartBlackout();
    }
    
    public void EndBlackout() => fadeBlackout.SetActive(false);
    
    public void StartBlackout() => fadeBlackout.SetActive(true);
    
}