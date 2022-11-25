using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class Plane_terrain : MonoBehaviour
{
    string filename = "./rectangle_tsp_result.txt";
     public Terrain t;
     public TerrainCollider Terrain_collider;
     public CapsuleCollider ARM_collider;
     public GameObject arm_base;
    //  GameObject arm = arm_base.transform.Find("Cylinder").gameObject;
     List<float> pos_list = new List<float>();
     float terrain_origin_x;
     float terrain_origin_z;
     int index;
     float c = 1.018f;
     float ratio; // terrain/unit
     float[,,] map ;
     int count;
     bool flag;
     public List<List<float>> row_list = new List<List<float>>();
     List<float> path_list = new List<float>();
     float [,] path;
     int path_count;
     List<int> row_count = new List<int>();
    //  float width = 5.5f; // times of width of pixel, default width = 1
     string line;
     List<float> sublist = new List<float>();
     int row_times;
     float arm_height;
     int row_size;
     float bound_size; // unit
     float offset_z;
     float offset_x;
     float path_width;
     public float scale;
     float resize_scale;
     public float posx;
     public float posz;

     
     void Awake(){
        // t.transform.position = new Vector3(Terrain_collider.bounds.min.x, 0, Terrain_collider.bounds.min.z);
        terrain_origin_x = Terrain_collider.bounds.max.x;
        terrain_origin_z = Terrain_collider.bounds.min.z;
        map = new float[t.terrainData.alphamapWidth, t.terrainData.alphamapHeight, 2];
        index = 0;
        offset_z = terrain_origin_z; // unit
        offset_x = Terrain_collider.bounds.min.x - terrain_origin_x; // unit

        row_size = 0;
        StreamReader reader = new StreamReader(filename);
        line = reader.ReadLine();
        line = reader.ReadLine();
        line = reader.ReadLine();
        string[] pathwidth = line.Split(':');
        path_width = Convert.ToSingle(pathwidth[1]);

        line = reader.ReadLine();
        string[] scaletimes = line.Split(':');
        resize_scale = Convert.ToSingle(scaletimes[1]);
        Debug.Log("resize = " + resize_scale);

        while (!reader.EndOfStream)
        { 
            sublist = new List<float>();
            line = reader.ReadLine();
            string[] entries = line.Split(',');
            if(entries[0] == "0" && entries[entries.Length - 1] == "0"){
                continue;
            }
            else{
                for(int i = 0; i < entries.Length; i = i + 1){
                    sublist.Add(Convert.ToSingle(entries[i]));
                }

                row_list.Add(sublist);
                row_count.Add(entries.Length);
                row_size = row_size + 1;
            }
            
        }
     }

    // Blend the two terrain textures according to the steepness of
    // the slope at each point.
    void Start(){
        // For each point on the alphamap...
        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                map[y, x, 0] = 1;
            }
        }
        flag = false;
        t.terrainData.SetAlphamaps(0, 0, map);
        path_count = 0;
        row_times = 0;
        index = 0;
        arm_height = 110f; // unit
        
    }
    void FixedUpdate()
    {        
        if(row_times == row_size){
            enabled = false;
        }
        else{
            if(flag){
                if(path_count >= count){
                    flag = false;
                }
                else{
                    Armcontrol(path_list[path_count]* c, path_list[path_count + 1]* c, path_width);
                    path_count = path_count + 2;
                    // arm_height = arm_height + 0.1f; // test
                }    
            }
            else{
                arm_height = 106f; // test
                if(index + 3 < row_count[row_times]){
                    Pathgenerator(row_list[row_times][index], row_list[row_times][index + 1], row_list[row_times][index + 2], row_list[row_times][index + 3]);
                    path_count = 0;
                    index = index + 2;
                    flag = true;
                }
                else{
                    row_times = row_times + 1;
                    index = 0;
                }
            } 
        }  
           
    }
    void Armcontrol (float px, float pz, float arm_y){ 
        // float base_width = 3f; // default area = 1 terrain pixel
        // float arm_length = 100f; // unit
        // float area_scale = 0.1f;
        // pz = pz - posz;
        // px = px - posx;
        pz = (pz - posz/(1.5f)) * scale;
        px = (px - posx/(1.5f))* scale;
        float px_temp = px;
        float pz_temp = pz;
        pz = pz + (Terrain_collider.bounds.min.z);
        px = -px + (Terrain_collider.bounds.max.x);
        
        arm_base.transform.position = new Vector3(px, arm_height, pz);
        // RaycastHit hit;
        // if(Physics.Raycast( (arm_base.transform.position + new Vector3(0, -(arm_length), 0) ), arm_base.transform.TransformDirection(Vector3.down), out hit, 500)){
        //     if(hit.point.y < arm_base.transform.position.y - arm_length){
        //         arm_y = ((arm_base.transform.position.y - (arm_length) - hit.point.y)* area_scale + 1)* base_width;
        //         Setpixelcolor(px_temp, pz_temp, arm_y);
        //     }
        // }
        Setpixelcolor(px_temp, pz_temp, (arm_y* 1.5f - 5f)* scale);
        
    }

    void Setpixelcolor (float px, float pz, float width){
        if(Terrain_collider.bounds.size.x > Terrain_collider.bounds.size.z){
            bound_size = Terrain_collider.bounds.size.x;
        }
        else{
            bound_size = Terrain_collider.bounds.size.z;
        }
        
        ratio = (t.terrainData.heightmapResolution/bound_size);
        pz = (pz - offset_z)* ratio;
        px = (-(px + offset_x))* ratio;

        for (int y = (int)Math.Round(pz - (ratio* width)/2); y <= (int)Math.Round(pz + (ratio* width)/2); y++)
        {
            if(y < 0 || y > t.terrainData.heightmapResolution - 2){
                continue;
            }
            for (int x = (int)Math.Round(px - (ratio* width)/2); x <= (int)Math.Round(px + (ratio* width)/2); x++)
            {
                if(x < 0 || x > t.terrainData.heightmapResolution - 2){
                    continue;
                }
                // Get the normalized terrain coordinate that
                // corresponds to the point.
                float normX = x * 1.0f / (t.terrainData.alphamapWidth - 1);
                float normZ = y * 1.0f / (t.terrainData.alphamapHeight - 1);

                // Get the steepness value at the normalized coordinate.
                var angle = t.terrainData.GetSteepness(normX, normZ);
                var hight = t.terrainData.GetInterpolatedHeight(normX, normZ);
                // Debug.Log(hight);

                // Steepness is given as an angle, 0..90 degrees. Divide
                // by 90 to get an alpha blending value in the range 0..1.
                var frac = angle / 90.0;
                // Debug.Log("x = " + x);
                // Debug.Log("y = " + y);
                
                map[y, x, 0] = (float)frac;
                map[y, x, 1] = (float)(1 - frac);
            }
        }


        
        t.terrainData.SetAlphamaps(0, 0, map);

    }

    void Pathgenerator(float start_x, float start_z, float end_x, float end_z){
        float length_x = end_x - start_x;
        float length_z = end_z - start_z;
        float length = 0;
        float gap = 1; // default 1 pixel
        path_list = new List<float>();
        count = 0;
        if(length_x == 0){
            length = end_z;
            if(length_z > 0){
                for(float i = start_z; i <= length; i = i + gap){
                    path_list.Add(start_x);
                    if(i > length){
                        path_list.Add(length);
                    }
                    else{
                        path_list.Add(i);
                    }
                    count = count + 2;
                }
            }
            else{
                for(float i = start_z; i >= length; i = i - gap){
                    path_list.Add(start_x);
                    if(i < length){
                        path_list.Add(length);
                    }
                    else{
                        path_list.Add(i);
                    }
                    count = count + 2;
                }
            }
        }
        else{
            length = end_x;
            if(length_x > 0){
                for(float i = start_x; i <= length; i = i + gap){
                    if(i > length){
                        path_list.Add(length);
                    }
                    else{
                        path_list.Add(i);
                    }
                    path_list.Add(start_z);
                    count = count + 2;
                }
            }
            else{
                for(float i = start_x; i >= length; i = i - gap){
                    if(i < length){
                        path_list.Add(length);
                    }
                    else{
                        path_list.Add(i);
                    }
                    path_list.Add(start_z);
                    count = count + 2;
                }
            }
        }
        
        
    }
}
