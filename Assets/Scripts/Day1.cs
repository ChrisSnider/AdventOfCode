using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using Unity.VisualScripting;
using UnityEngine;

public class Day1 : MonoBehaviour
{
    List<int> list1 = new List<int>();
    List<int> list2 = new List<int>();

    string filename = "data_input_1.txt";
    
    // Start is called before the first frame update
    void Start()
    {

        string filepath = Application.dataPath + "/" + "Data" + "/" + filename;

        if (File.Exists(filepath))
        {
            StreamReader reader = new StreamReader(filepath);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                
                string[] values = line.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                list1.Add(int.Parse(values[0]));
                list2.Add(int.Parse(values[1]));
            }

            reader.Close();
        }
        else{
            UnityEngine.Debug.Log("File Not Found");
        }

        Stopwatch distanceStopwatch = new Stopwatch();
        Stopwatch similarityStopwatch = new Stopwatch();

        distanceStopwatch.Start();
        FindDistances(list1, list2);
        distanceStopwatch.Stop();

        similarityStopwatch.Start();
        FindSimilarity(list1, list2);
        similarityStopwatch.Stop();

        UnityEngine.Debug.Log($"Distance Elapsed Time: {distanceStopwatch.ElapsedMilliseconds}ms");
        UnityEngine.Debug.Log($"Similarity Elapsed Time: {similarityStopwatch.ElapsedMilliseconds}ms");

        long distanceTicks = distanceStopwatch.ElapsedTicks;
        long similarityTicks = similarityStopwatch.ElapsedTicks;

        long freq = Stopwatch.Frequency;

        double distanceMicro = (double) distanceTicks / freq * 1000000;
        double similarityMicro = (double) similarityTicks / freq * 1000000;

        UnityEngine.Debug.Log($"Distance Elapsed Time: {distanceMicro}micros");
        UnityEngine.Debug.Log($"Similarity Elapsed Time: {similarityMicro}micros");
    
    }

    int FindDistances(List<int> list1, List<int> list2)
    {
        list1.Sort();
        list2.Sort();

        int sum = 0;

        for (int i = 0; i < list1.Count; i++)
        {
            sum += Mathf.Abs(list1[i] - list2[i]);
        }
    
        UnityEngine.Debug.Log($"Total distance: {sum}");

        return sum;
    }

    int FindSimilarity(List<int> list1, List<int> list2)
    {
        Dictionary<int, int> enumeratedList = new Dictionary<int, int>();

        foreach (int entry in list2)
        {
            if (!enumeratedList.ContainsKey(entry))
            {
                enumeratedList.Add(entry, 1);
            }
            else
            {
                enumeratedList[entry] += 1;
            }
        }

        int similarity = 0;

        foreach (int entry in list1)
        {
            if (enumeratedList.ContainsKey(entry))
            {
                similarity += enumeratedList[entry] * entry;
            }
            
        }

        UnityEngine.Debug.Log($"Similarity: {similarity}");
    
        return similarity;
    }   


}
