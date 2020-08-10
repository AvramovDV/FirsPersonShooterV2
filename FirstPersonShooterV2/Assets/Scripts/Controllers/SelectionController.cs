using UnityEngine;


public class SelectionController : BaseController
{
    #region Fields

    private Camera _camera;
    private Vector2 _center;

    private GameObject _selection;

    private SelectionModel _model;

    private float _rayLenght = 20f;

    #endregion


    #region ClassLifeCycles

    public SelectionController()
    {
        _camera = Camera.main;
        _center = new Vector2(Screen.width / 2f, Screen.height / 2f);

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
        if (Physics.Raycast(_camera.ScreenPointToRay(_center), out var hit, _rayLenght))
        {
            _selection = hit.collider.gameObject;
            ISelectable target = _selection.GetComponent<ISelectable>();
            IInteractable interactionTarget = _selection.GetComponent<IInteractable>();
            if (target != null)
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
