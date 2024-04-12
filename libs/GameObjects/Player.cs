namespace libs;

public class Player : GameObject, IMovement {

    // Constructor
    public Player() : base() {
        Type = GameObjectType.Player; // Identify this object as the player
        CharRepresentation = '☻'; // Visual representation of the player
        Color = ConsoleColor.DarkYellow; // Color of the player character
    }
    
    // Implement the Move method from IMovement
    public new void Move(int dx, int dy) {
        // Calculate the new position
        int newX = PosX + dx;
        int newY = PosY + dy;

        // Move the player if the new position is within boundaries and not blocked
        if (CanMove(newX, newY)) {
            // Set the player's new position
            PosX = newX;
            PosY = newY;
        }
    }

    // Helper method to determine if the player can move to the specified position
    private bool CanMove(int newX, int newY) {
        // Get the game map from the GameEngine
        var map = GameEngine.Instance.GetMap();
        
        // Check if the new position is within map boundaries
        if (newX < 0 || newY < 0 || newX >= map.MapWidth || newY >= map.MapHeight) {
            return false; // Movement out of bounds is not allowed
        }
        
        // Check the map for any obstacles at the new position
        GameObject? gameObjectAtNewPos = map.Get(newX, newY);
        if (gameObjectAtNewPos is Obstacle) {
            return false; // Movement into obstacles is not allowed
        }
        
        // If the new position is a box, check if it can be pushed
        if (gameObjectAtNewPos is Box box) {
            // Calculate the position the box would be moved to
            int boxNewX = newX + dx;
            int boxNewY = newY + dy;
            
            // Check if the box can be moved to the new position
            if (box.CanMove(boxNewX, boxNewY)) {
                // Push the box
                box.Move(dx, dy);
                return true; // Allow the player to move and push the box
            } else {
                return false; // Box can't be pushed, player can't move
            }
        }

        return true; // The new position is free to move into
    }
}