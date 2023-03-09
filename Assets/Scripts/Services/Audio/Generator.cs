using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Services.Audio {
    public class Generator : MonoBehaviour {

        private const string PathToSoundDirectory = "Assets/Sounds";
        private const string FileName = @"Assets/Scripts/Generate/AudioList.cs";    

        [MenuItem("AudioManager/GenerateAudioList")]
        private static void GenerateSoundsList() {
            
            var info = new DirectoryInfo(PathToSoundDirectory);
            var fileInfo = info.GetFiles();
            var fileNames = new List<string>();
            foreach (var file in fileInfo) {
                if (!file.Extension.Contains(".meta")) {
                    fileNames.Add(file.Name.Replace(file.Extension, ""));
                }
            }

            GenerateFile(fileNames);
        }

        private static void GenerateFile(List<string> files) {
            FileInfo fi = new FileInfo(FileName);    
    
            try {    
                if (fi.Exists) {    
                    fi.Delete();    
                }

                using FileStream fs = fi.Create();
                WriteToFile(fs, "/* " +
                                "\n ATTENTION " +
                                "\n THIS CODE HAS BEEN GENERATED" +
                                "\n DO NOT EDIT" +
                                "\n */");

                WriteToFile(fs, "\n " +
                                "public static class AudioList { ");
                 
                foreach (var fileName in files) {
                    WriteToFile(fs, $"\n \t public static string {fileName};");
                }

                WriteToFile(fs, "\n }");
            }    
            catch (Exception Ex) {    
                Console.WriteLine(Ex.ToString());    
            }
        }

        private static void WriteToFile(FileStream fileStream, string text) {
            var info = new UTF8Encoding(true).GetBytes(text);
            fileStream.Write(info, 0, info.Length);
        }
    }
}