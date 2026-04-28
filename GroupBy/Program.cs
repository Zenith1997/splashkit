using System;
using System.Collections.Generic;

// -----------------------------
// Employee = table row
// -----------------------------
class Employee
{
    public string Name;
    public string Dept;
    public int Salary;

    public Employee(string name, string dept, int salary)
    {
        Name = name;
        Dept = dept;
        Salary = salary;
    }
}

// -----------------------------
// AggregateState = group summary
// -----------------------------
class AggregateState
{
    public int Count = 0;
    public int SumSalary = 0;

    // Update aggregates
    public void Update(Employee e)
    {
        Count++;                     // COUNT(*)
        SumSalary += e.Salary;       // SUM(salary)
    }

    public double Avg()
    {
        return Count == 0 ? 0 : (double)SumSalary / Count;
    }
}

// -----------------------------
// Main class (ENTRY POINT)
// -----------------------------
class Program
{
    static void Main(string[] args)
    {
        // -----------------------------
        // DATA (like a table)
        // -----------------------------
        List<Employee> employees = new List<Employee>
        {
            new Employee("John", "IT", 4000),
            new Employee("Sara", "IT", 7000),
            new Employee("Mike", "HR", 6000),
            new Employee("Anna", "HR", 3000),
            new Employee("David", "IT", 8000)
        };

        // -----------------------------
        // GROUP BY logic
        // -----------------------------
        Dictionary<string, AggregateState> groups =
            new Dictionary<string, AggregateState>();

        foreach (Employee row in employees)
        {
            // Step 1: extract group key
            string key = row.Dept;

            // Step 2: lookup group
            if (!groups.ContainsKey(key))
            {
                // Step 3: create group if not exists
                groups[key] = new AggregateState();
            }

            // Step 4: update aggregates
            groups[key].Update(row);
        }

        // -----------------------------
        // OUTPUT
        // -----------------------------
        Console.WriteLine("Dept | Count | Sum | Avg");

        foreach (var entry in groups)
        {
            string dept = entry.Key;
            AggregateState state = entry.Value;

            Console.WriteLine(
                $"{dept} | {state.Count} | {state.SumSalary} | {state.Avg()}"
            );
        }
    }
}