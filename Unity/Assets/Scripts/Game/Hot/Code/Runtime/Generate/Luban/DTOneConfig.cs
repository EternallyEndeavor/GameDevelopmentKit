
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
public partial class DTOneConfig : IDataTable
{

    private DROneConfig _data;
    public DROneConfig Data => _data;
    private readonly System.Func<Cysharp.Threading.Tasks.UniTask<ByteBuf>> _loadFunc;

    public DTOneConfig(System.Func<Cysharp.Threading.Tasks.UniTask<ByteBuf>> loadFunc)
    {
        _loadFunc = loadFunc;
    }

    public async Cysharp.Threading.Tasks.UniTask LoadAsync()
    {
        ByteBuf _buf = await _loadFunc();
        int n = _buf.ReadSize();
        if (n != 1) throw new SerializationException("table mode=one, but size != 1");
        _data = DROneConfig.DeserializeDROneConfig(_buf);
        PostInit();
    }

    public string GameId => _data.GameId;
    public int SceneMenu => _data.SceneMenu;
    public int SceneMain => _data.SceneMain;

    public void ResolveRef(Tables tables)
    {
        _data.ResolveRef(tables);
        PostResolveRef();
    }

    partial void PostInit();
    partial void PostResolveRef();
}
}
