using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.IO;

 
class CsvBinaryReader : CsvStream
{
    BinaryReader m_bs;
    public   void SetData(BinaryReader bs)
    {
        m_bs = bs; 
    }
    public override void GetVar(ref int getValue)
    { 
        getValue = m_bs.ReadInt32(); 
    }
    public override void GetVar(ref float getValue)
    { 
        getValue = m_bs.ReadSingle(); 

    }
    public override void GetVar(ref long getValue)
    { 
        getValue = m_bs.ReadInt64(); 
    }
    public override void GetVar(ref double getValue)
    { 
        getValue = m_bs.ReadDouble(); 
    }

    public override void GetVar(ref bool getValue)
    { 
        getValue = m_bs.ReadBoolean(); 
    }

    public override void GetVar(ref string getValue)
    { 
        getValue = m_bs.ReadString(); 
    }
} 