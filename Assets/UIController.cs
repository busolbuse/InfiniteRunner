using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    Player player;
    TextMeshProUGUI distanceText;

    //results pannel
    GameObject results;
    TextMeshProUGUI finalDistanceText;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();

        finalDistanceText = GameObject.Find("FinalDistance").GetComponent<TextMeshProUGUI>();
        results = GameObject.Find("Results");
        results.SetActive(false);//default
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";
        
        //if player dead
        if(player.isDead)
        {
            results.SetActive(true);//show results pannel
            finalDistanceText.text = distance + " m";
        }
    }

    //Scenes and Button
    public void Quit()
    {
        //use scene management
        SceneManager.LoadScene("RanMenu");
    }

    public void Retry()
    {
        //use scene management
        SceneManager.LoadScene("SampleScene");//default scene( current  scene)
    }


}
