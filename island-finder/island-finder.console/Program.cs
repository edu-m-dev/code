using island_finder.console;

int[,] grid = new int[,]{
{ 1,0,1,1,0,1},
{ 0,0,1,1,0,0},
{ 1,0,0,0,0,1},
{ 1,1,1,0,0,1},
{ 1,0,1,1,0,1},
};

var islandCount = new IslandCounter().CountIslands(grid);
Console.WriteLine($"Number of islands is {islandCount}");
