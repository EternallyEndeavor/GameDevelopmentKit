
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;

namespace Game
{
public sealed partial class DRMusic : Luban.BeanBase
{
    public DRMusic(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        AssetName = _buf.ReadString();
        Volume = _buf.ReadFloat();
        PostInit();
    }

    public static DRMusic DeserializeDRMusic(ByteBuf _buf)
    {
        return new DRMusic(_buf);
    }

    /// <summary>
    /// 音乐编号
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 资源名称
    /// </summary>
    public readonly string AssetName;
    /// <summary>
    /// 音量（0~1）
    /// </summary>
    public readonly float Volume;
    public const int __ID__ = -1651958217;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(TablesComponent tables)
    {
        
        
        
        PostResolveRef();
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "AssetName:" + AssetName + ","
        + "Volume:" + Volume + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolveRef();
}
}
