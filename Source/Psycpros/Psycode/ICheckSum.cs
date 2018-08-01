using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Psycpros.Psycode {
    class ICheckSum {
        private Dictionary<string, string> mHash;
        private MD5CryptoServiceProvider crypto;
        /**
         * Constructor
        **/
        public ICheckSum(string hashfile) {
            //Create hash dictonary
            mHash = new Dictionary<string, string>();

            //Create CryptoProvider
            crypto = new MD5CryptoServiceProvider();

            //Load Hash File.
            ReadHashFile(hashfile);
            

        }

        public string GetNameFromHash(byte[] str) {
            string b = System.Text.Encoding.UTF8.GetString(crypto.ComputeHash(str)); 
            
            if (mHash.ContainsKey(b)) {
                return mHash[b];
            }
            mHash[b] = "unknown";

            return mHash[b];
        }

        private void ReadHashFile(string filepath) {
            try {
                //Open Hash File
                BinaryReader sr = new BinaryReader(File.Open(filepath, FileMode.OpenOrCreate));

                //Read Hashes
                while(sr.BaseStream.Position < sr.BaseStream.Length) {
                    string key = sr.ReadString();
                    string val = sr.ReadString();

                    mHash.Add(key, val);
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
                foreach(KeyValuePair<string, string> kv in mHash) {
                    f.Write(kv.Key);
                    f.Write(kv.Value);
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
