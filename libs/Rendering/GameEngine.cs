using Newtonsoft.Json; // For JSON parsing
using System.Collections.Generic; // For list data structures
using System.Linq; // For LINQ operations

namespace libs;

public sealed class GameEngine
{
    private static GameEngine? _instance;
    private Map map;
    private List<GameObject> gameObjects;
    private Player? player; // Single instance of Player

    // Singleton pattern
    public static GameEngine Instance {
        get {
            if (_instance == null) {
                _instance = new GameEngine();
            }
            return _instance;
        }
    }

    // Private constructor for singleton
    private GameEngine() {
        map = new Map(); // Initialize the game map
        gameObjects = new List<GameObject>(); // Initialize the list of game objects
        Setup(); // Setup the game
    }

    // Initializes the game objects based on a JSON configuration
    public void Setup() {
        // Parse the JSON configuration
        string setupJsonContent = File.ReadAllText("../../Setup.json");
        dynamic gameData = JsonConvert.DeserializeObject(setupJsonContent);
        map = new Map((int)gameData.map.width, (int)gameData.map.height);
        
        // Create a single instance of the player
        if (player == null) {
            var playerData = gameData.gameObjects.FirstOrDefault(obj => obj.type == "Player");
            player = new Player(/* Initialize with playerData */);
            gameObjects.Add(player);
        }
        
        // Initialize other game objects based on gameData
        foreach (var data in gameData.gameObjects.Where(obj => obj.type != "Player")) {
            // Create game objects using a factory or constructor
            // Add them to the gameObjects list
            // Ensure only one instance of each object type is created as needed
            GameObject gameObject = GameObjectFactory.Create(data);
            gameObjects.Add(gameObject);
        }
    }

    // Main game loop method for updating the game state
    public void Update() {
        // Process input
        InputHandler.Instance.HandleInput();
        
        // Update object states (e.g., move player or boxes)
        foreach (var obj in gameObjects.OfType<IMovement>()) {
            // Update each movable object
            // Example: obj.Move(...)
        }
        
        // Check for win condition (all boxes on goals)
        if (CheckWinCondition()) {
            // Handle winning the game
        }

        // Render the game state
        Render();
    }

    // Render method to draw the game objects on the console
    public void Render() {
        // Clear the console
        map.Clear(); // Clear previous game state
        
        // Place and draw each game object
        foreach (var gameObject in gameObjects) {
            map.Set(gameObject);
        }
        
        map.Render(); // Draw the map with all objects placed
    }

    // Checks if all boxes are on the goal positions
    private bool CheckWinCondition() {
        // Assume map keeps track of goal positions
        return map.AreAllGoalsCovered();
    }

    public Map GetMap() {
        return this.map;
    }

    // Additional methods to handle game state, like resetting the level, etc.
}