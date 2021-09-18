using UnityEngine;

public enum State
{
    Standart,
    Selected,
    Variant
}

public class Cell : MonoBehaviour
{
    public Color StandartColor;
    public Color HoverColor;
    public Color HoverMoveColor;
    public Color HoverAttackColor;

    public Color SelectedColor;
    public Color MoveColor;
    public Color AttackColor;

    [SerializeField] private MeshRenderer _meshRenderer;

    public UnitBase Unit; // Юнит который на клетке

    [HideInInspector] public Vector2 Position;
    [HideInInspector] public State State;

    public GridInteractor GridInteractor;

    public void ChangeColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    private void OnMouseEnter()
    {
        if(State == State.Standart)
        {
            ChangeColor(HoverColor);
        }
        else if(State == State.Variant)
        {
            if(Unit == null)
            {
                ChangeColor(HoverMoveColor);
            }
            else
            {
                ChangeColor(HoverAttackColor);
            }
        }
    }

    private void OnMouseDown()
    {
        if (State == State.Selected)
        {
            GridInteractor.DeselectCell(this);
        }
        else if (State == State.Variant)
        {
            GridInteractor.GridRepository.SelectedCell.Unit.GoToCell(this);
        }
        else if(State == State.Standart)
        {
            if(GridInteractor.GridRepository.SelectedCell != null)
            {
                GridInteractor.DeselectCell(GridInteractor.GridRepository.SelectedCell);
            }

            GridInteractor.SelectCell(this);
        }
    }

    private void OnMouseExit()
    {
        if(State == State.Standart)
        {
            ChangeColor(StandartColor);
        }

        else if(State == State.Variant)
        {
            if(Unit == null)
            {
                ChangeColor(MoveColor);
            }
            else
            {
                ChangeColor(AttackColor);
            }
        }
    }
}
