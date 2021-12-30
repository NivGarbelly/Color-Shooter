using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBase : MonoBehaviour
{
   private GameManeger _gameManeger;

    public enum EnemyStates
    {
        Idle = 0,
        Patrol = 1,
        Follow = 2,
        NotFollow = 3
    }

    private NavMeshAgent agent;
    private bool isStateChanged = false;
    private EnemyStates prevState;
    [SerializeField] private EnemyStates currentState;
    [SerializeField] private bool idleIsDefault;
    private Transform player;
    [SerializeField] private float minDistToFollow;
    [SerializeField] private float speed;
    [SerializeField] private float lerpSpeed= 1f;
    private float startTime;
    [SerializeField] private AudioSource changeSound;
    [SerializeField] private AudioSource EnemiesSound;
    [SerializeField] private Transform[] patrolPoints;
    private int _currentPoint = 0;

    [SerializeField] private List<Vector3> patrolPositions;

    private void Awake()
    {
        changeSound = GetComponent<AudioSource>();
        _gameManeger = FindObjectOfType<GameManeger>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        startTime = Time.time;
        if (agent != null)
        {
            foreach (var point in patrolPoints)
            {
                patrolPositions.Add(point.position);
            }
            agent.speed = speed;
            SetStateToDefault();
        }
    }

    private void Update()
    {
        if (agent != null)
        {
            if (player != null)
            {
                CheckStateChange();
            }

            DoAccordingToState();
        }

        if (_gameManeger.isWin==true)
        {
            agent.speed = 0;
        }
        if (_gameManeger.isWin==true)
        {
            agent.speed = speed;
        }
    }

    private void CheckStateChange()
    {
        prevState = currentState;

        var distToPlayer = Vector3.Distance(transform.position, player.position);


        if (distToPlayer <= minDistToFollow)
        {
            currentState = EnemyStates.Follow;
        }
        else
        {
            SetStateToDefault();
        }

        isStateChanged = prevState != currentState;
    }

    private void DoAccordingToState()
    {
        if (agent != null)
        {
            switch (currentState)
            {
                case EnemyStates.Patrol:
                    DoPatrol();
                    break;

                case EnemyStates.Follow:
                    DoFollow();
                    break;

                case EnemyStates.NotFollow:
                    DoNotFollow();
                    break;

                case EnemyStates.Idle:
                default:

                    agent.isStopped = true;
                    break;

            }
        }
    }

    private void DoPatrol()
    {
        if (isStateChanged || (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0))
        {
            _currentPoint++;
            if (patrolPositions.Count <= _currentPoint)
            {
                _currentPoint = 0;
            }

            var newDest = patrolPositions[_currentPoint];
            agent.SetDestination(newDest);
        }
    }

    private void SetStateToDefault()
    {
        currentState = idleIsDefault ? EnemyStates.Idle : EnemyStates.Patrol;
        currentState = idleIsDefault ? EnemyStates.Idle : EnemyStates.Patrol;
    }



    private void DoFollow()
    {
        if (player != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    private void DoNotFollow()
    {
        if (player != null)
        {
            agent.isStopped = false;
            agent.SetDestination(-player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullets"))
        {
            if (other.gameObject.GetComponents<Renderer>() != null)
            {
                var otheRenderer = other.GetComponent<Renderer>();
                var cubeRenderer = GetComponentsInChildren<Renderer>();
                foreach (var ren in cubeRenderer)
                {
                    //ren.material.SetColor("_BaseColor", otheRenderer.material.GetColor("_BaseColor"));
                    float t = (Time.time - startTime) * lerpSpeed;
                    ren.material.color= Color.Lerp( ren.material.GetColor("_BaseColor"),otheRenderer.material.GetColor("_BaseColor"),t);
                }
                changeSound.Play();
            }
            _gameManeger.enemiesColors.Clear();
            _gameManeger.AddColorToList();
            agent.speed = 0;
            _gameManeger.HUDreset();
            _gameManeger.HUDTEST();
            Invoke("Resume",0.5f);
        }
    }
    private void Resume()
    {
     agent.speed = speed;
    }

    public void EnemyLose()
    {
        var cubeRenderer = GetComponentsInChildren<Renderer>();
        foreach (var ren in cubeRenderer)
        {
            ren.material.SetColor("_BaseColor", Color.gray);
            ren.material.SetFloat("_Smoothness", 0f);
        }
        EnemiesSound.Stop();
        var coll = GetComponent<Collider>();
        coll.enabled = false;
        agent.speed = 0;
        
    }
}