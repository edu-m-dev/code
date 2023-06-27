namespace island_finder.console;

public class IslandCounter
{
    public int CountIslands(int[,] grid)
    {
        var islandCount = 0;
        var rowCount = grid.GetLength(0);
        var columnCount = grid.GetLength(1);

        var visited = new bool[rowCount, columnCount];

        for (var x = 0; x < rowCount; x++)
        {
            for (var y = 0; y < columnCount; y++)
            {
                if (grid[x, y] == 1 && !visited[x, y])
                {
                    CheckCell(x, y, grid, visited, rowCount, columnCount);
                    islandCount++;
                }
            }
        }

        return islandCount;
    }

    private void CheckCell(int x, int y, int[,] grid, bool[,] visited, int rowCount, int columnCount)
    {
        if (grid[x, y] == 0 || visited[x, y])
            return;

        visited[x, y] = true;

        var neighbX = new int[] { 1, 1, 0, -1, -1, -1, 0, 1 };
        var neighbY = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };

        for (var z = 0; z < 8; z++)
        {
            var cellX = x + neighbX[z];
            var cellY = y + neighbY[z];

            if (IsInsideGrid(cellX, cellY, grid, rowCount, columnCount))
            {
                CheckCell(cellX, cellY, grid, visited, rowCount, columnCount);
            }
        }
    }

    private bool IsInsideGrid(int x, int y, int[,] grid, int rowCount, int columnCount)
    {
        if (x < 0 || x >= rowCount)
        {
            return false;
        }
        if (y < 0 || y >= columnCount)
        {
            return false;
        }
        return true;
    }
}
