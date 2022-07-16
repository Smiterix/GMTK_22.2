using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Animancer;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float runSpeed = 1f;
    public AnimationClip death;
    public AnimationClip run;
    public AnimationClip hit;
    public AnimancerComponent animancer;
    public RichAI richAI;
    public Transform player;
    float initialMaxVelocity = 0f;
    public SkinnedMeshRenderer mr;
    public Material standard;
    public bool materialzed = false;
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
        state.NormalizedTime = Random.Range(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        richAI.destination = player.position;
    }

    public void takeDamage(int damage)
    {
        if (!materialzed) return;

        health -= damage;

        if (health <= 0) die();

    }

    void die()
    {
        animancer.Play(death);
        richAI.maxSpeed = 0;
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
