using System.Collections.Generic;
using UnityEngine;

public class GridInteractor : MonoBehaviour
{
    public GridRepository GridRepository;

    public void SelectCell(Cell cell)
    {
        GridRepository.SelectedCell = cell;
        cell.State = State.Selected;
        cell.ChangeColor(cell.SelectedColor);

        if (cell.Unit != null && cell.Unit.GreenCells.Count != 0)
        {
            ShowVariants(cell.Unit.GreenCells);
        }
        else if(cell.Unit != null)
        {
            cell.Unit.Variants = CalculateVariants(cell, cell.Unit.MoveVariants, cell.Unit.AttackVariants);

            ShowVariants(cell.Unit.Variants);
        }
    }

    public void DeselectCell(Cell cell)
    {
        GridRepository.SelectedCell.State = State.Standart;
        GridRepository.SelectedCell.ChangeColor(GridRepository.SelectedCell.StandartColor);
        GridRepository.SelectedCell = null;

        if (cell.Unit != null && cell.Unit.GreenCells.Count != 0)
        {
            ClearVariants(cell.Unit.GreenCells);
        }
        else if(cell.Unit != null)
        {
            ClearVariants(cell.Unit.Variants);
            cell.Unit.Variants.Clear();
        }
    }

    public List<Cell> CalculateVariants(Cell unitcell, List<Vector2> movevariants, List<Vector2> attackvariants)
    {
        var variants = new List<Cell>();
        Cell cell;

        foreach (Vector2 offset in movevariants)
        {
            cell = FindCellByPosition(offset, unitcell, true);

            if (cell != null)
            {
                variants.Add(cell);
            }
        }

        foreach (Vector2 offset in attackvariants)
        {
            cell = FindCellByPosition(offset, unitcell, false);

            if (cell != null)
            {
                variants.Add(cell);
            }
        }

        return variants;
    }

    public Cell FindCellByPosition(Vector2 offset, Cell UnitCell, bool MoveVariant)
    {
        if (MoveVariant)
        {
            return GridRepository.Cells.Find(cell => cell.Position == UnitCell.Position + offset && cell.Unit == null);
        }
        else
        {
            return GridRepository.Cells.Find(cell => cell.Position == UnitCell.Position + offset && cell.Unit != null && cell.Unit.UnitSide != UnitCell.Unit.UnitSide);
        }
    }

    public void ShowVariants(List<Cell> variants)
    {
        foreach (Cell variant in variants)
        {

            if(variant.Unit == null && GridRepository.SelectedCell.Unit != variant.Unit)
            {
                variant.ChangeColor(variant.MoveColor);

                variant.State = State.Variant;
            }
            else if(GridRepository.SelectedCell.Unit != variant.Unit)
            {
                variant.ChangeColor(variant.AttackColor);

                variant.State = State.Variant;
            }
        }
    }

    public void ClearVariants(List<Cell> variants)
    {
        foreach (Cell variant in variants)
        {
            variant.State = State.Standart;
            variant.ChangeColor(variant.StandartColor);
        }
    }
}