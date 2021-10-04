using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<string, patern> paternDic = new Dictionary<string, patern>();
    public LineRenderer PaternLr;
    public MeshCollider ec;
    public static GameObject gm;
    public static int MinimumLenght = 5000000;
    public static string currentPatern = "0";
    [SerializeField]
    private Material Material;
    public int segments;
    public float xradius;
    public float yradius;
    // Start is called before the first frame update
    public void Awake()
    {
        gm = new GameObject();
        ec = gm.AddComponent<MeshCollider>();
        PaternLr = gm.AddComponent<LineRenderer>();
        paternDic.Clear();
        DataSet();
        PaternLr.sortingOrder = 1;
        PaternLr.startWidth = 1f;
        PaternLr.numCapVertices = 20;
        PaternLr.numCornerVertices = 20;
        PaternLr.positionCount = 0;
        PaternLr.endWidth = 1f;
        PaternLr.material = Material;
        //ChangeCurrentPatern("1");

    }
    void AttachCollider()
    {
        Mesh mesh = new Mesh();
        mesh.Clear();
        PaternLr.BakeMesh(mesh, Camera.main, false);
        ec.sharedMesh = mesh;
    }
    public void ChangeCurrentPatern(string PaternName)
    {
        int i = 0;
        if (PaternName != "0")
        {
            foreach (Vector3 vector in paternDic[PaternName].Alpha)
                {

                    PaternLr.positionCount = i + 1;
                    PaternLr.SetPosition(i, vector);
                    i++;
                }


        }
        else 
        {
            PaternLr.positionCount = 0;
            currentPatern = PaternName;
            return;
        }

        MinimumLenght = paternDic[PaternName].minimumlenght;
        AttachCollider();
        currentPatern = PaternName;
    }
    void DataSet()
    {
        patern Alpha1 = new patern();
        List<Vector3> Alpha = new List<Vector3>();
        #region Alpha1
        Alpha.Add(new Vector3(1f, 4f, 0));
        Alpha.Add(new Vector3(-3.5f, 0, 0));
        Alpha.Add(new Vector3(3.5f, 0, 0));
        Alpha.Add(new Vector3(-1f, -4f, 0));
        Alpha1.Alpha = Alpha;
        Alpha1.minimumlenght = 350;
        paternDic.Add("1", Alpha1);
        #endregion
        patern Alpha2 = new patern();
        List<Vector3> AlphaB = new List<Vector3>();
        #region Alpha2
        AlphaB.Add(new Vector3(-5f, -2f, 0));
        AlphaB.Add(new Vector3(-3f, 4f, 0));
        AlphaB.Add(new Vector3(0f, -2.5f, 0));
        AlphaB.Add(new Vector3(3f, 4f, 0));
        AlphaB.Add(new Vector3(5f, -2f, 0));
        Alpha2.Alpha = AlphaB;
        Alpha2.minimumlenght = 450;
        paternDic.Add("2", Alpha2);
        #endregion
        #region Alpha3
        patern Alpha3 = new patern();
        List<Vector3> AlphaC = new List<Vector3>();
        AlphaC.Add(new Vector3(0f, -4f, 0));
        AlphaC.Add(new Vector3(0f, 3.5f, 0));
        AlphaC.Add(new Vector3(4.5f, -0.75f, 0));
        AlphaC.Add(new Vector3(-3.5f, -0.75f, 0));
        Alpha3.Alpha = AlphaC;
        Alpha3.minimumlenght = 550;
        paternDic.Add("3", Alpha3);
        #endregion
        #region Alpha4
        patern Alpha4 = new patern();
        List<Vector3> AlphaD = new List<Vector3>();
        AlphaD.Add(new Vector3(0f, 0f, 0));
        AlphaD.Add(new Vector3(3f, 3f, 0));
        AlphaD.Add(new Vector3(-3f, 3f, 0));
        AlphaD.Add(new Vector3(3f, -3f, 0));
        AlphaD.Add(new Vector3(-3f, -3f, 0));
        AlphaD.Add(new Vector3(0f, 0f, 0));
        Alpha4.Alpha = AlphaD;
        Alpha4.minimumlenght = 450;
        paternDic.Add("4", Alpha4);
        #endregion
        #region Alpha5
        patern Alpha5 = new patern();
        List<Vector3> AlphaE = new List<Vector3>();
        AlphaE.Add(new Vector3(4f, 4f, 0));
        AlphaE.Add(new Vector3(-4f, 4f, 0));
        AlphaE.Add(new Vector3(-4f, -4f, 0));
        AlphaE.Add(new Vector3(4f, -4f, 0));
        AlphaE.Add(new Vector3(4f, 2f, 0));
        AlphaE.Add(new Vector3(-2f, 2f, 0));
        AlphaE.Add(new Vector3(-2f, -2f, 0));
        AlphaE.Add(new Vector3(2f, -2f, 0));
        AlphaE.Add(new Vector3(2f, 0f, 0));
        AlphaE.Add(new Vector3(0f, 0f, 0));
        Alpha5.Alpha = AlphaE;
        Alpha5.minimumlenght = 800;
        paternDic.Add("5", Alpha5);
        #endregion
        #region Alpha6
        patern Alpha6 = new patern();
        List<Vector3> AlphaF = new List<Vector3>();
        AlphaF.Add(new Vector3(0, 4, 0));
        AlphaF.Add(new Vector3(0, -1.5f, 0));
        AlphaF.Add(new Vector3(-3, -1.5f, 0));
        AlphaF.Add(new Vector3(-3, 2, 0));
        AlphaF.Add(new Vector3(3, 2, 0));
        AlphaF.Add(new Vector3(3, -4, 0));
        AlphaF.Add(new Vector3(-3, -4, 0));
        Alpha6.minimumlenght = 800;
        Alpha6.Alpha = AlphaF;
        paternDic.Add("6", Alpha6);
        #endregion
    }
    public class patern
    {
        public List<Vector3> Alpha = new List<Vector3>();
        public int minimumlenght;
    }
   
   
}