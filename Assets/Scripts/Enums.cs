namespace markow
{
    public enum ENTITY_TYPE
    {
        Red,
        Blue,
        Green,
        CubeEater
    }

    public enum ENTITY_STATE
    {
        // Default state
        Disabled,
        // State triggered when the object is being moved
        Initialized,
        // State triggered when the object is dropped on the floor
        Detached,
        // State triggered when the object is out of the floor
        Undetected,
        // State triggered when the object is being destroyed
        Destroyed
    }
}
