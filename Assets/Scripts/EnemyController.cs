using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool broken = true;

    public ParticleSystem smokeEffect;
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f; //that will be the time before you reverse the enemy’s direction.


    new Rigidbody2D rigidbody2D;
    float timer; //that will keep the current value of the timer.
    int direction = 1; //that is the enemy’s current direction

    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }


        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        broken = false;
        animator.SetTrigger("Fixed");
        rigidbody2D.simulated = false;
        smokeEffect.Stop();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
