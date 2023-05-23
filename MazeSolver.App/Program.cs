
namespace MazeSolver.App;

class Program
{
    static void Main()
    {
        var mazeSolver = new MazeSolver();
        char[][] maze = mazeSolver.ReadMaze("Maze.txt");

        maze.PrintMaze();

        var startPosition = mazeSolver.FindPosition(maze, MazeChar.Start);
        var endPosition = mazeSolver.FindPosition(maze, MazeChar.End);
        var currentPosition = startPosition;

        maze = mazeSolver.SolveMaze(maze, ref currentPosition, endPosition);

        maze.PrintMaze();
        mazeSolver.OutputMaze(maze, "MazeOutput.txt");
    }
}