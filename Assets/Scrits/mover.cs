using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class mover : MonoBehaviour
{
    public Rigidbody rb;

    public float runSpeed = 500f;
    public float strafeSpeed = 500f;
    public float jumpForce = 15f;

    private float _horizontalKey = 0f;

    protected bool strafeLeft = false;
    protected bool strafeRight = false;
    protected bool doJump = false;

    private Vector3 _startPosition;

    private Quaternion _startRotation;

    private SpawnManager _spawnManager;

    private bool _startGame = true;

    public GameObject startText;

    [SerializeField] private int _life = 1;
    [SerializeField] private SkinnedMeshRenderer _slimeBody;
    [SerializeField] private bool _isGOD = false;

    [SerializeField] private int _count = 100;

    [SerializeField] private AudioClip _jumpAuidioClip;
    private AudioSource _audioSource;

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL");
        }

        _slimeBody = transform.Find("Slime_01").GetComponent<SkinnedMeshRenderer>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("ETL AudioSourse");
        }
        else
        {
            _audioSource.clip = _jumpAuidioClip;
        }


    }

    void  Update()
    {
        _horizontalKey = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("space"))
        {
                doJump = true;
            Debug.Log(doJump);
        }
        if (transform.position.y < -1f)  ResetLevel();


       
    }

    public void SetSpeed(float speed)
    {
        runSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("barrier") && !_isGOD)
        {
            if (_life == 0)
            {
                ResetLevel();
            }
            else
            {
                _life--;
                transform.Find("Rig/Bone/Crown").gameObject.SetActive(false);
                StartCoroutine(BlinkCollaider());
            }
            
        }
    }

    private IEnumerator BlinkCollaider()
    {
        _isGOD = true;
        runSpeed = 6f;
        //yield return new WaitForSeconds(3f);
        for (int i = 0; i < 6; i++)
        {
            _slimeBody.enabled = false;
            yield return new WaitForSeconds(0.3f);
            _slimeBody.enabled = true;
            yield return new WaitForSeconds(0.3f);
        }
        _isGOD = false;
    }

    public int GetLife()
    {
        return _life;
    }

    private void ResetLevel()
    {
        int currentScore = (int)transform.position.z - 2;
        int bestScore = PlayerPrefs.GetInt("BestScore");
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
        SceneManager.LoadScene(0);
        //transform.position = _startPosition;
        //transform.rotation = _startRotation;
        //rb.velocity = Vector3.zero;
        //_spawnManager.ResetFloorSpawning();
    }
    private void FixedUpdate()
    {
        
        rb.AddForce(0,0,runSpeed * Time.deltaTime);
        if (runSpeed != 0)
        {
            if(transform.position.z > _count)
            {
                ++runSpeed;
                _count += 100;
            }
                
        }
        rb.MovePosition(transform.position + Vector3.forward * runSpeed * Time.deltaTime);

        if (_horizontalKey > 0 && transform.position.x < 4.42f)
        {
            rb.AddForce(strafeSpeed * Time.deltaTime,0,0,ForceMode.VelocityChange);
        }
        if (_horizontalKey < 0 && transform.position.x > -4.42f)
        {
            rb.AddForce(-strafeSpeed * Time.deltaTime,0,0,ForceMode.VelocityChange);
        }

        if (doJump && transform.position.y < 0.64)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            doJump = false;
            _audioSource.Play();
            Debug.Log(doJump);

        }

    }

    public float ReturnSpeed()
    {
        return runSpeed;
    }

   
}
