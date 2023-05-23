using System.Text;

public static class MazeHelper
{
    public static void PrintMaze(this char[][] maze)
    {
        StringBuilder strinBuilder = new StringBuilder();
        for (int row = 0; row < maze.Length; row++)
        {
            for (int col = 0; col < maze[0].Length; col++)
            {                
                strinBuilder.Append(maze[row][col]);
            }           
            strinBuilder.AppendLine();
        }

        Console.Write(strinBuilder.ToString());
    }
}
