using UnityEngine;


public class SelectionController : BaseController
{
    #region Fields

    private Camera _camera;
    private Vector2 _center;
    private Transform _cameraTransform;

    private GameObject _selection;

    private SelectionModel _model;

    private float _rayLenght = 100f;
    private float _interactDistance = 20f;

    #endregion


    #region Propeties

    public Vector3 HitPoint { get; private set; }

    #endregion


    #region ClassLifeCycles

    public SelectionController()
    {
        _camera = Camera.main;
        _center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        _cameraTransform = _camera.transform;

        _model = Object.FindObjectOfType<SelectionModel>();

        On();
    }

    #endregion


    #region Methods

    public override void On()
    {
        base.On();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += SelectionLogic;
    }

    public void SelectionLogic()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(ray, out var hit, _rayLenght))
        {
            _selection = hit.collider.gameObject;
            HitPoint = hit.point;
            ISelectable target = _selection.GetComponent<ISelectable>();
            IInteractable interactionTarget = _selection.GetComponent<IInteractable>();
            if (target != null && (HitPoint - _cameraTransform.position).sqrMagnitude <= _interactDistance * _interactDistance)
            {
                if (interactionTarget != null)
                {
                    _model.SetMessage($"{target.GetName()} \n Press E - interact");
                }
                else
                {
                    _model.SetMessage(target.GetName());
                }

            }
            else
            {
                _model.SetMessage("");
            }
        }
        else
        {
            HitPoint = ray.GetPoint(_rayLenght);
        }
    }

    public void Interact()
    {
        if (_selection != null)
        {
            IInteractable interaction = _selection.GetComponent<IInteractable>();
            if (interaction != null)
            {
                interaction.Interact();
            }
        }
    }

    #endregion
}
