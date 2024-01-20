
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
public sealed partial class DRThruster : Luban.BeanBase
{
    public DRThruster(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Speed = _buf.ReadFloat();
        PostInit();
    }

    public static DRThruster DeserializeDRThruster(ByteBuf _buf)
    {
        return new DRThruster(_buf);
    }

    /// <summary>
    /// 推进器编号
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 速度
    /// </summary>
    public readonly float Speed;
    public const int __ID__ = -1106796109;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        PostResolveRef();
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Speed:" + Speed + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolveRef();
}
}
