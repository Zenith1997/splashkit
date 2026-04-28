using System;
using System.Collections.Generic;

// -----------------------------
// Employee = table row
// -----------------------------


public enum columnOptions
{
    Name,
    Dept,
    Salary

}
public class Employee
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
public class AggregateState
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
  public class GroupBy
{
     
     columnOptions columnName;
        List<Employee> employees;
public GroupBy(columnOptions columnName, List<Employee> employees)
    {

      this.columnName =columnName;
      this.employees = employees;
    
    }
    public void groupBy()
   
    {
   
      
              // GROUP BY logic
        // -----------------------------
        Dictionary<string, AggregateState> groups =
            new Dictionary<string, AggregateState>();

        foreach (Employee row in employees)
        {
                  string key;
            // Step 1: extract group key
           if(columnName == columnOptions.Dept)
            {
                 key = row.Dept;
            }
            else if (columnName == columnOptions.Name)
            {
                key=row.Name;
            }
              else if (columnName == columnOptions.Salary)
            {
                key=row.Salary.ToString();
            }
            else
            {
                throw new Exception("Unassigned key value");
            }

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
        Console.WriteLine("Dept | Count | Sum");

        foreach (var entry in groups)
        {
            string dept = entry.Key;
            AggregateState state = entry.Value;

            Console.WriteLine(
                $"{dept} | {state.Count} | {state.SumSalary}"
            );
        }
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
        GroupBy g = new GroupBy(columnOptions.Dept,employees);
        g.groupBy();
  
}
}