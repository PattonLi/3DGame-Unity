using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ShootController CrossBow;// 弓对象
    private bool atSpot = false;// 是否到达射击点
    private SpotController spot;// 射击位controller
    private TargetController[] targetControllers;//target controller
    SkyController skyController;

    void Start()
    {
        CrossBow = GetComponentInChildren<ShootController>();
        skyController = Singleton<SkyController>.Instance;
    }

    void Update()
    {
        //show shootNums
        if (atSpot)
        {
            Singleton<UserGUI>.Instance.SetIsAtSpot(true);
            Singleton<UserGUI>.Instance.SetShootNum(spot.shootNum);
            if (targetControllers != null)
            {
                int sumSpotScore = 0;
                foreach (TargetController targetController in targetControllers)
                {
                    sumSpotScore += targetController.scores;
                }
                Singleton<UserGUI>.Instance.SetSpotScore(sumSpotScore);
            }
        }
        else
        {
            Singleton<UserGUI>.Instance.SetIsAtSpot(false);
            Singleton<UserGUI>.Instance.SetShootNum(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 与射击点位撞击
        if (collision.gameObject.tag == "Spot")
        {
            
            Debug.Log("collide with spot");
            spot = collision.gameObject.GetComponentInChildren<SpotController>();
            atSpot = true;

            if (spot.shootNum > 0)
            {
                CrossBow.GetComponentInChildren<ShootController>().readyToShoot = true;
                CrossBow.GetComponentInChildren<ShootController>().shootNum = spot.shootNum;
                CrossBow.GetComponentInChildren<ShootController>().currentSpotController = spot;
            }

            // 切换天空盒
            skyController.ChangeBox();
            
            //update spot score
            targetControllers = spot.targetControllers; 
            if (targetControllers != null)
            {
                int sumSpotScore = 0;
                foreach (TargetController targetController in targetControllers)
                {
                    sumSpotScore += targetController.scores;
                }
                Singleton<UserGUI>.Instance.SetSpotScore(sumSpotScore);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Spot")
        {
            Debug.Log("collideExit with spot");
            CrossBow.GetComponentInChildren<ShootController>().readyToShoot = false;
            atSpot = false;

        }
    }

}
