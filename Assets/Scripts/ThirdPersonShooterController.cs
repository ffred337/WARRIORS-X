using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{

    /*[SerializeField]*/ private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs StarterAssetsInputs;
    private Animator animator;
    //Here our _mainCamera is the camera of our player
    private Camera _mainCamera;
    private GameObject playerAimCamera;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        //_mainCamera = this.gameObject.GetComponentInChildren<Camera>();
        // _mainCamera = GetComponentInChildren<Camera>();
        //aimVirtualCamera = GameObject.Find("PlayerAimCamera");
        

    }

    private void Start()
    {
        playerAimCamera = GameObject.Find("PlayerAimCamera");
        aimVirtualCamera = playerAimCamera.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
         Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        //Ray ray = _mainCamera.ScreenPointToRay(screenCenterPoint);


        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (StarterAssetsInputs.aim)
        {
            playerAimCamera.SetActive(true);
            //aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldlAimTarget = mouseWorldPosition;
            worldlAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldlAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            playerAimCamera.SetActive(false);
            //aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (StarterAssetsInputs.shoot && StarterAssetsInputs.aim)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            StarterAssetsInputs.shoot = false;
        }


    }

}
