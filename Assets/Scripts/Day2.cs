using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public struct Report
{
    public List<int> report;
}

public class Day2 : MonoBehaviour
{

    [SerializeField] int iterations = 100;
    List<double> part1Time = new List<double>();
    List<double> part2Time = new List<double>();


    List<Report> reports = new List<Report>();

    string filename = "data_input_2.txt";

    // Start is called before the first frame update
    void Start()
    {
    string filepath = Application.dataPath + "/" + "Data" + "/" + filename;

    if (File.Exists(filepath))
    {
        StreamReader reader = new StreamReader(filepath);

        while (!reader.EndOfStream)
        {
            Report report = new Report();
            report.report = new List<int>();
            string line = reader.ReadLine();
            
            string[] values = line.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var value in values)
            {
                report.report.Add(int.Parse(value));
            }

            UnityEngine.Debug.Log(report.report.Count);

            reports.Add(report);

        }

        reader.Close();
    }
    else
    {
        UnityEngine.Debug.Log("File Not Found");
    }

    Stopwatch oneStopwatch = new Stopwatch();
    Stopwatch twoStopwatch = new Stopwatch();

    oneStopwatch.Start();
    FindSafe(reports);
    oneStopwatch.Stop();

    twoStopwatch.Start();
    FindTolerantSafe(reports);
    twoStopwatch.Stop();

    long distanceTicks = oneStopwatch.ElapsedTicks;
    long similarityTicks = twoStopwatch.ElapsedTicks;

    long freq = Stopwatch.Frequency;

    double distanceMicro = (double) distanceTicks / freq * 1000000;
    double similarityMicro = (double) similarityTicks / freq * 1000000;

    // part1Time.Add(distanceMicro);
    // part2Time.Add(similarityMicro);

    // double part1Ave = part1Time.Average();
    // double part2Ave = part2Time.Average(); 

    // UnityEngine.Debug.Log($"Part 1 Elapsed Time: {oneStopwatch.ElapsedMilliseconds}ms");
    // UnityEngine.Debug.Log($"Part 2 Elapsed Time: {twoStopwatch.ElapsedMilliseconds}ms");

    UnityEngine.Debug.Log($"Part 1 Elapsed Time: {distanceMicro}us");
    UnityEngine.Debug.Log($"Part 2 Elapsed Time: {similarityMicro}us");

    }

    void FindSafe(List<Report> reports)
    {
        int safeCount = 0;

        foreach (Report report in reports)
        {
            if (CheckSafe(report.report))
            {
                safeCount += 1;
            }
        }

        UnityEngine.Debug.Log($"Safe Reports: {safeCount}");
    }

    void FindTolerantSafe(List<Report> reports)
    {
        int safeCount = 0;

        foreach (Report report in reports)
        {
            if (CheckTolerantSafe(report.report))
            {
                safeCount += 1;
            }
        }

        UnityEngine.Debug.Log($"Safe Reports: {safeCount}");
    }

    bool CheckSafe(List<int> report)
    {
        bool increasing = true;
        bool safe = true;
        
        if (!CheckIncreasing(report[0], report[1]))
        {
            increasing = false;
        }
        
        for (int i = 1; i < report.Count; i++)
        {
            if (!CheckIncreasing(report[i - 1], report[i]) == increasing || 
            Mathf.Abs(report[i-1] - report[i]) > 3 ||
            Mathf.Abs(report[i-1] - report[i]) < 1)
            {
                safe = false;
            }
        }

        return safe;
    }

    bool CheckIncreasing(int num1, int num2)
    {
        if (num2 > num1) { return true; }

        return false;
    }

    bool CheckTolerantSafe(List<int> report)
    {
        bool increasing = true;
        bool error = false;
        
        if (!CheckIncreasing(report[0], report[1]))
        {
            increasing = false;
        }
        
        for (int i = 1; i < report.Count; i++)
        {
            if (!CheckIncreasing(report[i - 1], report[i]) == increasing || 
            Mathf.Abs(report[i-1] - report[i]) > 3 ||
            Mathf.Abs(report[i-1] - report[i]) < 1)
            {
                if (!error)
                {
                    error = true;
                }
                else
                {
                    return false;
                }
            }
        }

        report.RemoveAt(0);

        if (CheckSafe(report))
        {
            return true;
        }

        return true;
    }

}
