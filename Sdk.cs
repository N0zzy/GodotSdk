using System;
using System.IO;
using Godot;
using SharpieSdk;
using SharpieSdk.Service;

namespace Php
{
    namespace Sdk 
    {
        public class Sdk: AssemblyIterator
        {
            /// settings - will need to create a configuration to get the sdk root
            private readonly string pathPhp = "/GodotPhp";
            
            public Sdk()
            {
                PathSdk = pathPhp;
                PathRoot = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName.ToReversSlash();
                PathRoot.WriteLn("root path:");
                if (!IsAssemblyConfig())
                {
                    "".WriteLn("error");
                    return;
                }
                AssemblyIterator();
            }
            
            protected void AssemblyIterator()
            {
                disassembler.SetPath(PathRoot, PathSdk);
                
                "".WriteLn($"sdk path {disassembler._pathRoot}");
                "".WriteLn($"sharpie sdk path {disassembler._pathSdk}");

                foreach (var assembly in GetAssemblies())
                {
                    TypeIterator(assembly.GetTypes());
                }
            }
            
            private void TypeIterator(Type[] types)
            {
                foreach (Type type in types)
                {
                    disassembler.Add(type);
                }
            }
        }
    }
    
    public static class SdkExtension
    {
        public static void WriteLn(this string s, string v = "") {
            GD.Print($"[SDK] {v} " + s);
        }
        
        public static void WriteLn(this object s, string v = "")
        {
            GD.Print($"[SDK] {v} " + s.ToString());
        }
    }
}


