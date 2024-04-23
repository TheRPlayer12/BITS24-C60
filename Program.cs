class Program
{
    static void Main(string[] args)
    {
        char[,] matrix = new char[,]
        {
            { '#', 'T', 'G', '0', '0' },
            { '#', 'T', '#', '0', '0' },
            { '0', '0', '0', '0', '0' },
            { '0', '0', '#', 'K', 'T' },
            { '0', '0', '#', 'K', 'T' }
        };

        List<Kingdom> kingdoms = GetKingdoms(matrix);
        foreach (var kingdom in kingdoms)
        {
            kingdom.Power = CalPower(kingdom);
            kingdom.Apples = CalApples(kingdom);
        }

        Kingdom superiorKingdom = FindSuperiorKingdom(kingdoms);
        int Days = CalDays(superiorKingdom);

        Console.WriteLine($"The superior kingdom can survive for {Days} days.");
    }

    static List<Kingdom> GetKingdoms(char[,] matrix)
    {
        List<Kingdom> kingdoms = [];
        bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == '#' && !visited[i, j])
                {
                    Kingdom kingdom = new();
                    DFS(matrix, i, j, visited, kingdom);
                    kingdoms.Add(kingdom);
                }
            }
        }

        return kingdoms;
    }

    static void DFS(char[,] matrix, int i, int j, bool[,] visited, Kingdom kingdom)
    {
        if (i < 0 || i >= matrix.GetLength(0) || j < 0 || j >= matrix.GetLength(1) || visited[i, j] || matrix[i, j] == '0')
        {
            return;
        }

        visited[i, j] = true;

        if (matrix[i, j] == 'K')
        {
            kingdom.Knights++;
        }
        else if (matrix[i, j] == 'G')
        {
            kingdom.Giants++;
        }
        else if (matrix[i, j] == 'T')
        {
            kingdom.Trees++;
        }

        DFS(matrix, i - 1, j, visited, kingdom);
        DFS(matrix, i + 1, j, visited, kingdom);
        DFS(matrix, i, j - 1, visited, kingdom);
        DFS(matrix, i, j + 1, visited, kingdom);
    }

    static int CalPower(Kingdom kingdom)
    {
        return kingdom.Knights * 10 + kingdom.Giants * 100;
    }

    static int CalApples(Kingdom kingdom)
    {
        return kingdom.Trees * 300;
    }

    static Kingdom FindSuperiorKingdom(List<Kingdom> kingdoms)
    {
        Kingdom superiorKingdom = null;
        int maxPower = 0;

        foreach (var kingdom in kingdoms)
        {
            int power = CalPower(kingdom);
            if (power > maxPower)
            {
                maxPower = power;
                superiorKingdom = kingdom;
            }
        }

        return superiorKingdom;
    }

    static int CalDays(Kingdom kingdom)
    {
        return CalApples(kingdom) / (kingdom.Knights + kingdom.Giants);
    }
}

class Kingdom
{
    public int Knights { get; set; }
    public int Giants { get; set; }
    public int Trees { get; set; }
    public int Power { get; set; }
    public int Apples { get; set; }
}

