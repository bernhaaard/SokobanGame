namespace libs;

public class Map {
    private GameObject?[,] gameObjectLayer; // Layer for game objects
    private List<Position> goalPositions; // Positions of goals

    public int MapWidth { get; set; }
    public int MapHeight { get; set; }

    // Constructor initializes the map with a given width and height
    public Map(int width, int height) {
        MapWidth = width;
        MapHeight = height;
        gameObjectLayer = new GameObject?[height, width];
        goalPositions = new List<Position>();
        
        Initialize();
    }

    // Initializes or resets the game map layers
    public void Initialize() {
        for (int y = 0; y < MapHeight; y++) {
            for (int x = 0; x < MapWidth; x++) {
                gameObjectLayer[y, x] = null; // Clear the game object layer
            }
        }

        // Initialize goal positions here or through a separate method
    }

    // Set a game object at a specific position on the map
    public void Set(GameObject gameObject) {
        if (IsWithinBounds(gameObject.PosX, gameObject.PosY)) {
            gameObjectLayer[gameObject.PosY, gameObject.PosX] = gameObject;
        }
    }

    // Clear a position on the map
    public void ClearPosition(int x, int y) {
        if (IsWithinBounds(x, y)) {
            gameObjectLayer[y, x] = null;
        }
    }

    // Check if the specified position is within the bounds of the map
    private bool IsWithinBounds(int x, int y) {
        return x >= 0 && x < MapWidth && y >= 0 && y < MapHeight;
    }

    // Render the map and its objects to the console
    public void Render() {
        for (int y = 0; y < MapHeight; y++) {
            for (int x = 0; x < MapWidth; x++) {
                GameObject? gameObject = gameObjectLayer[y, x];
                char symbol = gameObject?.CharRepresentation ?? ' '; // Default to empty space
                Console.Write(symbol);
            }
            Console.WriteLine();
        }
    }

    // Checks if all goals are covered by boxes
    public bool AreAllGoalsCovered() {
        foreach (var goalPosition in goalPositions) {
            GameObject? gameObject = gameObjectLayer[goalPosition.Y, goalPosition.X];
            if (!(gameObject is Box)) {
                return false; // A goal is not covered by a box
            }
        }
        return true; // All goals are covered by boxes
    }

    // Additional methods to add or remove goals, get game objects, etc.
}