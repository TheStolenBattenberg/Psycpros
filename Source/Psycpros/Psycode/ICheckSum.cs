using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Psycpros.Psycode {
    class ICheckSum {
        private Dictionary<string, string> mFiles;
        private Dictionary<string, string> mDirs;

        private MD5CryptoServiceProvider crypto;
        /**
         * Constructor
        **/
        public ICheckSum(string hashfile) {
            //Create hash dictonary
            mFiles = new Dictionary<string, string>();
            mDirs  = new Dictionary<string, string>();

            //Create CryptoProvider
            crypto = new MD5CryptoServiceProvider();

            //Load Hash File.
            ReadHashFile(hashfile);
            

        }

        public string GetDirFromHash(byte[] str, string defdir) {
            string b = System.Text.Encoding.UTF8.GetString(crypto.ComputeHash(str));

            if(mDirs.ContainsKey(b)) {
                return mDirs[b];
            }

            mDirs[b] = defdir;
            return mDirs[b];
        }

        public string GetNameFromHash(byte[] str, string defname) {
            string b = System.Text.Encoding.UTF8.GetString(crypto.ComputeHash(str)); 
            
            if (mFiles.ContainsKey(b)) {
                return mFiles[b];
            }
            mFiles[b] = defname;

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
