using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 


public abstract class ITableStream
{
    public abstract void ParseCSV(CsvStream csvStream);
}
