#region Support

public enum TypePoint
{
    Start = 0,
    End = 1,
    None = 2
}

#endregion

/// <summary>
/// This class is responsible for save informaction about cell
/// </summary>
public class Cell
{
    public int id;
    public bool isEnable = true;
    public TypePoint typePoint = TypePoint.None;
    public bool canGoUp = false;
    public bool canGoDown = false;
    public bool canGoRight = false;
    public bool canGoLeft = false;
    public bool walkeable = true;

    public Cell()
    {

    }

    public void CheckGoUp()
    {
        switch (id)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 9:
            case 11:
            case 13:
            case 15:
            case 49:
            case 50:
            case 51:
                canGoUp = true;
                break;
        }
    }

    public void CheckGoRight()
    {
        switch (id)
        {
            case 2:
            case 3:
            case 6:
            case 7:
            //case 8:
            case 10:
            case 11:
            case 14:
            case 15:
            case 50:
            case 51:
                canGoRight = true;
                break;
        }
    }

    public void CheckGoLeft()
    {
        switch (id)
        {
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
                canGoLeft = true;
                break;
        }
    }

    public void CheckGoDown()
    {
        switch (id)
        {
            case 4:
            case 5:
            case 6:
            case 7:
            case 12:
            case 13:
            case 14:
            case 15:
            case 36:
                canGoDown = true;
                break;
        }
    }
}
