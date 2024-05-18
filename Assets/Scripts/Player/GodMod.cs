using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMod : MonoBehaviour
{
    enum State
    {
        Waiting,
        I,
        ID,
        IDD,
        IDDQ,
        IDDQD,
        Done
    }

    private Player player;
    private bool isGod;

    State currentState = State.Waiting;

    private float comboTime = 1.0f;

    private float waitTime = 0.0f;
    private void Start()
    {
        player = GetComponent<Player>();
        isGod = false;
    }
    void Update()
    {
        waitTime -= Time.deltaTime;

        switch (currentState)
        {
            case State.Waiting:
                if (Input.GetKey(KeyCode.I))
                {
                    currentState = State.I;
                    waitTime = comboTime;
                }
                break;

            case State.I:
                if (Input.GetKey(KeyCode.D) && waitTime > 0.0f)
                {
                    currentState = State.ID;
                    waitTime = comboTime;
                }
                else if (waitTime <= 0.0f)
                {
                    currentState = State.Waiting;
                }
                break;

            case State.ID:
                if (Input.GetKey(KeyCode.D) && waitTime > 0.0f)
                {
                    currentState = State.IDD;
                    waitTime = comboTime;
                }
                else if (waitTime <= 0.0f)
                {
                    currentState = State.Waiting;
                }
                break;

            case State.IDD:
                if (Input.GetKey(KeyCode.Q) && waitTime > 0.0f)
                {
                    currentState = State.IDDQ;
                    waitTime = comboTime;
                }
                else if (waitTime <= 0.0f)
                {
                    currentState = State.Waiting;
                }
                break;

            case State.IDDQ:
                if (Input.GetKey(KeyCode.D) && waitTime > 0.0f)
                {
                    currentState = State.IDDQD;
                    waitTime = comboTime;
                }
                else if (waitTime <= 0.0f)
                {
                    currentState = State.Waiting;
                }
                break;

            case State.IDDQD:
                currentState = State.Done;
                break;

            case State.Done:
                if (!isGod)
                {
                    player.health = 100000;
                    isGod = true;
                }
                else
                {
                    player.health = 1;
                    isGod = false;
                }
                currentState = State.Waiting;
                break;
        }
    }
}
