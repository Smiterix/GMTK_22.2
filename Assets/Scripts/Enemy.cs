using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Animancer;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
    public int health = 100;
    public float meleeDistanceThreshhold = 1f;
    public float runSpeed = 1f;
    public AnimationClip[] attackings;
    public AnimationClip death;
    public AnimationClip run;
    public AnimationClip hit;
    public AnimancerComponent animancer;
    public RichAI richAI;
    public Transform player;
    float initialMaxVelocity = 0f;
    public SkinnedMeshRenderer mr;
    public Material standard;
    public bool alive = true;
    public bool materialzed = false;
    bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        initialMaxVelocity = richAI.maxSpeed;
        playWalk();
        transform.localScale = Vector3.one * Random.Range(.8f, 1.1f);
    }

    void playWalk()
    {
        var state = animancer.Play(run);
        state.Speed = runSpeed;
        state.Time = Random.Range(0, state.Clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;

        richAI.destination = player.position;

        if ((player.position - transform.position).magnitude < meleeDistanceThreshhold && !attacking)
        {
            performAttack();
        }
    }

    void performAttack()
    {
        attacking = true;
        richAI.maxSpeed = 0f;

        var state = animancer.Play(attackings[Random.Range(0, attackings.Length)]);

        state.Events.OnEnd += tryDamage;
        state.Events.OnEnd += resetMovement;

    }



    public void takeDamage(int damage)
    {
        if (!materialzed) return;

        health -= damage;

        if (health <= 0) die();

    }

    void die()
    {
        var state = animancer.Play(death);
        richAI.maxSpeed = 0;
        alive = false;
    }

    void tryDamage()
    {
        if ((player.position - transform.position).magnitude < meleeDistanceThreshhold * 1.5f && attacking)
            PlayerController.inst.takeDamage(damage);
    }

    public void dematerialize()
    {
        if (materialzed) return;
        materialzed = true;
        richAI.maxSpeed = 0;
        var state = animancer.Play(hit);
        state.Events.OnEnd = resetMovement;
        mr.material = standard;

    }

    void resetMovement()
    {
        richAI.maxSpeed = initialMaxVelocity;
        playWalk();
    }
}
