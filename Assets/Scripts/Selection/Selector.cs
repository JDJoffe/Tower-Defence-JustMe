using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [Header("Towers")]
    public GameObject[] towers;

    //track towers we spawn
    [Header("Holograms")]
    public GameObject[] holograms;
    [Header("Raycasts")]

    //current prefab selected
    private int currentIndex = 0;
    //check if tile has a tower on it
    #region Debug and info
    
    void DrawRay(Ray ray)
    {
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
    }
    private void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray playerRay = new Ray(transform.position, transform.forward);
        //float angle = Vector3.Angle(mouseRay.direction, playerRay.direction);
        //print(angle);
        Gizmos.color = Color.white;
        DrawRay(mouseRay);
        Gizmos.color = Color.red;
        DrawRay(playerRay);


    }
    #endregion

    /// <summary>
    /// changes currentIndex to selected index with filters
    /// </summary>
    /// <param name="index">changes to the index we want</param>
    public void SelectTower(int index)
    {
        //index range of prefabs
        if (index >= 0 && index < towers.Length)
        {
            //set current index
            currentIndex = index;
        }
    }
    /// <summary>
    /// Disables the gamemobjects of all referenced holograms
    /// </summary>
    void DisableAllHolograms()
    {
        foreach (var holo in holograms)
        {
            holo.SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        //
      
    }

    // Update is called once per frame
    void Update()
    {
        //disable all holograms at start
        DisableAllHolograms();

        //fire a raycast from camera to the mouse position 
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //perform raycast
        if (Physics.Raycast(mouseRay, out hit))
        {
            //get the placeable script
            Placeable p = hit.transform.GetComponent<Placeable>();
            
            if (p && p.isAvailable)
            {
               

                //get hologram of current tower
                GameObject hologram = holograms[currentIndex];  
                hologram.SetActive(true);
                //set position of hologram
                hologram.transform.position = p.GetPivotPoint();


                
            }
        }

    }
}
