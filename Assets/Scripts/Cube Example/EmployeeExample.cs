using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Employee
{
    public int EmpId { get; set; }
    public string EmpName { get; set; }
    public double Salary { get; set; }
    public string City { get; set; }
}


public class EmployeeExample : MonoBehaviour
{


    void Start()
    {

        List<Employee> empList = new List<Employee>()
        {
         new Employee { EmpId= 101, EmpName= "Mark" },
         new Employee { EmpId = 102, EmpName = "John"},
         new Employee {EmpId= 103, EmpName = "Mary" }
        };

        Employee empl =
            empList.Find(emp => emp.EmpId == 102);
        Debug.Log(empl.EmpId);
    }




}