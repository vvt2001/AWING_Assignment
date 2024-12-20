using AWING_Assignment_API.View;
using AWING_Assignment_Data;
using AWING_Assignment_Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AWING_Assignment_API.Services
{
    public interface IPirateNavigationServices
    {
        Task<string> Navigate(Input input);
        Task<List<InputHistory>> GetHistories();
        Task<bool> SaveToDataBase(Input input);
    }

    public class PirateNavigationServices : IPirateNavigationServices
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;


        public PirateNavigationServices(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Navigate(Input input)
        {
            //await SaveToDataBase(input);

            int n = input.n;
            int m = input.m;
            int p = input.p;

            var matrix = ParseMatrix(input.matrix, n, m);

            // Step 1: Group coordinates by their values
            var valueToCoords = new Dictionary<int, List<(int, int)>>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int v = matrix[i][j];
                    if (!valueToCoords.ContainsKey(v))
                    {
                        valueToCoords[v] = new List<(int, int)>();
                    }
                    valueToCoords[v].Add((i, j));
                }
            }

            // Step 2: Initialize priority queue(pq) and visited set
            var pq = new List<(double dist, int x, int y, int currValue)>();
            pq.Add((0, 0, 0, 0)); 
            var visited = new HashSet<(int x, int y, int currValue)>();

            // Step 3: Dijkstra-like traversal
            while (pq.Count > 0)
            {
                pq.Sort((a, b) => a.dist.CompareTo(b.dist)); // Sort by distance (min heap logic)
                var (dist, x, y, currValue) = pq[0];
                pq.RemoveAt(0);

                // Skip if already visited this state
                if (visited.Contains((x, y, currValue)))
                {
                    continue;
                }
                visited.Add((x, y, currValue));

                // If we've reached the goal value, return the distance
                if (currValue == p)
                {
                    return $"The least amount of fuel required is: {dist}";
                }

                // Move to the next value (v + 1)
                if (valueToCoords.ContainsKey(currValue + 1))
                {
                    foreach (var (nx, ny) in valueToCoords[currValue + 1])
                    {
                        double cost = Math.Sqrt(Math.Pow(x - nx, 2) + Math.Pow(y - ny, 2));
                        pq.Add((dist + cost, nx, ny, currValue + 1));
                    }
                }
            }

            // If no path exists, return -1
            return $"The least amount of fuel required is: [unknown]";
        }

        public async Task<List<InputHistory>> GetHistories()
        {
            var histories = await _context.inputHistories.ToListAsync();
            return histories;
        }

        private static int[][] ParseMatrix(string matrixStr, int n, int m)
        {
            matrixStr = matrixStr.Trim('[', ']');
            var rows = matrixStr.Split(new string[] { "],[" }, StringSplitOptions.None);
            int[][] matrix = new int[n][];

            for (int i = 0; i < n; i++)
            {
                var row = rows[i].Split(',').Select(int.Parse).ToArray();
                matrix[i] = row;
            }

            return matrix;
        }

        public async Task<bool> SaveToDataBase(Input input)
        {
            string ID = Guid.NewGuid().ToString("N");

            var inputHistory = new InputHistory{
                ID = ID,
                n = input.n,
                m = input.m,
                p = input.p,
                matrix = input.matrix,
                time = DateTime.UtcNow,
            };

            _context.inputHistories.Add(inputHistory);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
