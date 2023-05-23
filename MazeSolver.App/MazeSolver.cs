using System.Text;

namespace MazeSolver.App
{
    public class MazeSolver
    {
        private int GetDistance(Position consideringPost, Position endPos) =>
            (consideringPost.row - endPos.row) + (consideringPost.col - endPos.col);

        internal Position FindPosition(char[][] maze, MazeChar position)
        {
            for (int row = 0; row < maze.Length; row++)
            {
                for (int col = 0; col < maze[0].Length; col++)
                {
                    if (maze[row][col] == (char)position)
                        return new Position { col = col, row = row };
                }
            }
            return new Position();
        }

        internal char[][] ReadMaze(string filePath)
        {
            using var fileReader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(fileReader);
            List<string> maze = new List<string>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                maze.Add(line);
            }

            return maze.Select(x => x.ToCharArray()).ToArray();
        }

        internal void OutputMaze(char[][] maze, string filePath)
        {  
            StringBuilder stringBuilder = new StringBuilder();
            for (int row = 0; row < maze.Length; row++)
            {
                for (int col = 0; col < maze[0].Length; col++)
                {
                    stringBuilder.Append(maze[row][col]);                        
                }
                stringBuilder.AppendLine();
            }
            using FileStream fileStream = new FileStream($"../../../{filePath}", FileMode.OpenOrCreate, FileAccess.Write);

            using StreamWriter writer = new StreamWriter(fileStream, Encoding.ASCII);
              
            writer.Write(stringBuilder.ToString());
            writer.Close();
             
                fileStream.Close();
           ;
            
        }
        internal char[][] SolveMaze(char[][] maze, ref Position currentPosition, Position endPosition)
        {
            var possiblePositions = new Position[] { new Position { col = currentPosition.col, row = currentPosition.row - 1 },
                new Position { col = currentPosition.col, row = currentPosition.row + 1 },
                new Position { col = currentPosition.col - 1, row = currentPosition.row },
                new Position { col = currentPosition.col + 1, row = currentPosition.row } };
            var distances = new List<ConsiderPosition>();

            for (int i = 0; i < possiblePositions.Length; i++)
            {
                if (IsValidPosition(maze, possiblePositions[i]))
                {
                    distances.Add(new ConsiderPosition { dist = GetDistance(possiblePositions[i], endPosition), pos = possiblePositions[i] });
                }
            }

            foreach (var lowestPossiblePosition in SortProspectivePositions(distances.ToArray()))
            {
                currentPosition = lowestPossiblePosition.pos;
                if (currentPosition.Equals(endPosition))
                    break;

                maze[currentPosition.row][currentPosition.col] = (char)MazeChar.Path;

                SolveMaze(maze, ref currentPosition, endPosition);
                if (currentPosition.Equals(endPosition))
                    break;
                else
                {
                    maze[lowestPossiblePosition.pos.row][lowestPossiblePosition.pos.col] = (char)MazeChar.Clear;
                }
            }
            return maze;
        }

        private ConsiderPosition[] SortProspectivePositions(ConsiderPosition[] distances)
        {
            for (int i = 1; i < distances.Length; i++)
            {
                ConsiderPosition x = distances[i];
                int j = Math.Abs(Array.BinarySearch(distances, 0, i, x) + 1);

                Array.Copy(distances, j, distances, j + 1, i - j);
                distances[j] = x;
            }

            return distances;
        }

        private bool IsValidPosition(char[][] maze, Position position) =>
            position.row <= maze.Length && position.col <= maze[0].Length
            && position.row >= 0 && position.col >= 0
            && maze[position.row][position.col] != (char)MazeChar.Wall
            && maze[position.row][position.col] != (char)MazeChar.Path
            && maze[position.row][position.col] != (char)MazeChar.Start;
    }
}