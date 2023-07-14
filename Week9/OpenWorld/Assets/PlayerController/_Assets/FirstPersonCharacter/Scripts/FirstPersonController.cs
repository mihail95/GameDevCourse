

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (CharacterController))]
[RequireComponent(typeof (AudioSource))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] private bool m_IsWalking = default;
    [SerializeField] private float m_WalkSpeed = default;
    [SerializeField] private float m_RunSpeed = default;
    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten = default;
    [SerializeField] private float m_JumpSpeed = default;
    [SerializeField] private float m_StickToGroundForce = default;
    [SerializeField] private float m_GravityMultiplier = default;
    [SerializeField] private MouseLook m_MouseLook = default;
    [SerializeField] private bool m_UseFovKick = default;
    [SerializeField] private FOVKick m_FovKick = new FOVKick();
    [SerializeField] private bool m_UseHeadBob = default;
    [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
    [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
    [SerializeField] private float m_StepInterval = default;
    [SerializeField] private AudioClip[] m_FootstepSounds = default;   // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip m_JumpSound = default;      // the sound played when character leaves the ground.
    [SerializeField] private AudioClip m_LandSound = default;        // the sound played when character touches back on ground.
    [SerializeField] private AudioClip m_FlashSound = default;
    [SerializeField] private GameObject flashlightGO = default;
    [SerializeField] private GameObject projectilePrefab = default;
    [SerializeField] private GameObject ballSpawner = default;
    [SerializeField] private Camera playerCam = default;
    [SerializeField] private List<GameObject> boxPrefabsList = default;
    [SerializeField] private GameObject rayEmitter = default;
    [SerializeField] private List<GameObject> boxUIIconsList = default;
    [SerializeField] private GameObject boxHighlighter = default;

    private Camera m_Camera;
    private bool m_Jump;
    private float m_YRotation;
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    private bool m_PreviouslyGrounded;
    private Vector3 m_OriginalCameraPosition;
    private float m_StepCycle;
    private float m_NextStep;
    private bool m_Jumping;
    private AudioSource m_AudioSource;

    private float speed;
    private float mouseX;
    private float mouseY;

    bool cheatOn = false;
    float initialRunSpeed, initialJumpSpeed;

    bool flashOn = false;
    private List<GameObject> ballsSpawned = new(30);
    private int ballListIdx = 0;
    private GameObject currentProjectlie;
    private GameObject currentBox;
    private int selectedBoxPrefab = 0;
    private bool isDraggingBox = false;
    private GameObject companionCube = null;
    // Use this for initialization
    private void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions.Enable();
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        m_OriginalCameraPosition = m_Camera.transform.localPosition;
        m_FovKick.Setup(m_Camera);
        m_HeadBob.Setup(m_Camera, m_StepInterval);
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle/2f;
        m_Jumping = false;
        m_AudioSource = GetComponent<AudioSource>();
		m_MouseLook.Init(transform , m_Camera.transform);
        speed = m_WalkSpeed;

        initialRunSpeed = m_RunSpeed;
        initialJumpSpeed = m_JumpSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        RotateView();

        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            StartCoroutine(m_JumpBob.DoBobCycle());
            PlayLandingSound();
            m_MoveDir.y = 0f;
            m_Jumping = false;
        }
        if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
        {
            m_MoveDir.y = 0f;
        }

        m_PreviouslyGrounded = m_CharacterController.isGrounded;

        if (isDraggingBox)
        {
            currentBox.transform.position = ballSpawner.transform.position;
        }
    }

    void OnJump()
    {
        // the jump state needs to read here to make sure it is not missed
        if (!m_Jump)
        {
            m_Jump = true;
        }
    }

    void OnRun(InputValue value)
    {
        m_IsWalking = !value.isPressed;
        
        bool waswalking = m_IsWalking;

        speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;

        // handle speed change to give an fov kick
        // only if the player is going to a run, is running and the fovkick is to be used
        if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
        {
            StopAllCoroutines();
            StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
        }
    }

    void OnMouseX(InputValue value)
    {
        mouseX = value.Get<float>();
    }

    void OnMouseY(InputValue value)
    {
        mouseY = value.Get<float>();
    }

    void OnMovement(InputValue value)
    {
        var horizontal = value.Get<Vector2>().x;
        var vertical = value.Get<Vector2>().y;

        
        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }

    void OnGodMode(InputValue value)
    {
        cheatOn = !cheatOn;

        m_RunSpeed = cheatOn ? 200f : initialRunSpeed;
        m_JumpSpeed = cheatOn ? 30f : initialJumpSpeed;
    }

    void OnToggleFlash(InputValue value)
    {
        flashOn = !flashOn;
        flashlightGO.SetActive(flashOn);
        m_AudioSource.clip = m_FlashSound;
        m_AudioSource.Play();
    }

    void OnFireBall(InputValue value)
    {
        if (ballsSpawned.Count >= ballsSpawned.Capacity)
        {
            currentProjectlie = ballsSpawned[ballListIdx];
            currentProjectlie.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentProjectlie.transform.position = ballSpawner.transform.position;
            ballListIdx = (ballListIdx + 1) % ballsSpawned.Capacity;
        }
        else
        {
            currentProjectlie = Instantiate(projectilePrefab, ballSpawner.transform.position, Quaternion.identity);
            ballsSpawned.Add(currentProjectlie);
        }
        currentProjectlie.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * 4250f);
    }

    void OnSpawnBox(InputValue value)
    {
        // This handles spawning boxes or picking up existing ones
        if (value.isPressed)
        {
            // Bit shift the index of the layer (6) to get a bit mask
            // This would cast rays only against colliders in layer 6.
            int layerMask = 1 << 6;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(rayEmitter.transform.position, playerCam.transform.forward, out RaycastHit hit, 10f, layerMask))
            {
                currentBox = hit.rigidbody.gameObject;
            }
            else
            {
                if (selectedBoxPrefab == 3 && companionCube == null)
                {
                    currentBox = Instantiate(boxPrefabsList[selectedBoxPrefab], ballSpawner.transform.position, Quaternion.identity);
                    companionCube = currentBox;
                }
                else if (selectedBoxPrefab != 3)
                {
                    currentBox = Instantiate(boxPrefabsList[selectedBoxPrefab], ballSpawner.transform.position, Quaternion.identity);
                }
                else
                {
                    currentBox = companionCube;
                }
            }
            isDraggingBox = true;
        }
        // This drops the current box
        else if (!value.isPressed)
        {
            isDraggingBox = false;
            currentBox.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentBox = null;
        }
    }

    void OnChooseBox(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            float value = inputValue.Get<float>();
            selectedBoxPrefab = (int)value - 1;

            boxHighlighter.transform.position = boxUIIconsList[selectedBoxPrefab].transform.position;
        }
    }

    private void PlayLandingSound()
    {
        if (cheatOn) return;

        m_AudioSource.clip = m_LandSound;
        m_AudioSource.Play();
        m_NextStep = m_StepCycle + .5f;
    }


    private void FixedUpdate()
    {
        // float speed;
        // GetInput(out speed);
        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
        
        m_MoveDir.x = desiredMove.x*speed;
        m_MoveDir.z = desiredMove.z*speed;


        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce;

            if (m_Jump)
            {
                m_MoveDir.y = m_JumpSpeed;
                PlayJumpSound();
                m_Jump = false;
                m_Jumping = true;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

        ProgressStepCycle(speed);
        UpdateCameraPosition(speed);

        m_MouseLook.UpdateCursorLock();
    }


    private void PlayJumpSound()
    {
        if (cheatOn) return;

        m_AudioSource.clip = m_JumpSound;
        m_AudioSource.Play();
    }


    private void ProgressStepCycle(float speed)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }


    private void PlayFootStepAudio()
    {
        if (cheatOn) return;

        if (!m_CharacterController.isGrounded)
        {
            return;
        }
        
        if (m_FootstepSounds.Length == 0)
        {
            return;
        }
        
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }


    private void UpdateCameraPosition(float speed)
    {
        Vector3 newCameraPosition;
        if (!m_UseHeadBob)
        {
            return;
        }
        if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
        {
            m_Camera.transform.localPosition =
                m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                  (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
        }
        else
        {
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
        }
        m_Camera.transform.localPosition = newCameraPosition;
    }


    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform, mouseX, mouseY);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
    }
}