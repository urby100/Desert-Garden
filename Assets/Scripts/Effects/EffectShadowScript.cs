using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EffectShadowScript : MonoBehaviour
{
    public Particle[] m_Particles;
    public ParticleSystem m_System;
    public int numParticlesAlive;
    public List<Particle> alive;
    public List<GameObject> particleShadows;

    // Start is called before the first frame update
    void Start()
    {
        m_System = GetComponent<ParticleSystem>();
        m_Particles = new Particle[m_System.main.maxParticles];
        alive = new List<Particle>();
    }

    // Update is called once per frame
    void Update()
    {/*
        numParticlesAlive = m_System.GetParticles(m_Particles);
        if (numParticlesAlive !=null)
        {
            for (int i = 0; i < numParticlesAlive; i++)
            {
                
               
            }
        }*/
    }
    void FixedUpdate()
    {
    }
}
