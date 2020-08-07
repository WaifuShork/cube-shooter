using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

namespace Shork
{
    public class GameManager : MonoBehaviour
    {
        Rigidbody rb;
        public Transform playerTransform;
        
        private void Start()
        {
            
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        private void Update()
        {
            


        }


    }
}