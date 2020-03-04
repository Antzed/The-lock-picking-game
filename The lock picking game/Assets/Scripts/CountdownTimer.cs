using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime = 0f;
    float startingTime = 20f;
    bool haveShowLoseScreen = false;

    [SerializeField] Text countdownText;
    [SerializeField] GameObject loseScreen;
    [SerializeField] TouchManager touchManager;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        loseScreen.GetComponent<MeshRenderer>().enabled = false;
        
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        
        if (touchManager.isShake)
        {
            currentTime -= 2;
            countdownText.text = currentTime.ToString("0");
        }
        countdownText.text = currentTime.ToString("0");



        if (currentTime <= 0 && !haveShowLoseScreen)
        {
            loseScreen.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(waitOneSecond(loseScreen));
            haveShowLoseScreen = true;
            GameObject timerUI = GameObject.Find("CountdownText");
            Destroy(timerUI);
        }

        
    }


    private void StartCoroutine(System.Func<IEnumerator> waitOneSecond)
    {
        throw new System.NotImplementedException();
    }

    IEnumerator waitOneSecond(GameObject linScreen)
    {
        yield return new WaitForSeconds(1f);
        linScreen.GetComponent<MeshRenderer>().enabled = false;
    }

}
