using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class AICharacterControl : MonoBehaviour
    {
        private AISpawner ais;
        private Animator anim;

        public UnityEngine.AI.NavMeshAgent
            agent { get; private set; } // the navmesh agent required for the path finding

        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target; // target to aim for

        private void Start()
        {
            ais = Camera.main.GetComponent<AISpawner>();
            anim = GetComponent<Animator>();

            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update()
        {
            if (!target)
            {
                target = ais.patrolLocs[Random.Range(0, ais.patrolLocs.Length)];
                print("A");
            }
            
            if (agent.updatePosition)
            {
                agent.SetDestination(target.position);
            }
            
            character.Move(agent.desiredVelocity, false, false);

        }
    }
}
