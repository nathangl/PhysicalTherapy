using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CarPerson
{
    
        class Car
        {
            private string _carName;
            private int _carId;
            public string CarName
            {
                get { return _carName; }
                set { _carName = value; }
            }
            public int CarId
            {
                get { return _carId; }
                set { _carId = value; }
            }
        }

        class Person
        {
            private string fName, lName;
            private int _personId;
            private List<Car> _pCars = new List<Car>();

            public string FName
            {
                get; set;
            }
            public string LName
            {
                get; set;
            }
            public int PersonID
            {
                get; set;
            }
            public List<Car> GetSetCar
            {
                get { return _pCars; }
                set { _pCars = value; }
            }
            public void Print()
            {
                Debug.Log("ID:  " + this.PersonID);
                Debug.Log("First Name: " + this.FName);
                Debug.Log("Last Name:" + this.LName);
                Debug.Log("Owns the following cars: ");
                foreach (Car c in this.GetSetCar)
                {
                    Debug.Log("Car Name: " + c.CarName);
                }
            }
        }
    

}

