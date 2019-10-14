using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGloves : MonoBehaviour
{
    enum State { WAIT, PUNCH, RECOIL}

    public float punchTime;
    public float waitTime;
    public float recoilTime;

    public float distance;

    float punchCounter;
    float waitCounter;
    float recoilCounter;

    Vector3 originalPosition;
    Vector3 endPosition;

    State actualState;

    private void Awake()
    {
        punchCounter = 0;
        waitCounter = 0;
        recoilCounter = 0;

        //different moments for each glove
        waitCounter = UnityEngine.Random.Range(0, waitTime);

        actualState = State.WAIT;

        originalPosition = transform.position;
        endPosition = originalPosition + transform.right * distance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(actualState)
        {
            case State.WAIT:
                WaitUpdate();
                break;
            case State.PUNCH:
                PunchUpdate();
                break;
            case State.RECOIL:
                RecoilUpdate();
                break;
        }
    }

    void WaitUpdate()
    {
        waitCounter += Time.deltaTime;

        if(waitCounter >= waitTime)
        {
            waitCounter = 0;
            actualState = State.PUNCH;
        }
    }

    void PunchUpdate()
    {
        punchCounter += Time.deltaTime;

        if(punchCounter >= punchTime)
        {
            punchCounter = punchTime;
        }

        transform.position = Vector3.Lerp(originalPosition, endPosition, punchCounter / punchTime);

        if(punchCounter >= punchTime)
        {
            punchCounter = 0;
            actualState = State.RECOIL;
        }
    }

    void RecoilUpdate()
    {
        recoilCounter += Time.deltaTime;

        if (recoilCounter >= recoilTime)
        {
            recoilCounter = recoilTime;
        }

        transform.position = Vector3.Lerp(endPosition, originalPosition, recoilCounter / recoilTime);

        if (recoilCounter >= recoilTime)
        {
            recoilCounter = 0;
            actualState = State.WAIT;
        }
    }
}
