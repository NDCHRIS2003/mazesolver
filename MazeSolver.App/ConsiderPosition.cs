struct ConsiderPosition :
    IComparable
{
    public int dist;
    public Position pos;

    public int CompareTo(object? obj) =>
        this.dist > ((ConsiderPosition)obj!).dist ? 1 : (this.dist < ((ConsiderPosition)obj!).dist ? -1 : 0);
}
