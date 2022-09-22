using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEditor;
//using UnityEngine.CoreModule;

public class placingCities220719 : MonoBehaviour
{
    public static int cityNumberIO = 120;
    static int count = 0;
 //   public const int cityNumberIO = 6;
    public const int localStationNumber = 4;
    public GameObject city;
    //    public List<GameObject> cities;
    public int thickness = 1000;
    public GameObject[] cities = new GameObject[cityNumberIO];
    static float[,] stationxs = new float[cityNumberIO/2, localStationNumber];
    static float[,] stationys = new float[cityNumberIO/2, localStationNumber];
    static float[] max = new float[cityNumberIO/2];
    static float [,] redx = new float[cityNumberIO, 10*cityNumberIO];
    static float [,] redz = new float[cityNumberIO, 10*cityNumberIO];
    static int N = 8; // Brush width
    static float [,] ori = new float[cityNumberIO/2, 4];
    static float long0;
    static float long1;
    static float short0;
    static float short1;
    static float opt_distance = 0f;
    static float long_distance = 0f;
    static float short_distance = 0f;
    static float drawmax_x;
    static float drawmax_z;
    static float drawmin_x;
    static float drawmin_z;
    static int[] randnum = new int [cityNumberIO/2];
    static float resize;
    Vector3 orginal = new Vector3(0f, 0f, 0f);
    

static float[,] dist_mat = new float[cityNumberIO, cityNumberIO];
    //   private float optdis = 99999f;
    //   private string line, path = "t4.tsp";
    //    public Text ValueTxt;

    private StreamReader reader;
    private StreamReader reader2;
    /*    private string line, path = "att48s.tsp";
        private string pathopt = "att48opts.tsp";
            void readInCoord()  //tsplib format
            {
                reader = new StreamReader(path);
                int cityindex = 0;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] entries = line.Split(' ');
                    //         Debug.Log(entries[0] );
                    float xpt = Convert.ToSingle(entries[1]);
                    float ypt = Convert.ToSingle(entries[2]);
                    Vector3 pos = new Vector3(xpt, 0f, ypt);
                    GameObject go = (GameObject)Instantiate(city, pos,
                                                                                  Quaternion.identity);
                    go.name = (cityindex).ToString();
                    cities[cityindex] = go;
                    cityindex++;
                    // file.WriteLine("xpts.Add(" + xpt.ToString() + "); " + "ypts.Add(" +ypt.ToString() + ")",true);
                    //      Debug.Log("xpts.Add(" + xpt + "); " + "ypts.Add(" + ypt + ")");
                }
                //        ValueTxt.text = (xpts.Count).ToString() + "," + (ypts.Count).ToString();
                reader.Close();
            }
            */
    //   private string line, path = "210520gluing2.csv";
    //    private string line, path = "tspinput.csv";
 //   private string line, path = "output_org/tspinput000000000000010000000000010 ";
    //private string line, path = "output_org/tspinput000010001101010000000000110 ";
    //private string line, path = "output_org/rectangle_position_2.txt ";
    // private string line, path = "output_org/new_rectangle_position_8.txt ";
    // private string line2, path2 = "output_org/new_rectangle_position_8.txt ";
    private string line, path = "output_org/2.txt";
    private string line2, path2 = "output_org/2.txt";
    
