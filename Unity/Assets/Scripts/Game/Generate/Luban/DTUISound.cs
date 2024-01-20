
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
public partial class DTUISound : IDataTable
{
    private readonly System.Collections.Generic.Dictionary<int, DRUISound> _dataMap;
    private readonly System.Collections.Generic.List<DRUISound> _dataList;
    private readonly System.Func<Cysharp.Threading.Tasks.UniTask<ByteBuf>> _loadFunc;

    public DTUISound(System.Func<Cysharp.Threading.Tasks.UniTask<ByteBuf>> loadFunc)
    {
        _loadFunc = loadFunc;
        _dataMap = new System.Collections.Generic.Dictionary<int, DRUISound>();
        _dataList = new System.Collections.Generic.List<DRUISound>();
    }

    public async Cysharp.Threading.Tasks.UniTask LoadAsync()
    {
        ByteBuf _buf = await _loadFunc();
        _dataMap.Clear();
        _dataList.Clear();
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            DRUISound _v;
            _v = DRUISound.DeserializeDRUISound(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public System.Collections.Generic.Dictionary<int, DRUISound> DataMap => _dataMap;
    public System.Collections.Generic.List<DRUISound> DataList => _dataList;
    public DRUISound GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public DRUISound Get(int key) => _dataMap[key];
    public DRUISound this[int key] => _dataMap[key];

    public void ResolveRef(TablesComponent tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
        PostResolveRef();
    }


    partial void PostInit();
    partial void PostResolveRef();
}
}
