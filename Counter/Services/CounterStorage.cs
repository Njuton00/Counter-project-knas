using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Maui.Storage;
using Counter.Models;

namespace Counter.Services;

public class CounterStorage
{
    public void SaveCounters(List<CounterItem> counters)
    {
        string json = JsonSerializer.Serialize(counters);

        Preferences.Set("Counters", json);
    }

    public List<CounterItem> LoadCounters()
    {
        string json = Preferences.Get("Counters", "");

        if (string.IsNullOrEmpty(json)) 
        {
            return new List<CounterItem>();
        }

        List<CounterItem> counters =
            JsonSerializer.Deserialize<List<CounterItem>>(json);

        return counters ?? new List<CounterItem>();
    }
}
