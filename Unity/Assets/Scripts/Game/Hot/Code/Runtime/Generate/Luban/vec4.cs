
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;

namespace Game.Hot
{
public partial struct vec4
{
    public vec4(ByteBuf _buf) 
    {
        X = _buf.ReadFloat();
        Y = _buf.ReadFloat();
        Z = _buf.ReadFloat();
        W = _buf.ReadFloat();
        PostInit();
    }

    public static vec4 Deserializevec4(ByteBuf _buf)
    {
        return new vec4(_buf);
    }

    public readonly float X;
    public readonly float Y;
    public readonly float Z;
    public readonly float W;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        PostResolveRef();
    }

    public override string ToString()
    {
        return "{ "
        + "x:" + X + ","
        + "y:" + Y + ","
        + "z:" + Z + ","
        + "w:" + W + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolveRef();
}
}
