using UnityEngine;

public class Avatar : MonoBehaviour
{

    public ParticleSystem shape, trail, burst , wave;

    private Player player;

    public float deathCountdown = -1f;
    public GameObject user1, user2;
    public bool pause = false;
    public float user1size = 80.0f , user2size = 1.0f;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
        player.food = true;
        PlayerPrefs.SetInt("day", 1);
        PlayerPrefs.Save();

        user1.transform.parent = this.transform.parent;
        user1.active = true;
        user2.active = false;

        wave.transform.localScale = new Vector3(20.0f, 20.0f, 20.0f);

    }

    private void Start()
    {
        user1.transform.localScale = new Vector3(user1size, user1size, user1size);
        user2.transform.localScale = new Vector3(user2size, user2size, user2size);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (deathCountdown < 0f)
        {
            shape.enableEmission = false;
            trail.enableEmission = false;

            burst.Emit(burst.main.maxParticles);
            deathCountdown = burst.startLifetime;
            Debug.Log(collider.tag);

            if (PlayerPrefs.GetInt("day") == 1)
            {
                if (collider.tag.Equals("food"))
                {
                    player.food = true;
                    player.velocity += 0.5f;
                    user1size += 5.0f;
                    user2size += 0.1f;
                    

                    if (user1size < 80.0f){
                        user1.transform.localScale = new Vector3(user1size, user1size, user1size);
                    }

                    if (user2size < 5.0f)
                    {
                        user2.transform.localScale = new Vector3(user2size, user2size, user2size);
                    }
                    
                }
                else
                {
                    player.food = false;
                    if (player.velocity > 2)
                    {
                        player.velocity -= 1f;
                        user1size -= 5.0f;
                        user2size -= 0.1f;
                        if (user1size > 50.0f)
                        {
                            user1.transform.localScale = new Vector3(user1size, user1size, user1size);
                        }

                        if (user2size > 1.0f)
                        {
                            user2.transform.localScale = new Vector3(user2size, user2size, user2size);
                        }
                    }
                }
            } else {
                if (!collider.tag.Equals("food"))
                {
                    player.food = false;
                    player.velocity += 0.5f;
                    user1size += 5.0f;
                    user2size += 0.1f;

                    if (player.power < 5)
                    {
                        player.power++;
                    }

                    if (user1size < 100.0f)
                    {
                        user1.transform.localScale = new Vector3(user1size, user1size, user1size);
                    }

                    if (user2size < 7.0f)
                    {
                        user2.transform.localScale = new Vector3(user2size, user2size, user2size);
                    }
                }
                else
                {
                    player.food = true;
                    if (player.velocity > 2)
                    {
                        player.velocity -= 1f;
                        user1size -= 5.0f;
                        user2size -= 0.1f;
                        if (user1size > 50.0f)
                        {
                            user1.transform.localScale = new Vector3(user1size, user1size, user1size);
                        }

                        if (user2size > 1.0f)
                        {
                            user2.transform.localScale = new Vector3(user2size, user2size, user2size);
                        }
                    }
                }
            }

            Destroy(collider.gameObject);
        }
    }


    private void Update()
    {
        user1.transform.Rotate(new Vector3(1f, 1f, 1f), 90 * Time.deltaTime, Space.Self);
        user2.transform.Rotate(new Vector3(1f, 1f, 1f), 90 * Time.deltaTime, Space.Self);

        if (Input.GetKey(KeyCode.Escape))
        {
            //player.Die();
            if (pause) {
                Time.timeScale = 1;
                pause = false;
            } else
            {
                Time.timeScale = 0;
                pause = true;
            }
        }

        if (Input.GetKey(KeyCode.Return))
        {
            player.Die();
        }

            if (Input.GetKey(KeyCode.Space)){

            if (PlayerPrefs.GetInt("day") == 0)
            {
                if (player.power > 0)
                {
                   
                    wave.Emit(wave.main.maxParticles);
                    player.power = player.power - 1;

                    GameObject[] germs = GameObject.FindGameObjectsWithTag("germ");

                    for (int i = 0; i < germs.Length; i++)
                    {
                        Destroy(germs[i]);
                    }
                }
            }
            
            //player.GetComponent<Rigidbody>().AddForce(this.transform.up * 1000);

            //this.transform.Translate(new Vector3(0f, 1f, 0f));
            //user1.transform.Translate(new Vector3(0f, 1f, 0f));
            //user2.transform.Translate(new Vector3(0f, 1f, 0f));

            //thread t2 = new thread(delegate ()
            //{
            //    this.transform.translate(new vector3(0f, -1f, 0f));
            //    user1.transform.translate(new vector3(0f, -1f, 0f));
            //    user2.transform.translate(new vector3(0f, -1f, 0f));
            //});
            //t2.start();

        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    this.transform.Translate(new Vector3(0f, -1f, 0f));
        //    user1.transform.Translate(new Vector3(0f, -1f, 0f));
        //    user2.transform.Translate(new Vector3(0f, -1f, 0f));
        //}

        if (deathCountdown >= 0f)
        {
            deathCountdown -= Time.deltaTime;
            if (deathCountdown <= 0f)
            {
                deathCountdown = -1f;
                shape.enableEmission = true;
                trail.enableEmission = true;

                if (PlayerPrefs.GetInt("day") == 1)
                {
                    if (player.food)
                    {
                        if (player.life < 5)
                        {
                            player.life++;
                            player.distanceTraveled += 50;
                            Debug.Log("life must increase");
                        }
                    }
                    else
                    {
                        player.life = player.life -2;
                        player.distanceTraveled -= 50;
                        Debug.Log("life decrice");
                    }
                }
                else
                {
                    if (!player.food)
                    {
                        if (player.life < 5)
                        {
                            player.life++;
                            player.distanceTraveled += 50;
                            Debug.Log("you hit germ, life increase");
                        }
                    }
                    else
                    {
                        player.life = player.life - 2;
                        player.distanceTraveled -= 50;
                        Debug.Log("you hit food,life decrece");
                    }
                }

                if (player.life > 0)
                {

                    if (PlayerPrefs.GetInt("day") == 1)
                    {
                        if (player.food)
                        {
                            user1.transform.parent = this.transform.parent;
                            user1.active = true;
                            user2.active = false;

                            PlayerPrefs.SetInt("day", 1);
                            PlayerPrefs.Save();
                        }
                        else
                        {
                            user2.transform.parent = this.transform.parent;
                            user1.active = false;
                            user2.active = true;

                            PlayerPrefs.SetInt("day", 0);
                            PlayerPrefs.Save();
                        }
                    }
                    else
                    {
                        if (!player.food)
                        {
                            user2.transform.parent = this.transform.parent;
                            user1.SetActive( false);
                            user2.SetActive (true);

                            PlayerPrefs.SetInt("day", 0);
                            PlayerPrefs.Save();
                        }
                        else
                        {
                            user1.transform.parent = this.transform.parent;
                            user1.SetActive(true);
                            user2.SetActive(false);

                            PlayerPrefs.SetInt("day", 1);
                            PlayerPrefs.Save();
                        }
                    }

                }
                else
                {
                    player.life = 3;
                    player.power = 3;
                    player.food = true;
                    PlayerPrefs.SetInt("day", 1);
                    PlayerPrefs.Save();
                    user1.active = true;
                    user2.active = false;
                    player.Die();
                }
            }
        }
    }
}