namespace libs;

public class Box : GameObject, IMovement {

    // Constructor
    public Box() : base() {
        Type = GameObjectType.Box; // Correctly identify this object as a box
        CharRepresentation = '○'; // Visual representation of the box
        Color = ConsoleColor.DarkGreen; // Color of the box character
    }
    
    // Implement the Move method from IMovement
    public new void Move(int dx, int dy) {
        // Calculate the new position
        int newX = PosX + dx;
        int newY = PosY + dy;

        // Move the box if the new position is within boundaries and not blocked
        if (CanMove(newX, newY)) {
            // Update the box's current position to the new position
            PosX = newX;
            PosY = newY;
        }
    }

    // Helper method to determine if the box can move to the specified position
    public bool CanMove(int newX, int newY) {
        // Retrieve the game map from the GameEngine
        var map = GameEngine.Instance.GetMap();
        
        // Check if the new position is within map boundaries
        if (newX < 0 || newY < 0 || newX >= map.MapWidth || newY >= map.MapHeight) {
            return false; // Movement out of bounds is not allowed
        }
        
        // Check the map for any obstacles at the new position
        GameObject? gameObjectAtNewPos = map.Get(newX, newY);
        if (gameObjectAtNewPos is Obstacle || gameObjectAtNewPos is Box) {
            return false; // Movement into obstacles or other boxes is not allowed
        }
        
        // Additional logic can be placed here if there are more conditions for box movement
        // For instance, checking for specific floor types or triggers

        return true; // The new position is free for the box to move into
    }
}