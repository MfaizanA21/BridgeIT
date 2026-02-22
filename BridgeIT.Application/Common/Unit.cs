namespace BridgeIT.Application.Common.Unit;

/// <summary>
/// A marker type representing "no value" / "success without data".
/// Similar to void in functional programming.
/// </summary>
public readonly struct Unit
{
    public static readonly Unit Value = new Unit();

    public Unit() { }
}