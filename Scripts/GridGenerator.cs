using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Cell _prefab;
    [SerializeField] private float _offset;
    [SerializeField] private Transform _parent;
    [SerializeField] private GridRepository _gridRepository;
    [SerializeField] private GridInteractor _gridInteractor;

    [ContextMenu("Generate grid")]
    private void GenerateGrid()
    {
        _gridRepository.Cells.Clear();
        _prefab.GridInteractor = _gridInteractor;

        var cellsize = _prefab.GetComponent<MeshRenderer>().bounds.size;

        for(int x = 0; x < _gridSize.x; x++)
        {
            for(int y = 0; y < _gridSize.y; y++)
            {
                var position = new Vector3(x * (cellsize.x + _offset), 0, y * (cellsize.z + _offset));

                var cell = Instantiate(_prefab, position, Quaternion.identity, _parent);

                cell.name = $"X: {x} Y: {y}";
                cell.Position = new Vector2(x, y);

                _gridRepository.Cells.Add(cell);
            }
        }
    }
}
