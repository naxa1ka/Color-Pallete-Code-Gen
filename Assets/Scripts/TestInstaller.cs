using System.IO;
using UnityEngine;

public class TestInstaller : MonoBehaviour
{
    [SerializeField] private RunTimeColorSchemeProvider _schemeProvider;


    private void Start()
    {
        string json = "";
        using (var reader = new StreamReader(@"C:\Ilya\GH\text.json"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                json += line;
            }
        }

        _schemeProvider.Install(json);
    }
}