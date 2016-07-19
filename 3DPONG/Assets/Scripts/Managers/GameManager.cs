using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public GameObject RBObj;
    private Transform RBTran;


    public void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Game Manager is already in play. Deleting old Instantiating new.");
            Destroy(gameObject);
        }
        else
            Instance = this;
        
        RBObj = Resources.Load("Prefabs/RollerBall", typeof(GameObject)) as GameObject;
        RBTran = RBObj.transform.GetComponent<Transform>();
        BallNew();
    }

    public void BallReset()
    {
        Destroy(RBTran.gameObject);
        RBTran = null;
        RBTran = RBObj.transform;
        BallNew();
    }

    public void BallNew()
    {
        RBTran = Instantiate(RBObj, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
        
        //if(!RBTran.gameObject.activeInHierarchy)
        //  RBTran.gameObject.SetActive(true);
    }
}
