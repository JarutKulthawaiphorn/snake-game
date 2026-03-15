using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakeGame.Manager
{
    public class SnakeInputManager
    {
        #region Action Fields
        private Action<Vector2> _onMove;
        #endregion

        #region Private Fields
        private SnakeInput _snakeInput = new SnakeInput();
        #endregion

        #region Public Methods
        public void EnableSnakeInput(Action<Vector2> onMove)
        {
            _onMove = onMove;
            _snakeInput.SnakeControl.Move.performed += OnPerformedMove;
            _snakeInput.SnakeControl.Enable();
        }

        public void DisableSnakeInput()
        {
            _snakeInput.SnakeControl.Move.performed -= OnPerformedMove;
            _snakeInput.SnakeControl.Disable();
        }
        #endregion

        #region Private Methods
        private void OnPerformedMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            if (input.magnitude != 1) return;

            _onMove?.Invoke(input);
        }
        #endregion
    }
}
