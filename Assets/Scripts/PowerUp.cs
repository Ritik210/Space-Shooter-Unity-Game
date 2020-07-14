using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    private Player player;
    [SerializeField]
    private int PowerUpID;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
     //   transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, 0);
        if (transform.position.y < -9.5)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position);
           /* if (player != null)
            {
                if (PowerUpID == 0)
                {
                    player.TripleShotActive();
                }
                else if (PowerUpID == 1)
                {
                    Debug.Log("Speed up collected");
                }
                else if(PowerUpID == 2)
                {
                    Debug.Log("Shield collected");
                }
            }*/
            switch(PowerUpID)
            {
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    //Debug.Log("speed");
                    player.SpeedUpActive();
                    break;
                case 2:
                    player.shieldActive();
                    break;
                default:
                    Debug.Log("Default");
                    break;

            }
            Destroy(this.gameObject);
        }
    }
}
