using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

public abstract class CsvStream
{
    public abstract void GetVar(ref int outValue);
    public abstract void GetVar(ref float outValue);

    public abstract void GetVar(ref long outValue);

    public abstract void GetVar(ref double outValue);
    public abstract void GetVar(ref bool outValue);
    public abstract void GetVar(ref string outValue);
}
 