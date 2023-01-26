using UnityEngine;
using Mirror;
using Cinemachine;



namespace StarterAssets
{

    public class PlayerSetup : NetworkBehaviour
    {

        [SerializeField]
        Behaviour[] componentsToDisable;
        [SerializeField]
        private Transform target;

        GameObject sceneCamera;
         /*GameObject _mainCam;

        private void Awake()
        {
            _mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        }*/
        private void Start()
        {
            if (!isLocalPlayer)
            {
                // On boucle sur les composants renseign�s
                for (int i = 0; i < componentsToDisable.Length; i++)
                {
                    componentsToDisable[i].enabled = false;
                }
                
                // On d�sactive le Character controller
                CharacterController cc = GetComponent<CharacterController>();
                cc.enabled = false;
            }
            else
            {
                /* Si on est le joueur local on d�sactive la camera de la scene
                sceneCamera = GameObject.FindGameObjectWithTag("SceneCamera");

                //sceneCamera = Camera.main;
                if (sceneCamera != null)
                {
                    sceneCamera.gameObject.SetActive(false);
                    
                }
                _mainCam.gameObject.SetActive(true);*/
                //On fixe les position des cam�ra virtuelle sur le joueur spawn�
                GameObject playerFollowCamera = GameObject.Find("PlayerFollowCamera");
                CinemachineVirtualCamera followVirtualCamera = playerFollowCamera.GetComponent<CinemachineVirtualCamera>();
                followVirtualCamera.Follow = target;
                GameObject playerAimCamera = GameObject.Find("PlayerAimCamera");
                CinemachineVirtualCamera aimVirtualCamera = playerAimCamera.GetComponent<CinemachineVirtualCamera>();
                aimVirtualCamera.Follow = target;

            }

        }

        private void OnDisable()
        {
            /*if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(true);
            }
            _mainCam.gameObject.SetActive(false);*/

        }

    }
}
