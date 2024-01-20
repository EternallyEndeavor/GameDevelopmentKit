
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
public sealed partial class DREntity : Luban.BeanBase
{
    public DREntity(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        AssetName = _buf.ReadString();
        EntityGroupName = _buf.ReadString();
        Priority = _buf.ReadInt();
        PostInit();
    }

    public static DREntity DeserializeDREntity(ByteBuf _buf)
    {
        return new DREntity(_buf);
    }

    /// <summary>
    /// 实体编号
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 资源名称
    /// </summary>
    public readonly string AssetName;
    /// <summary>
    /// 实体组名称
    /// </summary>
    public readonly string EntityGroupName;
    /// <summary>
    /// 加载优先级
    /// </summary>
    public readonly int Priority;
    public const int __ID__ = 93435409;
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
        + "EntityGroupName:" + EntityGroupName + ","
        + "Priority:" + Priority + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolveRef();
}
}
