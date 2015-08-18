using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
  
 
public class DataTableMgr
{ 
	static public DataTableMgr Ins = null;

    public TableSampleList m_TableSampleList = new TableSampleList();
	public TableSampleList m_byteTableSampleList = new TableSampleList();
    public void LoadAll()
    {
		TextAsset l_TextAsset = Resources.Load ("CsvTest") as TextAsset;
		CsvDataLoader.ConvertStringToData( m_TableSampleList, l_TextAsset, ""); 

		TextAsset l_TextAssetBytes = Resources.Load ("byteCsv") as TextAsset;
		CsvDataLoader.ConvertByteToData (m_byteTableSampleList, l_TextAssetBytes);
    }
	
    public TableSample GetTableSample(int index)
    {
        return m_TableSampleList.Get(index);
    }

	static public void LoadAllBinary()
	{
		Ins = new DataTableMgr ();
		Ins.LoadAll ();
	}


} 