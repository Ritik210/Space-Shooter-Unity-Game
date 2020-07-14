using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _TripleShotprefab;
    [SerializeField]
    private GameObject _SpeedUpPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedUpActive = false;
    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _laserSoundClip;
   
    private AudioSource _audioSource;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>(); 
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

// Update is called once per frame
 void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }



    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(horizontalInput, VerticalInput, 0);
        transform.Translate(Direction * _speed * Time.deltaTime);
       // transform.Translate(Direction * _speed * Time.deltaTime);
       
       transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -8.8f, 0f), 0);

        if (transform.position.x >= 18.5f)
        {
            transform.position = new Vector3(18.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -18.5f)
        {
            transform.position = new Vector3(-18.5f, transform.position.y, 0);
        }

    }
    void FireLaser()
    {
     _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_TripleShotprefab, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        else
        _lives--;
        _uiManager.UpdateLives(_lives);

        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        if(_lives<1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {

        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedUpActive()
    {
        _isSpeedUpActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedUpPowerDown());

    }
    IEnumerator SpeedUpPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedUpActive = false;
        _speed /= _speedMultiplier;
    }

    public void shieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
       // _speed *= _speedMultiplier;
       // StartCoroutine(shieldDown());

    }
    IEnumerator shieldDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldActive = false;
        _speed /= _speedMultiplier;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }



}
