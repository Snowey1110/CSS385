using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int maxPlanes = 10;
    public int numberOfPlanes = 0;
    public Text EnemyCountText = null;
    public Text EggCountText = null;
    public Text HeroControlMode = null;
    public Text PlanesAlive = null;
    public Text Tips = null;
    public Text Waypoints = null;
    public Text followMode = null;
    public int planesDestroyed = 0;
    private int EggCount = 0;
    public bool MouseMode = true;
    public bool tipsOn = true;
    public GameObject GameOverScreen = null;
    public GameObject waypoints;
    public bool waypointsActive = true;
    public bool randomWaypoints = false;

    public GameObject Boss;
    public GameObject BossHP;
    public bool BossAppear = false;

    public GameObject BackgroundMusic;
    public GameObject BossMusic;

    public GameObject VictoryScreen;

    public bool debugDisable = false;

    public bool Cheated = false;

    public int Cheesed = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlanesAlive.text = "Planes remains: " + maxPlanes;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cheesed >= 10)
        {
            Cheated = true;
        }
        if (planesDestroyed >= 10 && BossAppear == false)
        {
            Boss.SetActive(true);
            BossHP.SetActive(true);
            BossMusic.SetActive(true);
            BackgroundMusic.SetActive(false);
            BossAppear = true;
            
        }

        if (Time.time >= 3f && debugDisable == false)
        {
            debug();
            debugDisable = true;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            debug();
        }
        if (Input.GetKey(KeyCode.Q))
        {
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if (numberOfPlanes < maxPlanes)
        {
            SpawnPlane();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (randomWaypoints == true)
            {
                randomWaypoints = false;
                followMode.text = "Following: Sequentially";
            }
            else
            {
                randomWaypoints = true;
                followMode.text = "Following: Randomly";
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (waypointsActive == true)
            {
                waypoints.SetActive(false);
                waypointsActive = false;
                Waypoints.text = "Waypoints: Hide";

            } else
            {
                waypoints.SetActive(true);
                waypointsActive = true;
                Waypoints.text = "Waypoints: Show";
            }
           
            

        }
    }

    public void EnemyDestroyed() {
        --numberOfPlanes;
        planesDestroyed = planesDestroyed + 1;
        EnemyCountText.text = "Planes Destroyed: " + planesDestroyed;
    } 
    public void SpawnPlane()
    {
        CameraSupport s = Camera.main.GetComponent<CameraSupport>();
        Assert.IsTrue(s != null);

        GameObject e = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject); // Prefab MUST BE locaed in Resources/Prefab folder!
        Vector3 pos;
        pos.x = s.GetWorldBound().min.x + Random.value * s.GetWorldBound().size.x;
        pos.y = s.GetWorldBound().min.y + Random.value * s.GetWorldBound().size.y;
        pos.z = 0;
        e.transform.localPosition = pos;
        ++numberOfPlanes;
    }


    public void addEgg()
    {
        ++EggCount;
        EggCountText.text = "Egg Count: " + EggCount;
    }

    public void removeEgg()
    {
        --EggCount;
        EggCountText.text = "Egg Count: " + EggCount;
    }

    public void switchMode()
    {
        if (MouseMode == true)
        {
            HeroControlMode.text = "Press M to change mode\nControl Mode: Keyboard";
        } else
        {
            HeroControlMode.text = "Press M to change mode\nControl Mode: Mouse";
        }
        
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void ExtraCredits()
    {
        SceneManager.LoadScene("Extra Credits");
    }

    public void gameOver()
    {
        GameOverScreen.SetActive(true);
        GameObject Mouse = Instantiate(Resources.Load("Prefabs/Mouse") as GameObject);
        VictoryScreen.SetActive(false);
    }

    public void debug()
    {
        if (tipsOn == true)
        {
            Tips.gameObject.SetActive(false);
            EnemyCountText.gameObject.SetActive(false);
            HeroControlMode.gameObject.SetActive(false);
            EggCountText.gameObject.SetActive(false);
            PlanesAlive.gameObject.SetActive(false);
            followMode.gameObject.SetActive(false);
            Waypoints.gameObject.SetActive(false);
            tipsOn = false;
            
        }
        else
        {
            Tips.gameObject.SetActive(true);
            EnemyCountText.gameObject.SetActive(true);
            HeroControlMode.gameObject.SetActive(true);
            EggCountText.gameObject.SetActive(true);
            PlanesAlive.gameObject.SetActive(true);
            followMode.gameObject.SetActive(true);
            Waypoints.gameObject.SetActive(true);
            tipsOn = true;
        }
    }

    public bool waypoint()
    {
        return randomWaypoints;
    }
}
