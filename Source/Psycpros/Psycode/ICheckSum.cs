using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace Psycpros.Psycode {
    class ICheckSum {
        private Dictionary<string, string> mFiles;
        private Dictionary<string, string> mDirs;

        private MD5CryptoServiceProvider crypto;

        private string sFilename = "";
        /**
         * Constructor
        **/
        public ICheckSum(string hashfile) {
            //Create hash dictonary
            mFiles = new Dictionary<string, string>();
            mDirs  = new Dictionary<string, string>();

            //Store file name
            sFilename = hashfile;

            //Create CryptoProvider
            crypto = new MD5CryptoServiceProvider();

            //Load Hash File.
            ReadHashFile(hashfile);
            

        }

        public string GetDirFromHash(byte[] str, string defdir) {
            //Create an MD5 key, and convert it to a string
            string b = System.Text.Encoding.UTF8.GetString(crypto.ComputeHash(str));

            //See if the key is already in our table, return if it is.
            if(mDirs.ContainsKey(b)) {
                return defdir + "\\" + mDirs[b];
            }

            //If it isn't, create the table entry using console i/o.
            mDirs[b] = defdir;
            //Console.Write("    New Directory Name: ");
            //mDirs[b] = Console.ReadLine();

            //Save table and return
            SaveHashFile(sFilename);
            return mDirs[b];
        }

        public string GetNameFromHash(byte[] str, string defname) {
            string b = System.Text.Encoding.UTF8.GetString(crypto.ComputeHash(str)); 
            
            if (mFiles.ContainsKey(b)) {
                return mFiles[b];
            }
            mFiles[b] = defname;
            //Console.WriteLine("Editing Hash for File '" + defname + "'");
            //Console.Write("    New File Name: ");
            //mFiles[b] = Console.ReadLine();

            return mFiles[b];
        }

        private void ReadHashFile(string filepath) {
            try {
                //Open Hash File
                BinaryReader sr = new BinaryReader(File.Open(filepath, FileMode.OpenOrCreate));

                //Read Hashes
                while(sr.BaseStream.Position < sr.BaseStream.Length) {
                    string key = sr.ReadString();
                    string val = sr.ReadString();
                    string dir = sr.ReadString();

                    mFiles.Add(key, val);
                    mDirs.Add(key, dir);
                }
                sr.Close();
            }catch (Exception e) {
                Console.WriteLine(e.Message);
                return;
            }

            return;     
        }

        public void SaveHashFile(string filepath) {
            try {
                BinaryWriter f = new BinaryWriter(File.Open(filepath, FileMode.Truncate));

                //write hashes
                foreach(KeyValuePair<string, string> kv in mFiles) {
                    f.Write(kv.Key);
                    f.Write(kv.Value);
                    f.Write(mDirs[kv.Key]);
                }

                f.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            return;
        }
    }
}
