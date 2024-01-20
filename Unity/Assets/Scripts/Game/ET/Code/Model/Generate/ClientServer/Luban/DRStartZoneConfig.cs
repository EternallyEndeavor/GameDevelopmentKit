
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;

namespace ET
{
public sealed partial class DRStartZoneConfig : Luban.BeanBase
{
    public DRStartZoneConfig(ByteBuf _buf) 
    {
        StartConfig = _buf.ReadString();
        Id = _buf.ReadInt();
        DBConnection = _buf.ReadString();
        DBName = _buf.ReadString();
        Desc = _buf.ReadString();
        PostInit();
    }

    public static DRStartZoneConfig DeserializeDRStartZoneConfig(ByteBuf _buf)
    {
        return new DRStartZoneConfig(_buf);
    }

    /// <summary>
    /// 开启类型
    /// </summary>
    public readonly string StartConfig;
    /// <summary>
    /// Id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 数据库地址
    /// </summary>
    public readonly string DBConnection;
    /// <summary>
    /// 数据库名
    /// </summary>
    public readonly string DBName;
    /// <summary>
    /// 说明
    /// </summary>
    public readonly string Desc;
    public const int __ID__ = -1835667038;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        PostResolveRef();
    }

    public override string ToString()
    {
        return "{ "
        + "StartConfig:" + StartConfig + ","
        + "Id:" + Id + ","
        + "DBConnection:" + DBConnection + ","
        + "DBName:" + DBName + ","
        + "Desc:" + Desc + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolveRef();
}
}
