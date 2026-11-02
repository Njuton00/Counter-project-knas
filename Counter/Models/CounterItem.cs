using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter.Models;

public class CounterItem
{
    public string Name { get; set; }
    public int Value { get; set; }
    public int InitialValue { get; set; }
    public string ColorName { get; set; }

    public CounterItem(string name, int value, int initialValue, string colorName)
    {
        Name = name;
        Value = value;
        InitialValue = initialValue;
        ColorName = colorName;
    }
}