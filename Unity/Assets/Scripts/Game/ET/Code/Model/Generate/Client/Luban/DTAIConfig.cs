//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET
{
   
public sealed partial class DTAIConfig : IDataTable
{
    private readonly Dictionary<int, DRAIConfig> _dataMap;
    private readonly List<DRAIConfig> _dataList;

    private readonly System.Func<Task<ByteBuf>> _loadFunc;

    public DTAIConfig(System.Func<Task<ByteBuf>> loadFunc)
    {
        _loadFunc = loadFunc;
        _dataMap = new Dictionary<int, DRAIConfig>();
        _dataList = new List<DRAIConfig>();
    }

    public async Task LoadAsync()
    {
        ByteBuf _buf = await _loadFunc();
        _dataMap.Clear();
        _dataList.Clear();
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            DRAIConfig _v;
            _v = DRAIConfig.DeserializeDRAIConfig(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, DRAIConfig> DataMap => _dataMap;
    public List<DRAIConfig> DataList => _dataList;

    public DRAIConfig GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public DRAIConfig Get(int key) => _dataMap[key];
    public DRAIConfig this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, IDataTable> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}