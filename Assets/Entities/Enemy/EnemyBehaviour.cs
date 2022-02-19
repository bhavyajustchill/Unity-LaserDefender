using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 150f;
    public GameObject projectile;
    public float projectileSpeed = 10;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        Projectile missile = collision.gameObject.GetComponent<Projectile>();

        if(missile)
        {
            health -= missile.GetDamage();

            if(health <= 0)
            {
                Die();
            }
            Debug.Log("Projectile Hit");
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        float probabilty = Time.deltaTime * shotsPerSecond;
        if(Random.value<probabilty)
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
