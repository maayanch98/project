using ConsoleApp2.Data;
using ConsoleApp2.Data.SQLite;
using ConsoleApp2.Program;
using System;
using System.Data.SQLite;

public class Program
{
    public static void Main(string[] args)
    {
        int id = 1;
        Data.CreateTables();
        Data.CreateData();
        Store storeNew = InitStore.initStore(id);
        id++;
        SaveNewStore.saveDataStore(storeNew);

        

    }
}
