using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CsvDataLoader 
{
	static public void ConvertStringToData( ITableStream iTableStream, TextAsset textAsset, string binaryWritePath)
	{
		string text = textAsset.text;
		ConvertStringToData (iTableStream, ref text, binaryWritePath);
	}

	static public void ConvertStringToData( ITableStream iTableStream, ref string text, string binaryWritePath)
	{
		if(iTableStream == null)
		{
			return;
		}
		binaryWritePath = Application.dataPath + "/00Game/Resources/byteCsv.bytes";

		System.IO.BinaryWriter binaryWriter = null; 
		if(string.IsNullOrEmpty(binaryWritePath) == false)
		{
			System.IO.FileStream fs = System.IO.File.OpenWrite(binaryWritePath);
			binaryWriter = new System.IO.BinaryWriter(fs);
		}
		
		
		CsvBinaryWriter csvBinaryWriter = new CsvBinaryWriter(binaryWriter);
		
		int nCsvLine = 0;
		int firstCoulmnCount = 0;
		string []splitData = new string[]{","};
		System.Text.StringBuilder strBuild = new System.Text.StringBuilder();
		List<string> stringList = new List<string>();
		for (int i = 0; i < text.Length; ++i)
		{
			char l_char = text[i];
			bool setData = false;
			if (l_char == '\r')
			{
				continue;
			}
			else if (l_char == '\n')
			{
				setData = true;
			}
			else  if (l_char == ',')
			{
				if(strBuild.Length == 0)
				{
					Debug.LogWarning("Csv Data Empty: " + "Row => " + nCsvLine);
				}
				stringList.Add(strBuild.ToString());
				strBuild.Remove(0, strBuild.Length);
				
			}
			else
			{
				strBuild.Append(l_char);
			}
			
			if( i == text.Length -1)
			{
				setData = true;
			}
			
			if(setData)
			{
				if(stringList.Count > 0 || strBuild.Length > 0)
				{
					if(strBuild.Length == 0)
					{
						Debug.LogWarning("Csv Data Empty: " + "Row => " + nCsvLine);
					}
					stringList.Add(strBuild.ToString());
					strBuild.Remove(0, strBuild.Length);
					if(i == text.Length -1)
					{
						int a;
						a = 10;
					}
					
					Debug.Log(nCsvLine.ToString());
					if(nCsvLine == 0)
					{
						firstCoulmnCount = stringList.Count;
					}
					else
					{
						if(firstCoulmnCount > 0 && firstCoulmnCount == stringList.Count)
						{
							csvBinaryWriter.SetData(stringList.ToArray());
							iTableStream.ParseCSV(csvBinaryWriter);
						}
						else
						{
							Debug.LogError("Csv Def Column Count  csv Row => " + nCsvLine + "Column Count => " + stringList.Count); 
						}
					}
					 
					++nCsvLine;
					stringList.Clear();
				}  
			}
		} 
		return;
	}

	static public void ConvertByteToData(ITableStream iTableStream, TextAsset textAsset)
	{
		ConvertByteToData (iTableStream, textAsset.bytes);
	}
	static public void ConvertByteToData( ITableStream iTableStream, byte[] bytes)
	{
		System.IO.MemoryStream memoryStream = new System.IO.MemoryStream (bytes); 
		System.IO.BinaryReader br = new System.IO.BinaryReader (memoryStream);  
		CsvBinaryReader csvBr = new CsvBinaryReader ();
		csvBr.SetData (br);

		while(true)
		{
			if(br.BaseStream.Length == br.BaseStream.Position)
			{
				break;
			}
			else
			{
				iTableStream.ParseCSV(csvBr);
			}
		}


	}
 
}
