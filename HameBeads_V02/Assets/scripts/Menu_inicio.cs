using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu_inicio : MonoBehaviour
{
    //public List<Sprite> fondos;
    //public float time;
    //public Image Boton;
    // Use this for initialization
    void Start()
    {
        //Boton. = fondos[Random.Range(0, 3)];
    }

    // Update is called once per frame
    void Update()
    {
        //time = time + Time.deltaTime;
        //if (time >= 3)
        //{
        //    for (int i = 0; i < fondos.Count - 1; i++)
        //    {
        //        if (Boton.sprite == fondos[i])
        //        {
        //            i++;
        //            if (i > fondos.Count - 1)
        //            {
        //                Boton.sprite = fondos[0];

        //            }
        //            else
        //            {
        //                Boton.sprite = fondos[i];
        //            }
        //            time = 0;
        //        }
        //    }
        //}

    }
    public void Cambio()
    {
        SceneManager.LoadScene(1);
    }
}
