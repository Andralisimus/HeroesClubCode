using System;

[Serializable]
public class PlayerPosition
{
    public float x;
    public float y;
    public string username;
    public string userId;

    public PlayerPosition(float x, float y, string username, string userId)
    {
        this.x = x;
        this.y = y;
        this.username = username;
        this.userId = userId;
    }
}
