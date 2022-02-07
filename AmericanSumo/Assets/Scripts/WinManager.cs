using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class WinManager : MonoBehaviour
{
    [SerializeField] GameObject hideOnWin;
    [SerializeField] GameObject info;
    [SerializeField] GameObject blueWin;
    [SerializeField] GameObject redWin;
    [SerializeField] GameObject showOnWin;
    [SerializeField] FixedJoystick restartButton;
    [SerializeField] static int blueWins;
    [SerializeField] static int redWins;
    [SerializeField] TextMeshProUGUI redWinsText;
    [SerializeField] TextMeshProUGUI blueWinsText;
    [SerializeField] TextMeshProUGUI sceneName1;
    [SerializeField] TextMeshProUGUI sceneName2;
    int map;
    bool won;
    private string[] discoveredGamemodesArray;

    [SerializeField] private static List<string> discoveredGamemodes =  new List<string>();
    public string gamemodename;
    bool p1ready;
    bool p2ready;
    [SerializeField] FixedJoystick ready1;
    [SerializeField] FixedJoystick ready2;
    public int bluePlayersAlive;
    public int redPlayersAlive;
    void Start()
    {
        showOnWin.SetActive(false);
        hideOnWin.SetActive(false);
        won = false;
        if (!discoveredGamemodes.Contains(gamemodename))
        {
            info.SetActive(true);
        }
        else
        {
            hideOnWin.SetActive(true);
            info.SetActive(false);
            Invoke("HideSceneName", 1);
        }
    }

    void HideSceneName()
    {
        sceneName1.gameObject.SetActive(false);
        sceneName2.gameObject.SetActive(false);
    }

    void Update()
    {
        redWinsText.text = redWins.ToString();
        blueWinsText.text = blueWins.ToString();
        sceneName1.text = SceneManager.GetActiveScene().name;
        sceneName2.text = SceneManager.GetActiveScene().name;
        if (restartButton.Horizontal != 0 || restartButton.Vertical != 0)
        {
            map++;
            int index = Random.Range(1, SceneManager.sceneCountInBuildSettings + 1);
            SceneManager.LoadScene(index);
        }
        if (ready1.Horizontal != 0 || ready1.Vertical != 0)
        {
            p1ready = true;
            ready1.gameObject.SetActive(false);
        }
        if (ready2.Horizontal != 0 || ready2.Vertical != 0)
        {
            p2ready = true;
            ready2.gameObject.SetActive(false);
        }
        if(p1ready && p2ready && !hideOnWin.activeSelf)
        {
            info.SetActive(false);
            hideOnWin.SetActive(true);
            discoveredGamemodes.Add(gamemodename);
            Invoke("HideSceneName", 1);
        }

        if (bluePlayersAlive <= 0)
        {
            Win("red");
        }
        if (redPlayersAlive <= 0)
        {
            Win("blue");
        }
    }
    public void Win(string player)
    {
        if (!won)
        {
            showOnWin.SetActive(true);
            hideOnWin.SetActive(false);
            won = true;
            if (player == "red")
            {
                redWin.SetActive(true);
                redWins++;
            }
            else
            {
                blueWin.SetActive(true);
                blueWins++;
            }
        }
      
    }
}
