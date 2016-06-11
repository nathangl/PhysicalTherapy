using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CarPerson;
using System;


public class ManyConstructors : MonoBehaviour {

	
	void Start () {
        List<Car> cList = new List<Car>();
        Car cCar1 = new Car();
        cCar1.CarName = "Toyota";
        cCar1.CarId = 11;

        Car cCar2 = new Car();
        cCar2.CarName = "Chevy";
        cCar2.CarId = 12;

        cList.Add(cCar1);
        cList.Add(cCar2);
        Person p1 = new Person();
        p1.PersonID = 5011;
        p1.FName = "Donnee";
        p1.LName = "Houssi";
        p1.GetSetCar = cList;

        //create mydata file
        CreateBasicDataFile(p1.PersonID, p1.FName, p1.LName);
        //reading from mydata file
        ReadFromBasicDataFile();
        CreatePerformanceDataFile(p1.PersonID, cCar1.CarId,
            cCar1.CarName);
        CreatePerformanceDataFile(p1.PersonID, cCar2.CarId,
            cCar2.CarName);
        ReadFromPerformanceFile();
    }

    private void CreateBasicDataFile(int id, string fn, string lname)
    {
        BinaryWriter bw;
        try
        {
            bw = new BinaryWriter(new FileStream("Assets/mydata", FileMode.Create));
            Debug.Log("File is Created");
        }
        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot create file.");
            return;
        }

        //writing into the file
        try
        {
            // bw.Write(i);
            bw.Write(id);
            bw.Write(fn);
            bw.Write(lname);

        }

        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot write to file.");
            return;
        }
        bw.Close();
    }

    private void ReadFromBasicDataFile()
    {
        BinaryReader br;
        try
        {
            br = new BinaryReader(new FileStream("Assets/mydata", FileMode.Open));
        }
        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot open file.");
            return;
        }
        try
        {
            Debug.Log("Person ID: " + br.ReadInt32());
            Debug.Log("Person First Name: " + br.ReadString());
            Debug.Log("Person Last Name: " + br.ReadString());
        }
        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot read from file.");
            return;
        }
        br.Close();
    }

    private void CreatePerformanceDataFile(int pid, int cid,
            string cname)
    {
        BinaryWriter bw;
        try
        {

            bw = new BinaryWriter(new FileStream("Assets/myPerformance", FileMode.Append));
            Debug.Log("myPerformance is Created");
        }
        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot create file.");
            return;
        }

        //writing into the file
        try
        {
            // bw.Write(i);
            bw.Write(pid);
            bw.Write(cid);
            bw.Write(cname);
            Debug.Log("writting to myPerformance is finished");
        }

        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot write to file.");
            return;
        }
        Debug.Log("Car data is written");
        bw.Close();
    }

    private void ReadFromPerformanceFile()
    {

        BinaryReader br;
        try
        {
            br = new BinaryReader(new FileStream("Assets/myPerformance", FileMode.Open));
        }
        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot open file.");
            return;
        }
        try
        {
            // first record of the file
            br.BaseStream.Position = 0;
            //End of file checking
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                Debug.Log("Person ID: " + br.ReadInt32());
                Debug.Log("Person Car Id: " + br.ReadInt32());
                Debug.Log("Person Car Name: " + br.ReadString());
            }

        }
        catch (IOException e)
        {
            Debug.Log(e.Message + "\n Cannot read from file.");
            return;
        }
        br.Close();
    }
    
}
