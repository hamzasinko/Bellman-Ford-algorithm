using System;
using System.Collections.Generic;

class Graph
{
    private int V; // Nombre de sommets
    private int E; // Nombre d'arêtes
    private List<Tuple<int, int, int>> edges; // Liste des arêtes (origine, destination, poids)

    public Graph(int v, int e)
    {
        V = v;
        E = e;
        edges = new List<Tuple<int, int, int>>(e);
    }

    public void AddEdge(int source, int destination, int weight)
    {
        edges.Add(new Tuple<int, int, int>(source, destination, weight));
    }

    public void BellmanFord(int source)
    {
        int[] distances = new int[V];
        int[] predecessors = new int[V];

        // Initialiser les distances à l'infini et le prédecesseur à -1
        for (int i = 0; i < V; i++)
        {
            distances[i] = int.MaxValue;
            predecessors[i] = -1;
        }

        distances[source] = 0;

        // Relâcher les arêtes V-1 fois pour trouver le plus court chemin
        for (int i = 1; i < V; i++)
        {
            foreach (var edge in edges)
            {
                int u = edge.Item1;
                int v = edge.Item2;
                int weight = edge.Item3;
                if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                {
                    distances[v] = distances[u] + weight;
                    predecessors[v] = u;
                }
            }
        }

        // Vérifier s'il y a un cycle de poids négatif
        foreach (var edge in edges)
        {
            int u = edge.Item1;
            int v = edge.Item2;
            int weight = edge.Item3;
            if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
            {
                Console.WriteLine("Le graphe contient un cycle absorbant.");
                return;
            }
        }

        // Afficher les distances minimales et les prédecesseurs
        for (int i = 0; i < V; i++)
        {
            Console.WriteLine($"Sommet {i}: Distance minimale = {distances[i]}, Prédecesseur = {predecessors[i]}");
        }
    }

    public static void Main(string[] args)
    {
        int V, E, o, d, p;

        do{
            Console.WriteLine("Saisir le nombre de sommets: ");
            V = Convert.ToInt32(Console.ReadLine()); // Nombre de sommets
            Console.WriteLine("Saisir le nombre d'arretes: ");
            E = Convert.ToInt32(Console.ReadLine()); // Nombre d'arêtes
        } while (V < 0 || E < 0);

        Graph graph = new Graph(V, E);
        for (int i = 0;i < E;i++)
        {
            bool test = false;
            Console.WriteLine($"saisir l'origine et la destination et le poids d'arete {i+1}: ");
            do {
                if (test)
                    Console.WriteLine($"Valeurs des sommets doivent etre positifs resaisir l'arete {i+1}: ");
                o = Convert.ToInt32(Console.ReadLine());
                d = Convert.ToInt32(Console.ReadLine());
                test = true;
            } while (o<0 || d<0 || o>=V || d >=E);
            test = false;
            p = Convert.ToInt32(Console.ReadLine());
            graph.AddEdge(o, d, p);
            Console.WriteLine($"l'arrete {i+1} a été créé: ({o}) -> ({d}) ");
        }
        Console.WriteLine("graphe créé completement.");
        Console.WriteLine("saisir la source du PCC: ");
        int source = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("BellmanFord...");
        graph.BellmanFord(source);
    }
}