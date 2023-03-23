using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private const float Epsilon = 0.001f;
        
        public CharacterController CharacterController;
        public float MovementSpeed;

        private IInputService _inputService;
        
        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }
    }
}
