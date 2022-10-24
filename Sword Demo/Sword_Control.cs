using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword_Control : MonoBehaviour
{
    [SerializeField] GameObject Shooter;
    public class Enemy
    {
        public bool positionActive;
        public int positionValue;
        public Enemy(bool active, int position)
        {
            positionActive = active;
            positionValue = position;
        }
    }
    private bool dead = false;
    private List<Enemy> enemies = new List<Enemy>();
    private manager2 manager2;
    [SerializeField] GameObject Sword;
    [SerializeField] Text Score;
    [SerializeField] Text Prompt;
    [SerializeField] Text subPrompt;
    private int enemiesHit = 0;

    protected float spawnTimer = 0;
    protected int enemyCount = 0;
    float dx = 0;
    float dy = 0;
    /*to do:
     *-tweak rate of shooter spawns/cap at 5 -> make the preset positions just in the center
     *-control enemiesList from 
     *-add left right and back walls to destroy stray projectiles
     *-right now it feels like you REALLY gotta try to hit your enemies
    */
    float targetSwordAngle = 0f;
    void Awake()
    {
        manager2 = GetComponent<manager2>();
        Prompt.text = "Block the lasers for as long as you can! Try to deflect them back at your attackers.";
        subPrompt.text = "Move your mouse left to right across the screen to control your sword!";
       
    }
    private void Start()
    {
        enemies.Add(new Enemy(false, 18));
        enemies.Add(new Enemy(false, 12));
        enemies.Add(new Enemy(false, 6));
        enemies.Add(new Enemy(false, 0));
        enemies.Add(new Enemy(false, -6));
        enemies.Add(new Enemy(false, -12));
        enemies.Add(new Enemy(false, -18));
    }

    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit raycastHit))
        {
            dx = raycastHit.point.x;
            dy = raycastHit.point.y - 2.5f;//2.5 =ycoordinate of rotation for sword
        }
        
        float targetSwordAngle = Mathf.Atan2(dx, dy) * 250 / Mathf.PI;
        
        //transform.RotateAround(new Vector3(0,.5f,0), Vector3.back ,targetSwordAngle-currentSwordAngle);
        /*
        if (manager2.connected) 
        {
            targetSwordAngle = manager2.datum;
        }*/
        Sword.transform.rotation = Quaternion.Euler(0, 0, -targetSwordAngle);

        //down the road we can spawn them s.t. they don't overlap
        //possibly by assigning them to a an open "bin" which is associated with an X coordinate

        if (!dead)
        {
            spawnTimer += Time.deltaTime; //reset this
            if (spawnTimer > 12)
            {
                Prompt.text = "";
                subPrompt.text = "";
                spawnTimer = 0;
                //if (enemyCount < 6)
                //enemyCount++;
                int enemyIndex = spawnPos();
                Instantiate(Shooter, new Vector3(enemies[enemyIndex].positionValue, 3f, 25), Quaternion.identity, gameObject.transform);
            }
        }
    }

    void ShooterHit(float xpos) 
    {
         int i = ((int)xpos - 12) / (-4);
        enemies[i].positionActive = false;
        enemiesHit++;
        Score.text = enemiesHit.ToString();
    }

    void Dead() 
    {
        Prompt.text = "You were destroyed by lasers! Hit Reset to try again.";
        dead = true;
        BroadcastMessage("PlayerDead");
    }

    private int spawnPos() 
    {
        int i = Random.Range(0,6);
        if (enemies[i].positionActive)
        {
            i = spawnPos();
        }
        else 
        {
            enemies[i].positionActive = true;
        }

        return i;
    }

    public void Reset()
    {
        Prompt.text = "Block the laser balls for as long as you can! Try to deflect them back at your attackers.";
        dead = false;
        spawnTimer = 0;
        enemiesHit = 0;
        Score.text = enemiesHit.ToString();

        foreach(Enemy enemy in enemies)
        {
            enemy.positionActive = false;
        }

        BroadcastMessage("ResetGame");
    }

}
