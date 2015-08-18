using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

 
public class TableSampleList : ITableStream
{
    public Dictionary<int, TableSample> m_DataDic = new Dictionary<int, TableSample>();

    public override void ParseCSV(CsvStream csvStream)
    { 
        TableSample tableSample = new TableSample();
        tableSample.Load(csvStream);
        m_DataDic[tableSample.index] = tableSample;
    }
    public virtual void Add(TableSample tableSample)
    {
        m_DataDic[tableSample.index] = tableSample;
    }

    public virtual TableSample Get(int index)
    {
        TableSample outData = null;
        m_DataDic.TryGetValue(index, out outData);
        return outData;
    }
	public virtual void Clear()
	{
		m_DataDic.Clear ();
	}
}

public class TableSample
{
    public int index;
    public int data1;
    public int data2;
    public int data3;
    public int data4;
    public int data5;
    public int GetData
    {
        get
        {
            return data2;
        }
    }
    public void Load(CsvStream csvStream)
    {
        csvStream.GetVar(ref index);
        csvStream.GetVar(ref data1);
        csvStream.GetVar(ref data2);
        csvStream.GetVar(ref data3);
        csvStream.GetVar(ref data4);
        csvStream.GetVar(ref data5); 
    }
} 
