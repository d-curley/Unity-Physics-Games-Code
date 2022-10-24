using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temp_Object_Control : MonoBehaviour
{
    protected float position = 0;
    protected float Mposition = 0;
    [SerializeField] Rigidbody Book;
    [SerializeField] float amp=50;
    [SerializeField] Rigidbody player;
    [SerializeField] GameObject Monkey;
    
    [SerializeField] GameObject LeftHoop;
    BookScore LeftScore;
    [SerializeField] GameObject RightHoop;
    BookScore RightScore;
    private bool gameStop = false;

    protected float timer=0;
    protected float velocity = 3.5f;
    
    public GameObject Floor;
    BookBreaker BookTracker;
    public Text Fails;
    public Text ResultText;




    /*
     * To do list:
     * 1) text in cen ter for win or loss X
     * 2) Way to "end" the game X
     *      -possibly a boolean that when true nothing moves?
     * 3) way to reset game
     *      -reset all text X
     *      -reset all variables X
     *      -delete all books (cycle through children) -> need to do this without destroying my game controller
     * 4) UI(?) meter for filling each bin, controlled by respective hoop object
     */

    public void Start()
    {
        BookTracker=Floor.GetComponent<BookBreaker>();
        LeftScore = LeftHoop.GetComponent<BookScore>();
        RightScore = RightHoop.GetComponent<BookScore>();

        Fails.text = "0";
        ResultText.text = "Fill the bins!";
    }

    public void Reset()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        /*gameStop = false;
        BookTracker.fails = 0;
        RightScore.Score = 0;
        LeftScore.Score = 0;
        ResultText.text = "";
        //here I could track previous positions in a list and cycle through in sync with the book
        foreach (Transform child in GetComponentsInChildren<Transform>()) 
        {
            Destroy(child.gameObject);//this is destorying my game controller too! I mean, the nerve!
        }*/
    }

    void Update()
    {
        if(LeftScore.Score==1|| RightScore.Score == 1 || BookTracker.fails == 1) { ResultText.text = ""; }
        Fails.text = BookTracker.fails.ToString();

        /*Move empty target, force is proportional to object distance
            * note: this method was way to slow and didn't properly correlate
            * player input to object movement
        float movement = Input.GetAxis("Horizontal");
        position += movement;//replace with BLE value (scaled to -8->8)
        position = Mathf.Clamp(position, -8, 8);
        transform.localPosition = new Vector3(position, -1f, 0f);
        float force = transform.localPosition.x - player.transform.localPosition.x;
        */

        float force = Input.GetAxis("Horizontal");

        player.AddForce(new Vector3(force * amp, 0));//control amp in inspector for quick changes

        //new Monkey Mover, still gets stuck on edges sometimes
        Mposition += velocity * Time.deltaTime; //move variable
        if (Mathf.Abs(Mposition) > 6) { velocity = -velocity; }//turn around if we've gone too far
        else if (Random.Range(0f, 10f) > 9.95f) { velocity = -velocity; }//flip directions @random
        Monkey.transform.localPosition = new Vector3(Mposition, 5.2f, 1.5f);

        //Book Release Timer
        timer += Time.deltaTime;
        if (timer > 4)
        {
            timer = 0;
            Instantiate(Book, new Vector3(Mposition, 4.5f, 0), Quaternion.identity, transform);
        }
        if (LeftScore.Score > 5 || RightScore.Score > 5) 
        { 
            ResultText.text = "You won!";
        }
        if (BookTracker.fails >= 5) 
        { 
            ResultText.text = "You lost!";
        }

            
    }
}