     public static void DrawThickLine(Vector3 start, Vector3 end, float thickness)
     {
         Camera c = Camera.current;
         if (c == null) return;
 
         // Only draw on normal cameras
         if (c.clearFlags == CameraClearFlags.Depth || c.clearFlags == CameraClearFlags.Nothing)
         {
             return;
         }
 
         // Only draw the line when it is the closest thing to the camera
         // (Remove the Z-test code and other objects will not occlude the line.)
         var prevZTest = Handles.zTest;
         Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
 
         Handles.color = Gizmos.color;
         Handles.DrawAAPolyLine(thickness * 10, new Vector3[] { start, end });
 
         Handles.zTest = prevZTest;
     }
    void readInCoord()  //gluing format
    {
         reader2 = new StreamReader(path2);
         line2 = reader2.ReadLine();
         resize = Convert.ToSingle(line2);
         while(!reader2.EndOfStream){
            line2 = reader2.ReadLine();
            count++;
            Debug.Log("count: " + count);
        }
        reader2.Close();
        cityNumberIO = 2*count;
        //for (int ii = 0; ii < cityNumberIO/2;ii++) {
        //    for (int i = 0; i < localStationNumber ; i++)
            //{ stationxs[ii, i] = -1;  stationys[ii, i] = -1;}
        //}
        int cityindex = 0;
        int k = 0;
        //line = reader.ReadLine();   //discard the first two lines
        reader = new StreamReader(path);
        line = reader.ReadLine();
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            string[] entries = line.Split(',');
            //         Debug.Log(entries[0] );
            float xpt0 = Convert.ToSingle(entries[0]);
            float ypt0 = Convert.ToSingle(entries[1]);
            // float xpt1 = Convert.ToSingle(entries[2]);
            // float ypt1 = Convert.ToSingle(entries[3]);
            // float xpt1 = Convert.ToSingle(entries[4]);
            // float ypt1 = Convert.ToSingle(entries[5]);
            float xpt1 = Convert.ToSingle(entries[entries.Length - 2]);
            float ypt1 = Convert.ToSingle(entries[entries.Length - 1]);
            // ori[k, 0] = xpt0;
            // ori[k, 1] = ypt0;
            // ori[k, 2] = xpt1;
            // ori[k, 3] = ypt1;
            // k++;
            // Debug.Log("oript: " + ori[k,0] + "," + k);
            Vector3 posleftup = new Vector3(xpt0, 0f, ypt0);
            // Vector3 posrightup = new Vector3(xpt1, 0f, ypt1);
            // Vector3 posrightdown = new Vector3(xpt2, 0f, ypt2);
            // Vector3 posleftup = new Vector3(xpt3, 0f, ypt3);
            GameObject go = (GameObject)Instantiate(city, posleftup, Quaternion.identity);
            go.name = (cityindex).ToString();
            cities[cityindex] = go;
            
            for (int i = 0; i < (entries.Length-1)/2; i++)
            {
                //Debug.Log("entries.Length "+ entries.Length);
                //stationxs[cityindex/2, i] = Convert.ToSingle(entries[2 * i]);//2*i + 1
                //stationys[cityindex/2, i] = Convert.ToSingle(entries[2 * i + 1]); //2*i + 2
            }
            cityindex++;
            Vector3 posrightdown = new Vector3(xpt1, 0f, ypt1);
            GameObject go1 = (GameObject)Instantiate(city, posrightdown, Quaternion.identity);
            go1.name = (cityindex).ToString();
            cities[cityindex] = go1;
            cityindex++;
            //Debug.Log("cityindex: " + xpt0 + "," + cityindex);
            //count++;
            Debug.Log("cityindex: " + cityindex + "," + cityNumberIO);
            // // Third
            // Vector3 posrightup = new Vector3(xpt2, 0f, ypt2);
            // GameObject go2 = (GameObject)Instantiate(city, posrightup, Quaternion.identity);
            // go2.name = (cityindex).ToString();
            // cities[cityindex] = go2;
            // cityindex++;
            // // Fourth
            // Vector3 posleftdown = new Vector3(xpt3, 0f, ypt3);
            // GameObject go3 = (GameObject)Instantiate(city, posleftdown, Quaternion.identity);
            // go3.name = (cityindex).ToString();
            // cities[cityindex] = go3;
            // cityindex++;
            // file.WriteLine("xpts.Add(" + xpt.ToString() + "); " + "ypts.Add(" +ypt.ToString() + ")",true);
            //      Debug.Log("xpts.Add(" + xpt + "); " + "ypts.Add(" + ypt + ")");
        }
        //        ValueTxt.text = (xpts.Count).ToString() + "," + (ypts.Count).ToString();
        reader.Close();
    }
    private static int[] opt_route = new int[cityNumberIO];
    public static float optdis = 999999f;
    private string pathopt = "att48opts.tsp";
    void readInOpt()
    {
        reader = new StreamReader(pathopt);
        int cityindex = 0;
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            string[] entries = line.Split(' ');
            //Debug.Log(entries[0]);
            int visitingPt = Convert.ToInt16(entries[0]);
            //         Debug.Log("visiting pt" + visitingPt);
            opt_route[cityindex] = visitingPt - 1;
            cityindex++;
        }
        reader.Close();
    }
    void fillUpDistMat()
    {
        for (int i = 0; i < cityNumberIO; i++)
        {
            for (int j = 0; j < cityNumberIO; j++)
            {
                // Eucli-dist
                //              dist_mat[i, j] = Vector3.Distance(cities[i].transform.position,
                //                                     cities[j].transform.position);
                // max of abs(x1-x2), abs(y1-y2)
                dist_mat[i, j] = Math.Max(Math.Abs(cities[i].transform.position.x - cities[j].transform.position.x),
                                                              Math.Abs(cities[i].transform.position.z - cities[j].transform.position.z));
                dist_mat[0, j] = 0; dist_mat[1, j] = 0;
                dist_mat[j,0] = 0; dist_mat[j,1] = 0;
            }
        }
    }
    void distortDistMat()
    {
        for (int i = 0; i < cityNumberIO / 2; i++)
        {
            dist_mat[2 * i, 2 * i + 1] = -10000f;
            dist_mat[2 * i + 1, 2 * i] = -10000f;
        }
    }
    void Shuffle(int[] array)
    {
        Random random = new Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            //          int i = Random.Range(1, n + 1);  // first point not changed
            int i = Random.Range(0, n + 1);  // first point changed
            int temp = array[i];
            array[i] = array[n];
            array[n] = temp;
        }
    }
    public static int tsp_pter = 0;
    public static int[] initial_route;
    public static int[] best_route;
    public static float bbest_distance = 7777777f;

    public class Tsp_solver
    {
        //       public int[] bbest_route = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //       public int[] new_route = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public int[] bbest_route = new int[cityNumberIO];
        public int[] new_route = new int[cityNumberIO];
        public float new_distance = 0f;
        //       public  float bbest_distance = 88888f;
        int[] distances = { };
        //       int num_cities = initial_route.Length;
        //     optdis=calculate_distance(opt_route);
        public void two_opt()
        {
            bbest_distance = 666666f;
            //        Debug.Log("zzz 1 " + bbest_distance);
            ////          optdis= calculate_distance(opt_route);
            bbest_distance = calculate_distance(initial_route);
            ////           Debug.Log("opt_route optdis " + optdis);

            for (int i = 0; i < cityNumberIO; i++) { bbest_route[i] = initial_route[i]; }
            float improvement_factor = 1;
            float previous_best = 9999999f;
            while (bbest_distance < previous_best)
            //           while (improvement_factor > 0.01)
            {
                previous_best = bbest_distance;
                for (int i = 0; i < cityNumberIO - 1; i++)
                {
                    for (int j = i + 1; j < cityNumberIO - 1; j++)
                    {
                        //                    Debug.Log("dddbefore zzz i  " + i + " j " + j + " bbest_distance " + bbest_distance);
                        twoOptSwap(bbest_route, i, j);
                        if (calculate_distance(new_route) < bbest_distance)
                        {
                            bbest_distance = calculate_distance(new_route);
                            for (int k = 0; k < cityNumberIO; k++)
                            {
                                bbest_route[k] = new_route[k];
                            }
                        }
                        //                Debug.Log("dddafter zzz i  " + i + " j " + j + " bbest_distance " + bbest_distance);
                    }
                }
                improvement_factor = 1 - bbest_distance / previous_best;
            }
        }
        public void twoOptSwap(int[] bbest_route, int swap_first, int swap_last)
        {
            //          Debug.Log("ddd swap_first=" + swap_first + "swap_last=" + swap_last);
            for (int i = 0; i < swap_first; i++) { new_route[i] = bbest_route[i]; }
            for (int i = swap_last; i >= swap_first; i--)
            { new_route[swap_last + swap_first - i] = bbest_route[i]; }
            for (int i = swap_last + 1; i < cityNumberIO; i++) { new_route[i] = bbest_route[i]; }
            //        for(int i=0; i < num_cities; i++) {
            //            Debug.Log("ddd bbest_route[" + i + "]=" + bbest_route[i]+
            //            "new_route[" + i + "]="+ new_route[i]); }
            //          Debug.Log("ddd bbest_distance=" + calculate_distance(bbest_route) +
            //           " new_route=" + calculate_distance(new_route));
        }
        //      float optdis=calculate_distance(opt_route);
        public float calculate_distance(int[] path)
        {
            float result_distance = 0f;
            for (int i = 0; i < path.Length - 1; i++)
            {
                result_distance += dist_mat[path[i], path[i + 1]];
                //              Debug.Log("path[i] " + path[i] + "path[i+1] " + path[i+1] + " dist_mat[path[i]+ path[i + 1]] " + dist_mat[path[i], path[i + 1]]);
            }
            result_distance += dist_mat[path[path.Length - 1], path[0]];
            //        Debug.Log("loop dist " + result_distance);
            return result_distance;
        }
    }
    private string outpath = "rectangle_tsp_result.txt";
    //private string outpath = "rectangle_tsp_result2.txt";
    public void Tsp()
    {
        int iteration = 500;
   //     int iteration = 500;
        //      string[] cities_names = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        //        string[] cities_names = { "00", "11", "22", "33", "44", "55", "66", "77", "88", "99" };
        string[] cities_names = new string[cityNumberIO];
        best_route = new int[cityNumberIO];
        initial_route = new int[cityNumberIO];
        for (int i = 0; i < cityNumberIO; i++)
        {
            best_route[i] = i;
            initial_route[i] = i;
            cities_names[i] = Convert.ToString(i);
        }

        float best_distance = 99999;
        while (iteration > 0)
        {
            iteration--;
            Shuffle(initial_route);
            //        Debug.Log("initial_route " + initial_route[0] + initial_route[1]
            // +initial_route[2] + initial_route[3] + initial_route[4] + initial_route[5]);
         //                  + initial_route[6] + initial_route[7] + initial_route[8] + initial_route[9]);

            Tsp_solver tsp_solver = new Tsp_solver();
            //          Debug.Log("aaaa1: itera " + iteration + " bbest_distance " + bbest_distance);
            tsp_solver.two_opt();
            if (bbest_distance < best_distance)
            {
                best_distance = bbest_distance;
                for (int i = 0; i < initial_route.Length; i++)
                {
                    best_route[i] = tsp_solver.bbest_route[i];
                }
            }
            //           Debug.Log("aaaa2:xxxx itera " + iteration
            //               + " best_distance" + best_distance + " error " + (best_distance - optdis) / optdis);
            float best_distance_norm2 = best_distance + 10000 * cityNumberIO / 2;

            // Debug.Log("aaaa2:xxxx itera " + iteration
            //     + " best_distance" + best_distance_norm2);
        }
        float best_distance_norm = best_distance + 10000 * cityNumberIO / 2;
        //Debug.Log("xxxx: best dis :" + best_distance_norm);
        for (int i = 0; i < cityNumberIO; i++)
        {
 //           Debug.Log("xxxx: " + cities_names[best_route[i]]
    //            + "cities[best_route[i]].transform.position" + cities[best_route[i]].transform.position);
            // Debug.Log(cities_names[best_route[i]]
            //     + " " + cities[best_route[i]].transform.position);
           // Debug.Log("test5: " + cities[best_route[i]].transform.position+ "," + i);
        }
        // for (int i = 0; i < cityNumberIO-1; i++) comment here test
        // {
        //     Debug.DrawLine(cities[best_route[i]].transform.position,
        //         cities[best_route[i + 1]].transform.position, Color.red, 0.2f);
        // }
        // Debug.DrawLine(cities[best_route[cityNumberIO - 1]].transform.position, 
        //     cities[best_route[0]].transform.position, Color.red, 0.2f);
        // 
        float turn = 0;
        int num = 0;
        int numd = 0;
        int rand = 0;
        // for(int i = 0; i < 10; i++){
        //     rand = Random.Range(0,3);
        //     Debug.Log("rand: " + rand);
        // }
        for(int i = 0; i < cityNumberIO/2 - 1; i++)
        {   
            rand = Random.Range(0, 4);
            //rand = 0;
            randnum[i] = rand;
            if(rand == 0 || rand == 2){
            if( Math.Max(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) 
              - Math.Min(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) >
                Math.Max(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) 
              - Math.Min(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) )
            {
                long0 = cities[best_route[2 * i + 1]].transform.position.x;
                long1 = cities[best_route[2 * i + 2]].transform.position.x;
                short0 = cities[best_route[2 * i + 1]].transform.position.z;
                short1 = cities[best_route[2 * i + 2]].transform.position.z;
            }
            else
            {
                long0 = cities[best_route[2 * i + 1]].transform.position.z;
                long1 = cities[best_route[2 * i + 2]].transform.position.z;
                short0 = cities[best_route[2 * i + 1]].transform.position.x;
                short1 = cities[best_route[2 * i + 2]].transform.position.x;
            }}
            else if(rand == 1 || rand == 3){
            if( Math.Max(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) 
              - Math.Min(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) >
                Math.Max(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) 
              - Math.Min(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) )
            {
                long0 = cities[best_route[2 * i + 2]].transform.position.x;
                long1 = cities[best_route[2 * i + 1]].transform.position.x;
                short0 = cities[best_route[2 * i + 1]].transform.position.z;
                short1 = cities[best_route[2 * i + 2]].transform.position.z;
            }
            else
            {
                long0 = cities[best_route[2 * i + 2]].transform.position.z;
                long1 = cities[best_route[2 * i + 1]].transform.position.z;
                short0 = cities[best_route[2 * i + 1]].transform.position.x;
                short1 = cities[best_route[2 * i + 2]].transform.position.x;
            }}
            numd = Mathf.FloorToInt(num / N);
            turn = Math.Max(cities[best_route[2 * i + 2]].transform.position.z, cities[best_route[2 * i + 1]].transform.position.z)
                  -Math.Min(cities[best_route[2 * i + 2]].transform.position.z, cities[best_route[2 * i + 1]].transform.position.z);
            turn = Math.Max(short0, short1) - Math.Min(short0, short1);
            num =  Mathf.FloorToInt(turn);
            numd = Mathf.FloorToInt(num / N);
            if(rand == 2 || rand == 3)
            {
                if(num < N){
                    numd = 5;
                }
                else if(Mathf.FloorToInt(num / N) % 2 == 0)
                {
                    numd = Mathf.FloorToInt(num / N) + 3;
                }
                else
                {
                    numd = Mathf.FloorToInt(num / N) + 2;
                }
            }
            else
            {
                if(num < N){
                    numd = 4;
                }
                else if(Mathf.FloorToInt(num / N) % 2 == 0)
                {
                    numd = Mathf.FloorToInt(num / N) + 2;
                }
                else
                {
                    numd = Mathf.FloorToInt(num / N) + 3;
                }
            }
            //numd = numd + 2;
            //Debug.Log("turn: " + turn + "," + numd + "," + i);
            for(int j = 0; j < 2*numd + 2; j = j + 1)
            {
                
                if(j % 2 == 0)
                {
                    //redz[i, j] = Convert.ToSingle(cities[best_route[2 * i + 1]].transform.position.z) +  ((cities[best_route[2 * i + 2]].transform.position.z - cities[best_route[2 * i + 1]].transform.position.z) / (2*numd)) * j;
                    redz[i, j] = Convert.ToSingle(short0) +  ((short1 - short0) / (2*numd)) * j;
                }
                else
                {
                    redz[i, j] = redz[i, j - 1];
                }
                //Debug.Log("redz: " + redx[i, j] + "," + redz[i, j] + "," + i + j);
                //Debug.Log("redz: " + cities[best_route[0]].transform.position + "," + cities[best_route[cityNumberIO/2 + 1]].transform.position + "," + cities[best_route[cityNumberIO/2]].transform.position + "," + i + j);
            }
            for(int j = 0; j < 2*numd + 2; j++)
            {
                if(j % 4 == 1 || j % 4 == 2)
                {
                    //redx[i, j] = cities[best_route[2 * i + 2]].transform.position.x;
                    redx[i, j] = long1;
                }
                else if(j % 4 == 3 || j % 4 == 0)
                {
                    //redx[i, j] = cities[best_route[2 * i + 1]].transform.position.x;
                    redx[i, j] = long0;
                }
                //Debug.Log("redz: " + redx[i, j] + "," + redz[i, j] + "," + i + j);
                //Debug.Log("redx: " + cities[best_route[2 * i + 1]].transform.position.x + "," +cities[best_route[2 * i + 2]].transform.position.x + "," + redx[i, j] + "," + i + j);
            }
            // for(int j = 0; j < 2*numd + 2; j++)
            // {
            //     long_distance += Mathf.Sqrt(Mathf.Pow(redx[i, j], 2f) + Mathf.Pow(redz[i, j], 2f));
            //     Debug.Log("long: " + long_distance);
            // }
        }
        if(rand == 0 || rand == 2){
        if( Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) 
          - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) >
            Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) 
          - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) )
        {
            long0 = cities[best_route[cityNumberIO - 1]].transform.position.x;
            long1 = cities[best_route[0]].transform.position.x;
            short0 = cities[best_route[cityNumberIO - 1]].transform.position.z;
            short1 = cities[best_route[0]].transform.position.z;
        }
        else
        {
            long0 = cities[best_route[cityNumberIO - 1]].transform.position.z;
            long1 = cities[best_route[0]].transform.position.z;
            short0 = cities[best_route[cityNumberIO - 1]].transform.position.x;
            short1 = cities[best_route[0]].transform.position.x;
        }}
        else if(rand == 1 || rand == 3){
        if( Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) 
          - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) >
            Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) 
          - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) )
        {
            long0 = cities[best_route[0]].transform.position.x;
            long1 = cities[best_route[cityNumberIO - 1]].transform.position.x;
            short0 = cities[best_route[cityNumberIO - 1]].transform.position.z;
            short1 = cities[best_route[0]].transform.position.z;
        }
        else
        {
            long0 = cities[best_route[0]].transform.position.z;
            long1 = cities[best_route[cityNumberIO - 1]].transform.position.z;
            short0 = cities[best_route[cityNumberIO - 1]].transform.position.x;
            short1 = cities[best_route[0]].transform.position.x;
        }}
        turn = Math.Max(short0, short1) - Math.Min(short0, short1);
        num = Mathf.FloorToInt(turn);
        numd = Mathf.FloorToInt(num / N);
        if(num < N){
            numd = 2;
        }
        else if(Mathf.FloorToInt(num / N) % 2 == 0)
        {
            numd = Mathf.FloorToInt(num / N);
        }
        else
        {
            numd = Mathf.FloorToInt(num / N) + 1;
        }
        //numd = numd + 2;
        for(int j = 0; j < 2*numd + 2; j = j + 1)
            {
                if(j % 2 == 0)
                {
                    redz[cityNumberIO - 1, j] = Convert.ToSingle(short0) +  ((short1 - short0) / (2*numd)) * j;
                }
                else
                {
                    redz[cityNumberIO - 1, j] = redz[cityNumberIO - 1, j - 1];
                }
                //Debug.Log("redz: " + redx[i, j] + "," + redz[i, j] + "," + i + j);
            }
        for(int j = 0; j < 2*numd + 2; j++)
        {
            if(j % 4 == 1 || j % 4 == 2)
            {
                redx[cityNumberIO - 1, j] = long1;
            }
            else if(j % 4 == 3 || j % 4 == 0)
            {
                redx[cityNumberIO - 1, j] = long0;
            }
            //Debug.Log("redz: " + redx[i, j] + "," + redz[i, j] + "," + i + j);
            //Debug.Log("redx: " + cities[best_route[2 * i + 1]].transform.position.x + "," +cities[best_route[2 * i + 2]].transform.position.x + "," + redx[i, j] + "," + i + j);
        }
        for(int i = 0; i < cityNumberIO; i++){
            for(int j = 0; j < 2*numd + 2; j++){
                //Debug.Log("redz: " + redx[i, j] + "," + redz[i, j] + "," + i + j);
                //Debug.Log("redz: " + redx[cityNumberIO - 1, j] + "," + redz[cityNumberIO - 1, j] + "," + i + j);
            }
        }
        float glue = 0;
        for(int i = 0; i < cityNumberIO / 2; i++)
        {
            for(int j = 0; j < 2*numd + 1; j++){
                glue += Mathf.Sqrt(Mathf.Pow(redx[i + 1, j] - redx[i,j], 2f) + Mathf.Pow(redz[i + 1, j] - redz[i,j], 2f));
                Debug.Log("glue: " + glue); 
            }
            opt_distance += Mathf.Sqrt(Mathf.Pow(cities[best_route[2 * i + 1]].transform.position.x - cities[best_route[2 * i]].transform.position.x,2f) + Mathf.Pow(cities[best_route[2 * i + 1]].transform.position.z - cities[best_route[2 * i]].transform.position.z,2f));
            Debug.Log("OPT: " + opt_distance);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        float starttime = Time.realtimeSinceStartup;
        Debug.Log("starttime " + starttime);
        readInCoord();
        //readInOpt();
        fillUpDistMat();
        distortDistMat();
        Tsp();
        float endtime = Time.realtimeSinceStartup;
        Debug.Log("time take" + (endtime - starttime));
    }

    //    void FixedUpdate()
    void Update()
    {
        Debug.Log("city: " + cityNumberIO);
        for (int i = 0; i < cityNumberIO; i++) {
           // Debug.Log("best_route[ " +i+" ] " + cities[best_route[i]].transform.position );
        }
        if ((best_route[0] % 2 == 0 && best_route[1]== best_route[0]+1)||
            (best_route[0] % 2 != 0 && best_route[1] == best_route[0] -1))
        {
            //     for (int i = 0; i < cityNumberIO / 2 - 1; i++)
    //        Debug.Log("cityNumberIO/2 "+cityNumberIO / 2+" " +cityNumberIO);
   //         for (int i = 0; i < cityNumberIO / 2 - 1; i++)
                for (int i = 0; i < cityNumberIO / 2; i++)
                {
                //     Debug.DrawLine(cities[best_route[2 * i+1]].transform.position,
                // cities[best_route[2 * i + 2]].transform.position, Color.cyan, 222f, false);
  //              Debug.Log("cities[best_route[2 * i+1]].transform.position i= " + i + " "
   //                 + cities[best_route[2 * i + 1]].transform.position);
    //            Debug.Log("[best_route[2 * i+1]] i= " + i + " "
     //               + best_route[2 * i + 1]);
                }
            // Debug.DrawLine(cities[best_route[cityNumberIO-1]].transform.position,
            // cities[best_route[0]].transform.position, Color.blue, 222f, false);

        }
        else
        {
     //       for (int i = 0; i < cityNumberIO / 2 - 1; i++)
                StreamWriter sw = new StreamWriter(outpath);
                sw.Write("Best Route Point: " + "\n");
                sw.Write("(x,y)= " + "\n");
                sw.Write("Width: " + N + "\n");
                sw.Write("Fator: " + resize + "\n");
                //Debug.DrawLine(orginal, cities[best_route[0]].transform.position, Color.blue, 222f, false); 
                for (int i = 0; i < cityNumberIO / 2 - 1; i++)
                {
                    //Debug.Log("test2: " + cities[best_route[i]].transform.position);
//                     Debug.DrawLine(cities[best_route[2 * i]].transform.position,
// //      cities[best_route[2 * i + 1]].transform.position, Color.green, 222f, false);
//                 cities[best_route[2 * i + 1]].transform.position, Color.yellow, 222f, false);
            //    Vector3 yel1 = new Vector3(redx[i + 1,0],0f,redz[i + 1,0]);
            //    Vector3 yel2 = new Vector3(redx[i,2*num2[i] + 1],0f,redz[i,2*num2[i] + 1]);
            //    Debug.DrawLine(yel2,yel1,Color.yellow,222f,false);
               // Debug.Log("best_routeyellow[ " + (2 * i) + " ] " + best_route[2 * i]+"best_routeyellow[ " + (2 * i+1) + " ] " + best_route[2 * i+1]);
                }
                float turn = 0;
                int num = 0;
                int numd = 0;
                int k = 0;
                for (int i = 0; i < cityNumberIO / 2 - 1; i++)
                {
                    Debug.DrawLine(cities[best_route[2 * i]].transform.position,
//      cities[best_route[2 * i + 1]].transform.position, Color.green, 222f, false);
                cities[best_route[2 * i + 1]].transform.position, Color.yellow, 222f, false);
//                 Debug.Log("best_routeyellow[ " + (2 * i) + " ] " + best_route[2 * i]+"best_routeyellow[ " + (2 * i+1) + " ] " + best_route[2 * i+1]);
                
                //opt_distance += Mathf.Sqrt(Mathf.Pow(cities[best_route[2 * i + 1]].transform.position.x - cities[best_route[2 * i]].transform.position.x,2f) + Mathf.Pow(cities[best_route[2 * i + 1]].transform.position.z - cities[best_route[2 * i]].transform.position.z,2f));
                //Debug.Log("temp4: " + opt_distance);
                //if(i<cityNumberIO/2 - 1){
                drawmax_x=Mathf.Max(cities[best_route[cityNumberIO - 1]].transform.position.x,cities[best_route[0]].transform.position.x);
                drawmax_z=Mathf.Max(cities[best_route[cityNumberIO - 1]].transform.position.z,cities[best_route[0]].transform.position.z);
                drawmin_x=Mathf.Min(cities[best_route[cityNumberIO - 1]].transform.position.x,cities[best_route[0]].transform.position.x);
                drawmin_z=Mathf.Min(cities[best_route[cityNumberIO - 1]].transform.position.z,cities[best_route[0]].transform.position.z); 
                Vector3 posleftup = new Vector3(drawmin_x, 0f, drawmax_z);
                Vector3 posleftdown = new Vector3(drawmin_x, 0f, drawmin_z);
                Vector3 posrightup = new Vector3(drawmax_x, 0f, drawmax_z);
                Vector3 posrightdown = new Vector3(drawmax_x, 0f, drawmin_z);
                //
                // float turn = 0;
                // int num = 0;
                // int numd = 0;
                 if( Math.Max(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) 
                    - Math.Min(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) >
                        Math.Max(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) 
                    - Math.Min(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) )
                    {
                        long0 = cities[best_route[2 * i + 1]].transform.position.x;
                        long1 = cities[best_route[2 * i + 2]].transform.position.x;
                        short0 = cities[best_route[2 * i + 1]].transform.position.z;
                        short1 = cities[best_route[2 * i + 2]].transform.position.z;
                    }
                    else
                    {
                        long0 = cities[best_route[2 * i + 1]].transform.position.z;
                        long1 = cities[best_route[2 * i + 2]].transform.position.z;
                        short0 = cities[best_route[2 * i + 1]].transform.position.x;
                        short1 = cities[best_route[2 * i + 2]].transform.position.x;
                    }
                // turn = Math.Max(cities[best_route[2 * i + 2]].transform.position.z, cities[best_route[2 * i + 1]].transform.position.z)
                //       -Math.Min(cities[best_route[2 * i + 2]].transform.position.z, cities[best_route[2 * i + 1]].transform.position.z);
                turn = Math.Max(short0, short1) - Math.Min(short0, short1);
                num = Mathf.FloorToInt(turn);
                if(randnum[i] == 2 || randnum[i] == 3)
                {
                    if(num < N){
                        numd = 5;
                    }
                    else if(Mathf.FloorToInt(num / N) % 2 == 0)
                    {
                        numd = Mathf.FloorToInt(num / N) + 3;
                    }
                    else
                    {
                        numd = Mathf.FloorToInt(num / N) + 2;
                    }
                }
                else{
                if(num < N)
                {
                    numd = 4;
                }
                else if(Mathf.FloorToInt(num / N) % 2 == 0)
                {
                    numd = Mathf.FloorToInt(num / N) + 2;
                }
                else
                {
                    numd = Mathf.FloorToInt(num / N) + 3;
                }}
                //numd = numd + 2;
                Debug.Log("turn: " + turn + "," + numd + "," + i);
                for(int j = 0; j < 2*numd + 1; j++){
                    if( Math.Max(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) 
                      - Math.Min(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) >
                        Math.Max(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) 
                      - Math.Min(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) )
                    {
                        Vector3 red1 = new Vector3(redx[i, j],0f,redz[i, j]);
                        Vector3 red2 = new Vector3(redx[i, j + 1],0f,redz[i, j + 1]);
                        Debug.DrawLine(red1, red2, Color.red, 222f, false);
                        //Debug.Log("num: " + numd + i + "," + j);
                    }
                    else
                    {
                        Vector3 red1 = new Vector3(redz[i, j],0f,redx[i, j]);
                        Vector3 red2 = new Vector3(redz[i, j + 1],0f,redx[i, j + 1]);
                        Debug.DrawLine(red1, red2, Color.red, 222f, false);
                        //Debug.Log("num: " + num + "," + j);
                    } 
                    // Vector3 red1 = new Vector3(redx[i, j],0f,redz[i, j]);
                    // Vector3 red2 = new Vector3(redx[i, j + 1],0f,redz[i, j + 1]);
                    // Debug.DrawLine(red1, red2, Color.red, 222f, false);
                    // Debug.Log("num: " + num + "," + j);
                }
                for(int j = 0; j < 2*numd + 2; j++){
                    //Debug.Log("test0: " + redx[i,j] + "," + redz[i,j] + "," + i + "," + j);
                    if( Math.Max(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) 
                    - Math.Min(cities[best_route[2 * i + 1]].transform.position.x, cities[best_route[2 * i + 2]].transform.position.x) >
                        Math.Max(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) 
                    - Math.Min(cities[best_route[2 * i + 1]].transform.position.z, cities[best_route[2 * i + 2]].transform.position.z) )
                    {
                    if(j == 0){
                        sw.Write(redx[i, j].ToString() + "," + redz[i, j].ToString());
                    }
                    else if(j == 2*numd + 1){
                        sw.Write("," + redx[i, j].ToString() + "," + redz[i, j].ToString());
                    }
                    else{sw.Write("," + redx[i, j].ToString() + "," + redz[i, j].ToString());} }
                    else {
                        if(j == 0){
                        sw.Write(redz[i, j].ToString() + "," + redx[i, j].ToString());
                    }
                    else if(j == 2*numd + 1){
                        sw.Write("," + redz[i, j].ToString() + "," + redx[i, j].ToString());
                    }
                    else{sw.Write("," + redz[i, j].ToString() + "," + redx[i, j].ToString());}
                    }
                    
                }
                //} here
                //sw.Write("\n");
                k = i;
                // for(int j = 0; j < 2*numd + 2; j++){
                //     //Debug.Log("test0: " + redx[i,j] + "," + redz[i,j] + "," + i + "," + j);
                //     sw.Write(redx[i, j].ToString() + "," + redz[i, j].ToString());
                // }
                if(k < cityNumberIO/2 - 2) {sw.Write("\n");} 
                //Draw multi line
                // for(float j = drawmin_x; j < drawmax_x + 2; j = j + 0.5f) {
                //     Vector3 posup = new Vector3(j, 0f, drawmax_z);
                //     Vector3 posdown = new Vector3(j, 0f, drawmin_z);
                //     Debug.DrawLine(posup,posdown, Color.red, 222f, false);
                // }
                // Debug.DrawLine(posleftup,posleftdown, Color.red, 222f, false);
                // Debug.DrawLine(posleftup,posrightup, Color.red, 222f, false);
                // Debug.DrawLine(posrightup,posrightdown, Color.red, 222f, false);
                // Debug.DrawLine(posrightdown,posleftdown, Color.red, 222f, false);
                //DrawThickLine(cities[best_route[2 * i + 1]].transform.position,  
                //cities[best_route[2 * i + 2]].transform.position, thickness);
            
                }
                if( Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) 
                    - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) >
                        Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) 
                    - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) )
                    {
                        long0 = cities[best_route[cityNumberIO - 1]].transform.position.x;
                        long1 = cities[best_route[0]].transform.position.x;
                        short0 = cities[best_route[cityNumberIO - 1]].transform.position.z;
                        short1 = cities[best_route[0]].transform.position.z;
                    }
                    else
                    {
                        long0 = cities[best_route[cityNumberIO - 1]].transform.position.z;
                        long1 = cities[best_route[0]].transform.position.z;
                        short0 = cities[best_route[cityNumberIO - 1]].transform.position.x;
                        short1 = cities[best_route[0]].transform.position.x;
                    }
                    turn = Math.Max(short0, short1) - Math.Min(short0, short1);
                    num = Mathf.FloorToInt(turn);
                    numd = Mathf.FloorToInt(num / N);
                    if(num < N){
                        numd = 2;
                    }
                    else if(Mathf.FloorToInt(num / N) % 2 == 0)
                    {
                        numd = Mathf.FloorToInt(num / N);
                    }
                    else
                    {
                        numd = Mathf.FloorToInt(num / N) + 1;
                    }
                    //numd = numd + 2;
                for(int p = 0; p < 2*numd + 1; p++){
                    if( Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) 
                    - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) >
                        Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) 
                    - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) )
                    {
                        Vector3 red3 = new Vector3(redx[cityNumberIO - 1, p],0f,redz[cityNumberIO - 1, p]);
                        Vector3 red4 = new Vector3(redx[cityNumberIO - 1, p + 1],0f,redz[cityNumberIO - 1, p + 1]);
                        Debug.DrawLine(red3, red4, Color.red, 222f, false);
                    }
                    else{
                        Vector3 red3 = new Vector3(redz[cityNumberIO - 1, p],0f,redx[cityNumberIO - 1, p]);
                        Vector3 red4 = new Vector3(redz[cityNumberIO - 1, p + 1],0f,redx[cityNumberIO - 1, p + 1]);
                        Debug.DrawLine(red3, red4, Color.red, 222f, false);
                    }
                }
                //sw.Write(redx[cityNumberIO/2, j].ToString() + "," + redz[cityNumberIO/2, j].ToString() + ",");
                //Debug.DrawLine(cities[best_route[cityNumberIO/2]].transform.position, orginal, Color.blue, 222f, false); 
               // Debug.Log("here: ");
                sw.Write("\n");
                for(int i = 0; i < 2*numd + 2; i++){
                    if( Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) 
                    - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.x, cities[best_route[0]].transform.position.x) >
                        Math.Max(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) 
                    - Math.Min(cities[best_route[cityNumberIO - 1]].transform.position.z, cities[best_route[0]].transform.position.z) )
                    {
                    if(i == 0){
                        sw.Write(redx[cityNumberIO - 1, i].ToString() + "," + redz[cityNumberIO - 1, i].ToString());
                    }
                    else{
                        sw.Write("," + redx[cityNumberIO - 1, i].ToString() + "," + redz[cityNumberIO - 1, i].ToString());
                    }
                    }
                    else{
                    if(i == 0){
                        sw.Write(redz[cityNumberIO - 1, i].ToString() + "," + redx[cityNumberIO - 1, i].ToString());
                    }
                    else{
                        sw.Write("," + redz[cityNumberIO - 1, i].ToString() + "," + redx[cityNumberIO - 1, i].ToString());
                    }
                    }
                }
                sw.Close();
            //Debug.Log("Opt_Distance: " + opt_distance);
   //         Debug.DrawLine(cities[best_route[cityNumberIO -1]].transform.position,
    //              cities[best_route[0]].transform.position, Color.green, 222f, false);
                  
    //        Debug.Log("best_routegreen[ " + (cityNumberIO - 1 )+ " ] " + best_route[cityNumberIO - 1] + "best_routegreen[ " + (0) + " ] " + best_route[0]);
        

        }
        //         Debug.Log("opt_route i"+i +" "+opt_route[i]);
        ////       Debug.DrawLine(cities[opt_route[i]].transform.position,
        ////               cities[opt_route[i + 1]].transform.position, Color.green, 222f, false);

        for (int i = 0; i < cityNumberIO / 2; i++)
        {
        //    Debug.DrawLine(cities[2 * i].transform.position,
           //          cities[2 * i + 1].transform.position, Color.red, 222f, false);
        }
        for (int ii = 0; ii < cityNumberIO / 2; ii++)
        {
            for (int i = 0; i < localStationNumber-1; i++)
            {   
               // Debug.Log("stationxs: " + stationxs[0, i]);
    //             if (stationxs[ii, i+1] > -1)
    //             {    
    //  //               Debug.Log("stationxs[" + ii + ", " + i + "]," + " stationys[ii, i]" + stationxs[ii, i] + " " + stationys[ii, i]);
    //                 Debug.DrawLine(new Vector3(stationxs[ii, i], 0, stationys[ii, i]),
    //           new Vector3(stationxs[ii, i + 1], 0, stationys[ii, i + 1]), Color.red, 222f, false);
    //             }
            }
        }
        for (int i = 0; i < cityNumberIO / 2; i++)
        {
         //   Debug.DrawLine(cities[2 * i].transform.position,
            //         cities[2 * i + 1].transform.position, Color.red, 222f, false);
        }
        //     Debug.Log("xxxx: opt dis :" + optdis);

        //    Debug.DrawLine(cities[best_route[cityNumberIO - 1]].transform.position,
        //     cities[best_route[0]].transform.position, Color.green, Time.deltaTime, true);
    }
}
