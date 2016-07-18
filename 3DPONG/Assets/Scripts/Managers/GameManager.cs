using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Transform RBObj;
    private Transform RBTran;


    public void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("PauseUI is already in play. Deleting old Instantiating new.");
            Destroy(gameObject);
        }
        else
            Instance = this;


        RBTran = RBObj;
        BallNew();
    }

    public void BallReset()
    {
        Destroy(RBTran.gameObject);
        RBTran = null;
        RBTran = RBObj;
        BallNew();
    }

    public void BallNew()
    {
        RBTran = (Transform)Instantiate(RBObj, new Vector3(0, 0, 0), Quaternion.identity);
        RBTran.gameObject.SetActive(true);
    }
}
