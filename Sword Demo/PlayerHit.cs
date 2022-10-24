using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 200;
    float hBarCenter = 140;
    [SerializeField] GameObject image;
    [SerializeField] int damage = 20;
    RectTransform HBar;
    [SerializeField] Image healthFlash;
    Coroutine currentFlashRoutine;
    [SerializeField] float flashTime=.2f;

    public void Awake()
    {
        HBar = image.GetComponent<RectTransform>();
    }



    void ResetGame()
    {
        health = 200;
        hBarCenter = 140;
        HBar.sizeDelta = new Vector2(50, health);
        HBar.transform.position = new Vector3(50, hBarCenter, 0);

    }

    public void OnCollisionEnter(Collision myCol)
    {
        if (myCol.gameObject.name == "Projectile(Clone)")
        {
            Destroy(myCol.gameObject);
            health -=damage;
            hBarCenter -= damage/2;
            HBar.sizeDelta = new Vector2(50, health);
            HBar.transform.position = new Vector3(50, hBarCenter, 0);
           // if (currentFlashRoutine != null)
            //{
              //  StopCoroutine(currentFlashRoutine); //sometime the screen stays tinted red
            //}
            currentFlashRoutine = StartCoroutine(Flash());
        }

        if (health < 1) 
        { SendMessageUpwards("Dead"); }
    }

    IEnumerator Flash()
    {
        for(float t=0; t < flashTime; t += Time.deltaTime) 
        {
            Color colorThisFrame = Color.red;
            colorThisFrame.a = Mathf.Lerp(0, .7f, t / flashTime);
            healthFlash.color = colorThisFrame;
            yield return null;
        }

        for (float t = 0; t < flashTime; t += Time.deltaTime)
        {
            Color colorThisFrame = Color.red;
            colorThisFrame.a = Mathf.Lerp(.7f,0, t / flashTime);
            healthFlash.color = colorThisFrame;
            yield return null;
        } 
    }
}
