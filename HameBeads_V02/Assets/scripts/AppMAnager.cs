using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class AppMAnager : MonoBehaviour
{
    public Image BaseBead, Cruz;
    public Canvas MainCanvas;
    public Image MainColour;
    public int FilaX, ColumnaY;
    private int colourswitch;
    private List<Image> Beads = new List<Image>();
    private List<Color> Colores = new List<Color>();
    public List<Button> Botones;
    public CanvasGroup yea;
    private bool reapareceCanvas = false, activarPote, activar_pincel = true;
    private float aparece;
    private Color pote;
    private AudioManager Manager;
    private int currentPatronId = -1;


    public Vector3 Prueba;

    [System.Serializable]
    public class Patrones
    {
        public string PatronName;
        public List<Vector2> Patron;
    }
    public List<Patrones> allPatrones;
    // Use this for initialization
    void Start()
    {
        print(Prueba.magnitude.ToString("00.00"));
        GameObject Beads_Group = new GameObject("All Beads");
        Beads_Group.transform.SetParent(MainCanvas.transform);
        Beads_Group.transform.localScale = Vector3.one;
        for (int i = 0; i < FilaX; i++)
        {
            for (int j = 0; j < ColumnaY; j++)
            {
                Image NewBead = Instantiate(BaseBead);
                NewBead.transform.SetParent(Beads_Group.transform);
                NewBead.transform.position = new Vector2(BaseBead.transform.position.x + i * 0.6f, BaseBead.transform.position.y - j * 0.6f);
                NewBead.transform.localScale = Vector3.one;
                NewBead.gameObject.SetActive(true);
                Beads.Add(NewBead);
            }
        }
        MainColour.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        Manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (reapareceCanvas)
        {
            aparece = aparece + Time.deltaTime;
            if (aparece >= 1.5f)
            {
                yea.alpha = 1;
                reapareceCanvas = false;
            }
        }
    }
    public void GetColour(Image MyColour)
    {
        MainColour.color = MyColour.color;
    }
    public void TakeFoto()
    {
        aparece = 0;
        yea.alpha = 0;
        string DateTime = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
        ScreenCapture.CaptureScreenshot("Hamabeads_" + DateTime + ".png");
        reapareceCanvas = true;
        print(DateTime);
    }
    public void EraseColour()
    {
        for (int i = 0; i < Beads.Count; i++)
        {
            Beads[i].color = Color.white;
        }
    }
    public void PaintBeads(Image BeadImage)
    {
        if (activar_pincel)
        {
            if (Input.GetMouseButton(0))
            {
                BeadImage.color = MainColour.color;
                Manager.CreateAudio(0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                BeadImage.color = MainColour.color;
                Manager.CreateAudio(0);
            }
        }
        else
        {
            if (activarPote) /*pote de pintura*/
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pote = BeadImage.color;
                    Manager.CreateAudio(0);
                    PaintBeads_BeadsExtend(BeadImage);
                }
            }
            else if (currentPatronId >= 0)
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 centropatron = BeadImage.transform.position;
                    Manager.CreateAudio(0);
                    for (int i = 0; i < Beads.Count; i++)
                    {
                        for (int j = 0; j < allPatrones[currentPatronId].Patron.Count; j++)
                        {
                            if (Beads[i].transform.position.x.ToString("00.00") == (centropatron.x + allPatrones[currentPatronId].Patron[j].x).ToString("00.00") &&
                                Beads[i].transform.position.y.ToString("00.00") == (centropatron.y + allPatrones[currentPatronId].Patron[j].y).ToString("00.00"))
                            {
                                Beads[i].color = MainColour.color;
                            }
                        }
                    }
                }
        }
    }
    public void PaintBeadsPot()
    {

        activarPote = true;
        activar_pincel = false;
        for (int i = 0; i < Botones.Count; i++)
        {
            if (Botones[i].interactable == false)
            {
                Botones[i].interactable = true;
            }
        }
        Botones[4].interactable = false;
    }
    public void GetChangeColour()
    {
        for (int i = 0; i < Beads.Count; i++)
        {
            if (!Colores.Contains(Beads[i].color))
            {
                Colores.Add(Beads[i].color);
            }
        }
    }
    public void SwitchColour()
    {
        Colores.Clear();
        GetChangeColour();
        for (int i = 0; i < Beads.Count; i++)
        {
            for (int j = 0; j < Colores.Count; j++)
            {
                if (Beads[i].color == Colores[j]) /*si el color del Bead es = al color j de la lista*/
                {
                    j++;
                    if (j > Colores.Count - 1) /*si ese color es mas grande o = a 0 al tamaño de la lista*/
                    {

                        Beads[i].color = Colores[0];
                    }
                    else
                    {
                        Beads[i].color = Colores[j]; /*cambia el color del Bead por el siguiente color de la lista de Colores*/
                    }
                }
            }
        }
    }
    public void ActivarPatron(int patronId)
    {
        Botones[currentPatronId + 1].interactable = true;
        activar_pincel = false;
        activarPote = false;
        currentPatronId = patronId;
        for (int i = 0; i < Botones.Count; i++)
        {
            if (Botones[i].interactable == false)
            {
                Botones[i].interactable = true;
            }
        }
        Botones[currentPatronId + 1].interactable = false;
    }
    public void pincel()
    {
        activar_pincel = true;
        activarPote = false;
        for (int i = 0; i < Botones.Count; i++)
        {
            if (Botones[i].interactable == false)
            {
                Botones[i].interactable = true;
            }
        }
        Botones[0].interactable = false;

    }
    public void FueraJUego()
    {

        Application.Quit();

    }

    public void PaintBeads_BeadsExtend(Image BeadImage)
    {
        Color OldColor = BeadImage.color;
        BeadImage.color = MainColour.color;
        Vector2 centropatron = BeadImage.transform.position;

        int Coincidencias = 0;
        for (int i = 0; i < Beads.Count; i++)
        {
            if (Coincidencias < 4)
            {
                if (Beads[i].color == OldColor)
                    if (Beads[i].transform.position.x.ToString("00.00") == (centropatron.x + 0.6f).ToString("00.00") &&
                        Beads[i].transform.position.y.ToString("00.00") == (centropatron.y).ToString("00.00"))
                    {
                        PaintBeads_BeadsExtend(Beads[i]);
                        Coincidencias++;
                    }
                    else if (Beads[i].transform.position.x.ToString("00.00") == (centropatron.x - 0.6f).ToString("00.00") &&
                             Beads[i].transform.position.y.ToString("00.00") == (centropatron.y).ToString("00.00"))
                    {
                        PaintBeads_BeadsExtend(Beads[i]);
                        Coincidencias++;
                    }
                    else if (Beads[i].transform.position.x.ToString("00.00") == (centropatron.x).ToString("00.00") &&
                             Beads[i].transform.position.y.ToString("00.00") == (centropatron.y + 0.6).ToString("00.00"))
                    {
                        PaintBeads_BeadsExtend(Beads[i]);
                        Coincidencias++;
                    }
                    else if (Beads[i].transform.position.x.ToString("00.00") == (centropatron.x).ToString("00.00") &&
                             Beads[i].transform.position.y.ToString("00.00") == (centropatron.y - 0.6f).ToString("00.00"))
                    {
                        PaintBeads_BeadsExtend(Beads[i]);
                        Coincidencias++;
                    }
            }
            else
                return;

        }
    } 

}



