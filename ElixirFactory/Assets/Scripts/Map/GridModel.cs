
public class GridModel 
{
    public int[,] grid;
    public int gridSize;

    public GridModel(int gridSize)
    {
        this.gridSize = gridSize;
        grid = new int[gridSize, gridSize];
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                grid[i, j] = 0;
            }
        }
    }
    
    public int GetValue(int row, int col)
    {
        return grid[row, col];
    }
    
}
