
using Cinemachine;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 movement;
    private Camera cam;
    private Vector3 mousePos;
    [SerializeField] private GameObject bullet;
    private Transform shootPoint;
    private GameManeger gameManeger;
    [SerializeField] private GameObject colorIndicator;
    [SerializeField] private GameObject playerArt;
    private AudioSource movementSound;
    private Rigidbody rigidbody;
    private void Awake()
    {
        movementSound = GetComponent<AudioSource>();
        shootPoint = GameObject.FindGameObjectWithTag("ShootPoint").transform;
        gameManeger = FindObjectOfType<GameManeger>();
        cam = FindObjectOfType<Camera>();
        rigidbody= GetComponent<Rigidbody>();
        var render = colorIndicator.GetComponent<Renderer>();
        render.material.SetColor("_BaseColor", GameManeger.colors[GameManeger.bulletNextColor]);
    }


    private void FixedUpdate() 
    {
                    playerMovement();
                    playerRotation(); 
        }     
    private void playerMovement()
    {
        Vector3 moveInput = new Vector3(-Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));;
        rigidbody.AddForce((moveInput * speed)-rigidbody.velocity, ForceMode.Acceleration);
        movementSound.volume=rigidbody.velocity.magnitude/15;
        var cmCam = FindObjectOfType<CinemachineVirtualCamera>();
        cmCam.m_Lens.FieldOfView= 45+1*rigidbody.velocity.magnitude*1.12f;
         float VerticalMove =Input.GetAxis("Vertical")/3.5f;
         float HorizontalMove =Input.GetAxis("Horizontal")/3.5f;
        var transposer = cmCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposer.m_ScreenX= 0.5f - HorizontalMove;
        transposer.m_ScreenY= 0.5f + VerticalMove;
    }

    private void playerRotation()
    {
        var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.70f);
    }
    private void playerShoot()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (colorIndicator != null)
            {
                var tempBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                var rigid = tempBullet.GetComponent<Rigidbody>();
                rigid.velocity = -shootPoint.right * 10f;
                gameManeger.nextColorSet();
                var render = colorIndicator.GetComponent<Renderer>();
                render.material.SetColor("_BaseColor", GameManeger.colors[GameManeger.bulletNextColor]);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            var col = GetComponent<Collider>();
            col.enabled = false;
            playerArt.GetComponent<Animation>().CrossFade("Death");
        }
    }

    public void desPlayer()
    {  
        Destroy(this.gameObject);
    }
    
    private void Update()
    {
         playerShoot();
    }
    public void CreateAnim()
    {
         playerArt.GetComponent<Animation>().CrossFade("Opening");
    }
}