using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{

    //for debugging only
    public float axisValue;

    public float maxSpeed;
    public float maxHealth;
    public Vector3 startingPosition;

    public float currentHealth;

    public float rotationSpeed;
    public Boolean idle = false;
    public Boolean canMove = true;
    public Boolean canRotate = true;
    public int nbBulletsLeft = 2;
    public int nbExplosiveBulletsLeft = 2;
    public int nbShotGunBulletsLeft = 0;
    public int money = 0;
    public int maxBullets = 6;


    public EndOfRoundScreen endOfRoundScreen;

    // handle the weapons cooldown
    public float coolDownPeriodInSeconds = 2f;
    private float timeStamp = -1f;
    private float explTimeStamp = -1f;
    private CameraShaker camShakeshake;
    public string horizontalMvtAxisName = "Horizontal_T_P1";
    public string verticalMvtAxisName = "Vertical_T_P1";
    public string horizontalRotationAxisName = "Horizontal_R_P1";
    public string verticalRotationAxisName = "Vertical_R_P1";
    public string shootButtonName = "Fire_P1";
    public string backButtonName = "Back";

    public string playerName = "plop2";
    public string playerNameEnemy = "plop";
    private PlayerIndex dildoControl;

    private Color32 black = new Color32(0, 0, 0, 100);
    private Color32 selectedColor = new Color32(255, 255, 0, 100);

    public Image imgBulletOne, imgBulletTwo, imgBulletThree, imgBulletFour, imgBulletFive, imgBulletSix;

    public Text moneyText;

    public GameObject bullet;
    public GameObject explosiveBullet;

    public GameObject explosion;
    public GameObject explosionOfBullet;
    public Boolean thereIsStillANonExplodedBulletTravelling = false;

    public GameObject currentExplodingBullet;

    Animator animator;

    // Boolean to indicate gender: true (male) false(female)
    [SerializeField]
    private bool isMale = true;

    //SHOP AXIS

    public string dPadXAxis = "dPadXAxis";
    public string dPadYAxis = "dPadYAxis";
    private float xAxis;
    private float yAxis;
    public bool canBuy = false;
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        animator = gameObject.GetComponent<Animator>();
        camShakeshake = Camera.main.GetComponent<CameraShaker>();
    }


    private void FixedUpdate()
    {

        if (canMove)
        {
            float valueH = Input.GetAxis(horizontalMvtAxisName);
            float valueV = Input.GetAxis(verticalMvtAxisName);
            //each bullet slows down the character by 5%
            transform.Translate((new Vector3(valueH, 0f, valueV)).normalized * maxSpeed * (1f - (nbShotGunBulletsLeft + nbExplosiveBulletsLeft + nbBulletsLeft) * 0.05f) * Time.fixedDeltaTime, Space.World);
        }
        if (canRotate)
        {
            float valueH = Input.GetAxis(horizontalRotationAxisName);
            float valueV = Input.GetAxis(verticalRotationAxisName);

            if (valueH != 0f || valueV != 0f)
            {
                float angle = Mathf.Atan2(valueV, valueH) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.up);

            }
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            print("entered collsion with a bullet");
            this.OnTouchedByBullet();
        }

        else if (collision.gameObject.tag == "Money")
        {
            money++;
            Destroy(collision.gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BasicShop")
        {
            canBuy = true;
            Debug.Log("Entering Shop");
            /*Debug.Log("Entering Shop");
            if (money > 0 && nbBulletsLeft < maxBullets)
            {
                money--;
                nbBulletsLeft++;
            }*/
        }
        /*else if (other.gameObject.tag == "ExplosiveShop")
        {
            Debug.Log("entering explosive shop");
            if (money >= 3)
            {
                nbExplosiveBulletsLeft++;
                money -= 3;
            }
        }
        else if (other.gameObject.tag == "ShotGunShop")
        {
            if (money >= 2)
            {
                nbShotGunBulletsLeft++;
                money -= 2;
            }
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BasicShop")
        {
            canBuy = false;
        }
    }


    public void pickUp()
    {
        nbBulletsLeft++;
    }

    public void OnTouchedByBullet()
    {
        maxSpeed = 0;
        Debug.Log("Touched by bullet ! " + playerNameEnemy);
        currentHealth = 0;
        //this.gameObject.SetActive(false);
        animator.SetTrigger(Animator.StringToHash("Death"));
        endOfRoundScreen.LaunchEndOfRound(playerNameEnemy);
        autoDisable disable = gameObject.GetComponent<autoDisable>();
        disable.autoFreez = true;
        disable.enabled = true;
        gameObject.gameObject.GetComponent<SphereCollider>().enabled = false;
        if (isMale)
        {
            AkSoundEngine.PostEvent("death_male", gameObject);
        }
        else
        {
            AkSoundEngine.PostEvent("death_female", gameObject);
        }
    }


    void Fire()
    {
        //camShakeshake.StartCoroutine(camShakeshake.shakshakshakira(0.2f));
        print("fire in the hole");
        nbBulletsLeft--;
        GameObject bulletClone = Instantiate(bullet, (transform.position + (3 * transform.forward) + new Vector3(0f, 0.5f, 0f)), transform.rotation);
        bulletClone.SetActive(true);
        bulletClone.GetComponent<Rigidbody>().velocity = transform.forward;
        explosion.SetActive(true);
        animator.SetTrigger(Animator.StringToHash("Fire"));


    }

    void ExplosiveFire(float explTimeStamp)
    {
        //camShakeshake.StartCoroutine(camShakeshake.shakshakshakira(1f));
        print("fire in the EXPLODING hole");
        nbExplosiveBulletsLeft--;
        currentExplodingBullet = Instantiate(explosiveBullet, (transform.position + 3 * transform.forward + new Vector3(0f, 0.5f, 0f)), transform.rotation);
        StartCoroutine("explodingBullet");
        currentExplodingBullet.SetActive(true);
        currentExplodingBullet.GetComponent<Rigidbody>().velocity = transform.forward;
        explosion.SetActive(true);

    }

    //coroutine for exploding bullets
    IEnumerator explodingBullet()
    {
        Debug.Log("On entre dans la routine de la mort");
        while ((Input.GetAxis(shootButtonName) <= 0.3 || Time.time < explTimeStamp))
        {
            Debug.Log("axis val " + Input.GetAxis(shootButtonName) + "  Time vs stamp " + Time.time + "  vs  " + explTimeStamp);
            yield return null;
        }
        //Debug.Log("prout");
        // yield return new WaitUntil(() => (Input.GetAxis(shootButtonName) >= 0.3 && Time.time > explTimeStamp) == true);
        //Detonate();
        //thereIsStillANonExplodedBulletTravelling = false;
        explosionOfBullet.transform.position = currentExplodingBullet.transform.position;
        explosionOfBullet.SetActive(true);
        int i = 0;
        float acc = 1f;
        //camShakeshake.StartCoroutine(camShakeshake.shakshakshakira(2f));
       
        /* while (acc < 2)
        {

            acc *= 1.03f;
            currentExplodingBullet.GetComponent<SphereCollider>().radius = acc;
            Debug.Log("  geom  " + currentExplodingBullet.GetComponent<SphereCollider>().radius);
            //currentExplodingBullet.GetComponentInChildren<SphereCollider>().radius = acc;
            yield return null;
        }*/
        currentExplodingBullet.GetComponent<SphereCollider>().radius = 15;
        Debug.Log("  geom  " + currentExplodingBullet.GetComponent<SphereCollider>().radius);
        yield return null;
        explosionOfBullet.SetActive(true);
        currentExplodingBullet.SetActive(false);
        Destroy(currentExplodingBullet);
        //yield return new WaitForSeconds(0.2f);
        thereIsStillANonExplodedBulletTravelling = false;
        Debug.Log("onj sort de la routine de la mort");
        Debug.Log("axis val " + Input.GetAxis(shootButtonName) + "  Time vs stamp " + Time.time + "  vs  " + explTimeStamp);

    }

    //pour les balles explosives
    void Detonate()
    {
        Debug.Log("entering detronate, so far so good");
        //DIRTY HACK AS DIRTY AS UR MUM (MAYBE EVEN WORSE) (NOT EVENT JOKING) (I SWEAR)

        //good version + add explosions particles
        /* this.gameObject.GetComponent<SphereCollider>();
         while (this.gameObject.GetComponent<SphereCollider>().radius < explosionRadius)
         {
             this.gameObject.GetComponent<SphereCollider>().radius += 0.01f;
         }*/

        //in case no enemy killed


    }

    // Update is called once per frame
    void Update()
    {

        xAxis = Input.GetAxis(dPadXAxis);
        yAxis = Input.GetAxis(dPadYAxis);

        shopManager();
        if (Input.GetAxis(shootButtonName) <= -0.3 && nbBulletsLeft > 0 && Time.time >= timeStamp)
        {
            StartCoroutine("myVibration");
            timeStamp = Time.time + coolDownPeriodInSeconds;
            print("HELLO");
            Fire();
        }

        if (Input.GetAxis(shootButtonName) >= 0.3 && nbExplosiveBulletsLeft > 0 && Time.time >= explTimeStamp && !thereIsStillANonExplodedBulletTravelling)
        {
            thereIsStillANonExplodedBulletTravelling = true;
            Debug.Log("Entered explosive LOOP MADAFAKA");
            StartCoroutine("myVibration");
            explTimeStamp = Time.time + coolDownPeriodInSeconds;
            ExplosiveFire(explTimeStamp);
        }

        //shortcut to end the round by killing the player
        if (Input.GetButtonDown("Fire3"))
        {
            OnTouchedByBullet();
        }

        if (Input.GetButtonDown(backButtonName))
        {
            Application.LoadLevel(0);
        }


        switch (nbBulletsLeft)
        {
            case 1:
                imgBulletOne.color = selectedColor;
                imgBulletTwo.color = imgBulletThree.color = imgBulletFour.color = imgBulletFive.color = imgBulletSix.color = black;
                break;
            case 2:
                imgBulletOne.color = imgBulletTwo.color = selectedColor;
                imgBulletThree.color = imgBulletFour.color = imgBulletFive.color = imgBulletSix.color = black;
                break;
            case 3:
                imgBulletOne.color = imgBulletTwo.color = imgBulletThree.color = selectedColor;
                imgBulletFour.color = imgBulletFive.color = imgBulletSix.color = black;
                break;
            case 4:
                imgBulletOne.color = imgBulletTwo.color = imgBulletThree.color = imgBulletFour.color = selectedColor;
                imgBulletFive.color = imgBulletSix.color = black;
                break;
            case 5:
                imgBulletOne.color = imgBulletTwo.color = imgBulletThree.color = imgBulletFour.color = imgBulletFive.color = selectedColor;
                imgBulletSix.color = black;
                break;
            case 6:
                imgBulletOne.color = imgBulletTwo.color = imgBulletThree.color = selectedColor;
                imgBulletFour.color = imgBulletFive.color = imgBulletSix.color = selectedColor;
                break;
            default:
                imgBulletOne.color = imgBulletTwo.color = imgBulletThree.color = black;
                imgBulletFour.color = imgBulletFive.color = imgBulletSix.color = black;
                break;
        }

        moneyText.text = "x" + money;

    }

    IEnumerator myVibration()
    {
        dildoControl = (PlayerIndex)(playerName == "P1" ? 0 : 1);
        GamePad.SetVibration(dildoControl, 1, 1);
        yield return new WaitForSeconds(.2f);
        GamePad.SetVibration(dildoControl, 0, 0);
    }


    void shopManager()
    {

        if (canBuy)
        {
            //Debug.Log("WELCOME TO THE SHOP");

            //Basic Bullets
            if (xAxis > 0 && money > 0 && nbBulletsLeft < maxBullets)
            {
                Debug.Log("Bought basic bullet!");
                money--;
                nbBulletsLeft++;
                canBuy = false;
                StartCoroutine(buyConfirmation());
                AkSoundEngine.PostEvent("money_exchange", gameObject);
            }

            //Explosive Bullets
            if (yAxis > 0 && money >= 3)
            {
                Debug.Log("Bought explosive bullet!!");
                nbExplosiveBulletsLeft++;
                money -= 3;
                canBuy = false;
                StartCoroutine(buyConfirmation());
                AkSoundEngine.PostEvent("money_exchange", gameObject);
            }

            //Shotgun Bullets
            if (yAxis < 0 && money >= 2)
            {
                Debug.Log("Bought shotgun shell!");
                nbShotGunBulletsLeft++;
                money -= 2;
                canBuy = false;
                StartCoroutine(buyConfirmation());
                AkSoundEngine.PostEvent("money_exchange", gameObject);
            }
            
        }

    }

    IEnumerator buyConfirmation()
    {
        yield return new WaitForSeconds(0.2f);
        canBuy = true;
    }
}
