namespace Engine.Resources.Scripts.MVC.Views
{
    using UnityEngine;

    [AddComponentMenu("Scripts/Engine/Resources/MVC/Views/Object Follower View")]
    public class ObjectFollowerView : MonoBehaviour
    {
        #region Variables

        #region Private Variables

        [SerializeField][Range(0, 100)] private float mouseSensibility;
        [SerializeField] private bool hasHorizontalLimits, hasVerticalLimits;
        [SerializeField] private Vector2 horizontalLimits, verticalLimits;
        [SerializeField] private Transform objectToServeAsEyes;
        
        private float _rotX, _rotY;
        private bool _canMove = true;

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                _canMove = !_canMove;
            
            if (!_canMove) return;
            MoveObject();
        }

        private void MoveObject()
        {
            _rotX += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensibility * 10;
            _rotY -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensibility * 10;

            if (hasHorizontalLimits)
                _rotX = _rotX > horizontalLimits.y ? horizontalLimits.y : _rotX < horizontalLimits.x ? horizontalLimits.x : _rotX;
            
            if (hasVerticalLimits)
                _rotY = _rotY > verticalLimits.y ? verticalLimits.y : _rotY < verticalLimits.x ? verticalLimits.x : _rotY;
            
            objectToServeAsEyes.transform.rotation = Quaternion.Euler(0, _rotX, 0);
            transform.rotation = Quaternion.Euler(_rotY, _rotX, 0);
        }

        #endregion

        #endregion
    }
}