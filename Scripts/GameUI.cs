using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    // Bullet Images
    public Image bullet1;
    public Image bullet2;
    public Image bullet3;
    public Image bullet4;
    public Image bullet5;
    public Image bullet6;

    
    public Color bulletColor;
    public Color noBulletColor;

    public void BulletUpdate(int nbBullet)
    {
        switch (nbBullet)
        {
            case 1:
                bullet1.color = bulletColor;
                bullet2.color = bullet3.color = bullet4.color = bullet5.color = bullet6.color = noBulletColor;
                break;
            case 2:
                bullet1.color =  bullet2.color = bulletColor;
                bullet3.color = bullet4.color = bullet5.color = bullet6.color = noBulletColor;
                break;
            case 3:
                bullet1.color =  bullet2.color =  bullet3.color = bulletColor;
                bullet4.color =  bullet5.color = bullet6.color = noBulletColor;
                break;
            case 4:
                bullet1.color =  bullet2.color = bullet3.color = bullet4.color = bulletColor;
                bullet5.color =  bullet6.color = noBulletColor;
                break;
            case 5:
                bullet1.color = bullet2.color = bullet3.color = bullet4.color =  bullet5.color = bulletColor;
                bullet6.color = noBulletColor;
                break;
            case 6:
                bullet1.color = bullet2.color = bullet3.color = bullet4.color = bullet5.color = bullet6.color = noBulletColor;
                break;
            default:
                bullet1.color = noBulletColor; bullet2.color = noBulletColor; bullet3.color = noBulletColor;
                bullet4.color = noBulletColor; bullet5.color = noBulletColor; bullet6.color = noBulletColor;
                break;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
