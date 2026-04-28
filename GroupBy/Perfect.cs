// using System;
// using System.Collections.Generic;



// class Employee
// {
//     public string Name;

//     public string Dept;

//     public int Salary;


//     public Employee(string name, string dept, int salary)
//     {
//         Name = name;
//         Dept = dept;
//         Salary = salary;

//     }
// }

// class AggregateState
// {
//     public int Count = 0;
//     public int SumSalary = 0;

//     public void Update(Employee e)
//     {
//         Count++;
//         SumSalary+=e.Salary;
//     }
// }


// class Perfect
// {
//      static void Main(string[] args)
//     {
        
//         List<Employee> list = new List<Employee>
//         {
//          new Employee("John", "IT", 4000),
//             new Employee("Sara", "IT", 7000),
//             new Employee("Mike", "HR", 6000),
//             new Employee("Anna", "HR", 3000),
//             new Employee("David", "IT", 8000)
//         };


        
//     //group by logic

//     Dictionary<string,AggregateState> groups= new Dictionary<string,AggregateState>();

//     foreach (Employee row in list)
//     {
//         string key =row.Dept;

//             if (!groups.ContainsKey(key))
//             {
//                 groups[key]=new AggregateState();
//             }

//             groups[key].Update(row);

//     }
//     }


    
// }