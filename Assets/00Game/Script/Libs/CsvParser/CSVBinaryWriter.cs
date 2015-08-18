using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.IO;
using UnityEngine;
 
class CsvBinaryWriter : CsvStream
{ 
    BinaryWriter m_bw;
    string[] m_strings;
    int m_currentPoint = 0;
    public CsvBinaryWriter(BinaryWriter bw)
    {
        m_bw = bw; 
    }
    public   void SetData( string[] str)
    { 
        m_strings   = str;
		m_currentPoint = 0;
    }
    public override void GetVar(ref int outValue)
    { 
		if(m_strings[m_currentPoint].Length > 0)
		{
			if (!int.TryParse(m_strings[m_currentPoint], out outValue))
			{ 
				Debug.LogError("Error:" + System.Reflection.MethodBase.GetCurrentMethod().Name); 
			}
		}
		else
		{
			Debug.LogWarning("Warning: Csv Empty Data.... string.Length == 0  set outValue = 0");
			outValue = 0;
		}

		++m_currentPoint;
        if (m_bw != null)
        {
            m_bw.Write(outValue);
        } 
    }
    public override void GetVar(ref float outValue)
    { 
		if(m_strings[m_currentPoint].Length > 0)
		{
			if (!float.TryParse(m_strings[m_currentPoint], out outValue))
			{
				Debug.LogError("Error:" + System.Reflection.MethodBase.GetCurrentMethod().Name); 
			}
		}
		else
		{
			Debug.LogWarning("Warning: Csv Empty Data.... string.Length == 0  set outValue = 0");
			outValue = 0;
		}
		++m_currentPoint;

        if (m_bw != null)
        {
            m_bw.Write(outValue);
        } 
    }
    public override void GetVar(ref long outValue)
    {
		if(m_strings[m_currentPoint].Length > 0)
		{
			if (!long.TryParse(m_strings[m_currentPoint], out outValue))
			{
				Debug.LogError("Error:" + System.Reflection.MethodBase.GetCurrentMethod().Name); 
			}
		}
		else
		{
			Debug.LogWarning("Warning: Csv Empty Data.... string.Length == 0  set outValue = 0");
			outValue = 0;
		}
		++m_currentPoint;
        if (m_bw != null)
        {
            m_bw.Write(outValue);
        }  
    }
    public override void GetVar(ref double outValue)
    { 
		if(m_strings[m_currentPoint].Length > 0)
		{
			if (!double.TryParse(m_strings[m_currentPoint], out outValue))
			{
				Debug.LogError("Error:" + System.Reflection.MethodBase.GetCurrentMethod().Name); 
			}
		}
		else
		{
			Debug.LogWarning("Warning: Csv Empty Data.... string.Length == 0  set outValue = 0");
			outValue = 0;
		}
		++m_currentPoint;
        if (m_bw != null)
        {
            m_bw.Write(outValue);
        } 
    }

    public override void GetVar(ref bool outValue)
    {
		if(m_strings[m_currentPoint].Length > 0)
		{
			if (!bool.TryParse(m_strings[m_currentPoint], out outValue))
			{
				//retry 0 or 1
				if(m_strings[m_currentPoint] == "0")
				{
					outValue = false;
				}
				else if (m_strings[m_currentPoint] == "1")
				{
					outValue = true;
				}
				else
				{
					outValue = false;
					//debug worring
				}  
			}
		}
		else
		{
			Debug.LogWarning("Warning: Csv Empty Data.... string.Length == 0  set outValue = 0");
			outValue = false;
		}
 
        ++m_currentPoint;
        if (m_bw != null)
        {
            m_bw.Write(outValue);
        } 
    }

    public override void GetVar(ref string outValue)
    {
        outValue = m_strings[m_currentPoint++];
        if (m_bw != null)
        {
            m_bw.Write(outValue);
        } 
    }
         
}
 